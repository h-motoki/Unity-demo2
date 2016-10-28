using UnityEngine;
using System.Collections;

using GameConstants;

public class PlayerController : MonoBehaviour {
	
	public float runspeed;
	public float walkspeed;
	public float radius;
	public float angleH;

	public int hp;
	public int attack;
	public int defence;

	public GameObject gameScript;

	private InputManagerController inputManager;
	private PlayerAnimationManagerController playerManager;
	private MoveManagerController moveManager;

	private AudioManagerController audioManager;

	private CharacterController controller;
	private Animator animatorController;

	void Start()
	{
		controller 			= GetComponent<CharacterController> ();
		animatorController 	= GetComponent<Animator> ();
		inputManager 		= gameScript.GetComponent<InputManagerController> ();
		playerManager 		= gameScript.GetComponent<PlayerAnimationManagerController> ();
		moveManager 		= gameScript.GetComponent<MoveManagerController> ();

		audioManager = gameScript.GetComponent<AudioManagerController> ();
		audioManager.PlayBGM ("bgm_normal_battle");
	}
	
	void Update()
	{
		if(Input.GetKey(KeyCode.L)) {
			RectTransform textRect = GameObject.Find ("GameUI/HPPanel/HPNormal").GetComponent<RectTransform>();
			Vector2 temp = textRect.sizeDelta;
			temp.x -= 10.0f;
			textRect.sizeDelta = temp;
		}

		if (inputManager.isMove()) {
			float speed;
			if(inputManager.isOffensive()) {
				speed = walkspeed;
			} else {
				speed = runspeed;
			}

			if (inputManager.isOffensive()) {
				int attackMode = playerManager.setAnimation(animatorController, 
				                                            inputManager.getPunchButtonFlg(),
				                                            inputManager.getKickButtonFlg());

				switch(attackMode){
					case GameConstants.PlayerConstants.MODE_ATTACK_NOW:
						speed = 0.5f;
						moveManager.playerAttackMove(transform, controller, speed);
						break;
					case GameConstants.PlayerConstants.MODE_ATTACK1:
						speed = 0.5f;
						moveManager.playerAttackMove(transform, controller, speed);	
						break;
					case GameConstants.PlayerConstants.MODE_ATTACK2:
						speed = 0.5f;
						moveManager.playerAttackMove(transform, controller, speed);	
						break;	
					default:
						moveManager.playerMove(Camera.main.transform,
					                       	   	transform,
					                       		controller,
					                       		inputManager.getHorizontal(),
					                       		inputManager.getVertical(),
					                       		speed);	
						break;
				}
			} else {
				animatorController.SetBool("attack_flag", false);
				animatorController.SetInteger("speed", (int)speed);

				// プレイヤーの移動
				moveManager.playerMove(Camera.main.transform,
				                       transform,
				                       controller,
				                       inputManager.getHorizontal(),
				                       inputManager.getVertical(),
				                       speed);
			}
		} else {
			if (inputManager.isOffensive()) {
				int attackMode = playerManager.setAnimation(animatorController, 
				                                            inputManager.getPunchButtonFlg(),
				                                            inputManager.getKickButtonFlg());

				float speed;
				switch(attackMode){
				case GameConstants.PlayerConstants.MODE_ATTACK_NOW:
					speed = 0.5f;
					moveManager.playerAttackMove(transform, controller, speed);
					break;
				case GameConstants.PlayerConstants.MODE_ATTACK1:
					speed = 0.5f;
					moveManager.playerAttackMove(transform, controller, speed);	
					break;
				case GameConstants.PlayerConstants.MODE_ATTACK2:
					speed = 0.5f;
					moveManager.playerAttackMove(transform, controller, speed);	
					break;
				}
			} else {

				animatorController.SetBool("attack_flag", false);
				animatorController.SetInteger("speed", 0);
			}
		}

		// カメラの追従処理追加
		Camera.main.transform.position = new Vector3 (transform.position.x - 3, 
		                               				  transform.position.y + 2, 
		                               				  transform.position.z);

	}

	/*
	 * 敵の攻撃受けた時の判定
	 */
	void OnTriggerEnter(Collider other) {

	}

	void LateUpdate() {
		if (Input.GetKey (KeyCode.O) || Input.GetKey (KeyCode.P)) {
			if (Input.GetKey (KeyCode.O)) {
				angleH += 1f * 180 * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.P)) {
				angleH += -1f * 180 * Time.deltaTime;
			}
			Vector3 rotDir = Quaternion.Euler (30f, angleH, 20f) * Vector3.back;
			Camera.main.transform.position = transform.position + (radius * rotDir);
			Camera.main.transform.LookAt (transform.position);
		}
	}
}
