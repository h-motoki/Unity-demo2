using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InputManagerController : MonoBehaviour {

	private float move_up    = 0.0f;
	private float move_down  = 0.0f;
	private float move_left  = 0.0f;
	private float move_right = 0.0f;

	private bool punch_button_flg = false;
	private bool kick_button_flg = false;
	private bool offensive_flg = false;

	private Dictionary<string, Sprite> buttonTextureHashMap = new Dictionary<string, Sprite>();
	private Dictionary<string, Image> buttonImageHashMap = new Dictionary<string, Image>();

	void Start() {
		buttonTextureHashMap.Add ("moveup_up", 		Resources.Load<Sprite>("UI/icon_cross01_1_up"));
		buttonTextureHashMap.Add ("moveup_down", 	Resources.Load<Sprite> ("UI/icon_cross01_1b_up"));
		buttonTextureHashMap.Add ("movedown_up", 	Resources.Load<Sprite> ("UI/icon_cross01_1_down"));
		buttonTextureHashMap.Add ("movedown_down", 	Resources.Load<Sprite> ("UI/icon_cross01_1b_down"));
		buttonTextureHashMap.Add ("moveleft_up", 	Resources.Load<Sprite> ("UI/icon_cross01_1_left"));
		buttonTextureHashMap.Add ("moveleft_down", 	Resources.Load<Sprite> ("UI/icon_cross01_1b_left"));
		buttonTextureHashMap.Add ("moveright_up", 	Resources.Load<Sprite> ("UI/icon_cross01_1_right"));
		buttonTextureHashMap.Add ("moveright_down", Resources.Load<Sprite> ("UI/icon_cross01_1b_right"));
	
		buttonTextureHashMap.Add ("punch_button_over", Resources.Load<Sprite> ("UI/パンチボタンOver"));
		buttonTextureHashMap.Add ("punch_button_up", Resources.Load<Sprite> ("UI/パンチボタンUp"));
		buttonTextureHashMap.Add ("kick_button_over", Resources.Load<Sprite> ("UI/キックボタンOver"));
		buttonTextureHashMap.Add ("kick_button_up", Resources.Load<Sprite> ("UI/キックボタンUp"));
		buttonTextureHashMap.Add ("offensive_button_down", Resources.Load<Sprite> ("UI/構えボタンOver"));
		buttonTextureHashMap.Add ("offensive_button_up", Resources.Load<Sprite> ("UI/構えボタンUp"));

		buttonImageHashMap.Add ("move_up", 			GameObject.Find ("GameUI/KeyPanel/KeyUp").GetComponent<Image>());
		buttonImageHashMap.Add ("move_down", 		GameObject.Find ("GameUI/KeyPanel/KeyDown").GetComponent<Image>());
		buttonImageHashMap.Add ("move_left", 		GameObject.Find ("GameUI/KeyPanel/KeyLeft").GetComponent<Image>());
		buttonImageHashMap.Add ("move_right", 		GameObject.Find ("GameUI/KeyPanel/KeyRight").GetComponent<Image>());

		buttonImageHashMap.Add ("punch_button", 	GameObject.Find ("GameUI/InputPanel/PunchButton").GetComponent<Image>());
		buttonImageHashMap.Add ("kick_button", 		GameObject.Find ("GameUI/InputPanel/KickButton").GetComponent<Image>());
		buttonImageHashMap.Add ("offensive_button", GameObject.Find ("GameUI/InputPanel/OffensiveButton").GetComponent<Image>());
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public void OnMoveUpPointerDownClick(BaseEventData data){
		buttonImageHashMap ["move_up"].sprite = buttonTextureHashMap ["moveup_down"];
		move_up = 1.0f;
	}

	public void OnMoveDownPointerDownClick(BaseEventData data){
		buttonImageHashMap ["move_down"].sprite = buttonTextureHashMap ["movedown_down"];
		move_down = -1.0f;
	}

	public void OnMoveLeftPointerDownClick(BaseEventData data){
		buttonImageHashMap ["move_left"].sprite = buttonTextureHashMap ["moveleft_down"];
		move_left = -1.0f;
	}

	public void OnMoveRightPointerDownClick(BaseEventData data){
		buttonImageHashMap ["move_right"].sprite = buttonTextureHashMap ["moveright_down"];
		move_right = 1.0f;
	}

	public void OnMoveUpPointerUpClick(BaseEventData data){
		buttonImageHashMap ["move_up"].sprite = buttonTextureHashMap ["moveup_up"];
		move_up = 0.0f;
	}
	
	public void OnMoveDownPointerUpClick(BaseEventData data){
		buttonImageHashMap ["move_down"].sprite = buttonTextureHashMap ["movedown_up"];
		move_down = 0.0f;
	}
	
	public void OnMoveLeftPointerUpClick(BaseEventData data){
		buttonImageHashMap ["move_left"].sprite = buttonTextureHashMap ["moveleft_up"];
		move_left = 0.0f;
	}
	
	public void OnMoveRightPointerUpClick(BaseEventData data){
		buttonImageHashMap ["move_right"].sprite = buttonTextureHashMap ["moveright_up"];
		move_right = 0.0f;
	}

	public void OnPunchButtonClick(BaseEventData data){
		punch_button_flg = !punch_button_flg;
		if (punch_button_flg) {
			buttonImageHashMap ["punch_button"].sprite = buttonTextureHashMap ["punch_button_over"];
		} else {
			buttonImageHashMap ["punch_button"].sprite = buttonTextureHashMap ["punch_button_up"];
		}
	}
	
	public void OnKickButtonClick(BaseEventData data){
		kick_button_flg = !kick_button_flg;
		if (kick_button_flg) {
			buttonImageHashMap ["kick_button"].sprite = buttonTextureHashMap ["kick_button_over"];
		} else {
			buttonImageHashMap ["kick_button"].sprite = buttonTextureHashMap ["kick_button_up"];
		}
	}

	public void OnOffensiveDownClick(BaseEventData data){
		offensive_flg = !offensive_flg;
		if (offensive_flg) {
			buttonImageHashMap ["offensive_button"].sprite = buttonTextureHashMap ["offensive_button_down"];
		} else {
			buttonImageHashMap ["offensive_button"].sprite = buttonTextureHashMap ["offensive_button_up"];
		}
	}

	public bool isMove() {
		if (move_up    != 0.0f || 
		 	move_down  != 0.0f || 
		 	move_left  != 0.0f || 
		    move_right != 0.0f) { 
			return true; 
		}

		return false;
	}

	public bool isOffensive() {
		return offensive_flg;
	}

	public float getHorizontal(){
		return move_left + move_right;
	}

	public float getVertical() {
		return move_up + move_down;
	}

	public bool getPunchButtonFlg() {
		return punch_button_flg;
	}

	public bool getKickButtonFlg() {
		return kick_button_flg;
	}

	public bool getOffensiveFlg() {
		return offensive_flg;
	}

}
