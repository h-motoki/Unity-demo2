using UnityEngine;
using System.Collections;

using GameConstants;

public class PlayerAnimationManagerController : MonoBehaviour {
	
	public int setAnimation(Animator controller, 
	                         bool punch_button_flg, 
	                         bool kick_button_flg) {

		int attackMode = GameConstants.PlayerConstants.MODE_OFFENSIVE;
		AnimatorStateInfo animInfo = controller.GetCurrentAnimatorStateInfo (0);

		if (animInfo.shortNameHash == GameConstants.PlayerConstants.ATTACK1 || 
		    animInfo.shortNameHash == GameConstants.PlayerConstants.ATTACK2) {
			attackMode = GameConstants.PlayerConstants.MODE_ATTACK_NOW;
			controller.SetBool("attack_flag", true);
			controller.SetBool("attack1_flag", false);
			controller.SetBool("attack2_flag", false);

		} else {
			if (punch_button_flg) {
				if (animInfo.shortNameHash != GameConstants.PlayerConstants.ATTACK1){
					controller.SetBool("attack1_flag", punch_button_flg);
					attackMode = GameConstants.PlayerConstants.MODE_ATTACK1;
				}
			} else if(kick_button_flg){
				if (animInfo.shortNameHash != GameConstants.PlayerConstants.ATTACK2){
					controller.SetBool("attack2_flag", kick_button_flg);
					attackMode = GameConstants.PlayerConstants.MODE_ATTACK2;
				}
			} else {
				controller.SetBool("attack_flag", true);
				controller.SetBool("attack1_flag", false);
				controller.SetBool("attack2_flag", false);
				attackMode = GameConstants.PlayerConstants.MODE_OFFENSIVE;
			}
		}

		return attackMode;
	}
}
