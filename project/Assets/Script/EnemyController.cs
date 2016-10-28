using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int hp;
	public int attack;
	public int defence;
	public float runspeed;
	public float walkspeed;

	public Text damageTextObj;
	public GameObject canvas;
	public GameObject gameScript;

	public float enemyThinkWaitTime;

	private Slider hp_bar;
	private AudioManagerController audioManager;
	private Animator animatorController;
	private MoveManagerController moveManager;
	private CharacterController controller;

	private GameObject playerGameObject;
	private PlayerController playerController;
	private Animator animator;

	private float enemyWaitTime = 0.0f;
	private float enemyWalkTime = 0.0f;
	private float enemyWalkRandomV = 0.0f;
	private float enemyWalkRandomH = 0.0f;

	private int enemyMode = 0;

	// Use this for initialization
	void Start () {
		controller 			= GetComponent<CharacterController> ();
		hp_bar  	 		= transform.GetComponentInChildren<Slider>();
		audioManager 		= gameScript.GetComponent<AudioManagerController> ();
		animatorController 	= gameObject.GetComponent<Animator> ();
		moveManager  		= gameScript.GetComponent<MoveManagerController> ();

		playerGameObject = GameObject.Find ("Player");
		playerController = playerGameObject.GetComponent<PlayerController> ();
		animator 		 = playerGameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		// 敵の検知
		if(enemyMode <= 2) {
			float distance = Vector3.Distance(playerGameObject.transform.position, this.transform.position);
			if (distance <= 20.0f) {
				enemyMode = 2;
			}
		}

		switch(enemyMode) {
		case 0://待機モード
			if(enemyWaitTime < enemyThinkWaitTime) {
				enemyWaitTime += Time.deltaTime;
				animatorController.SetBool("attack_flag", false);
				animatorController.SetInteger("speed", 0);
			} else {
				enemyWaitTime = 0.0f;
				enemyMode = 1;
			}
			break;

		case 1://移動モード
			if (enemyWalkRandomV == 0.0f && enemyWalkRandomH == 0.0f){
				enemyWalkRandomV = Random.Range(-1.0f,1.0f);
				enemyWalkRandomH = Random.Range(-1.0f,1.0f);
			}

			if(enemyWalkTime < 4.0f) {
				enemyWalkTime += Time.deltaTime;

				moveManager.enemyMove(transform,
				                      controller,
				                      enemyWalkRandomV,
				                      enemyWalkRandomH,
				                      walkspeed);

				animatorController.SetBool("attack_flag", false);
				animatorController.SetInteger("speed", (int)walkspeed);
			} else {
				enemyWalkRandomV = 0.0f;
				enemyWalkRandomH = 0.0f;
				enemyWalkTime = 0.0f;
				enemyMode = 0;
			}
			break;

		case 2:
			float distance = Vector3.Distance(playerGameObject.transform.position, this.transform.position);
			if (distance >= 3.0f) {
				moveManager.enemyMoveSeach(transform,
				                           controller,
				                           playerGameObject.transform,
				                           runspeed);
				// 走るアニメーション
				animatorController.SetBool("attack_flag", false);
				animatorController.SetInteger("speed", (int)runspeed);
			} else {
				enemyMode = 3;

				// 構えアニメーション
				animatorController.SetBool("attack_flag", true);
				animatorController.SetInteger("speed", (int)0);
			}
			break;

		case 3: // 攻撃モード
			//未実装
			break;
		}
	}

	void OnTriggerEnter(Collider other) {
		// 衝突した相手がplayerの場合
		if(other.gameObject.CompareTag("Player")){
			//攻撃力とHPを取得
			PlayerController playerController = null;
			if(other.gameObject.GetComponentInParent<PlayerController>() != null){
				playerController = other.gameObject.GetComponentInParent<PlayerController>();
			}
			else if(other.gameObject.GetComponentInChildren<PlayerController>() != null){
				playerController = other.gameObject.GetComponentInChildren<PlayerController>();
			} 
			else {
				playerController = other.gameObject.GetComponent<PlayerController>();
			}

			int damage = playerController.attack - defence;
			damage = damage < 0 ? 0 : damage;

			// アニメーション情報を取得
			AnimatorStateInfo animInfo;
			if(other.gameObject.GetComponentInParent<Animator>() != null){
				animInfo = other.gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0);
			}
			else if(other.gameObject.GetComponentInChildren<Animator>() != null){
				animInfo = other.gameObject.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0);
			} 
			else {
				animInfo = other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
			}
			// アニメーション１の攻撃だった場合
			if(animInfo.shortNameHash == Animator.StringToHash("attack1")) {
				this.hp_bar.value -= damage;
			} else if(animInfo.shortNameHash == Animator.StringToHash("attack2")){
				this.hp_bar.value -= damage;
			}

			hp -= damage;

			// ダメージを表示
			Text damageText = Instantiate(damageTextObj, Vector3.zero, Quaternion.identity) as Text;
			damageText.transform.SetParent(canvas.transform, false);
			damageText.text = "" + damage;

			/* 撃破処理 */
			if (hp < 0) {
				audioManager.PlaySE("se_gekiha");
				Destroy (this.gameObject,0.3f);
			}
		}
	}
}
