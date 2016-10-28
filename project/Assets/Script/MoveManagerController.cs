using UnityEngine;
using System.Collections;

public class MoveManagerController : MonoBehaviour {

	public float gravity = 9.8f;
	public float rotSpeed = 90f;

	public void playerAttackMove(Transform playerTransfome, 
	                             CharacterController controller, 
	                             float speed) {
		Vector3 moveDirection = playerTransfome.TransformDirection(Vector3.forward);
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * speed * Time.deltaTime);
		//方向転換
		//moveDirection.y = 0;
		if (moveDirection.sqrMagnitude > 0.001) {
			Vector3 newDir = Vector3.RotateTowards (playerTransfome.position, moveDirection, rotSpeed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation (newDir);
		}
	}

	public void playerMove(Transform cameraTransform, 
	                       Transform playerTransform,
	                       CharacterController controller, 
	                       float h,
	                       float v,
	                       float speed){
		// プレイヤーの移動
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		Vector3 right   = cameraTransform.TransformDirection(Vector3.right);

		Vector3 moveDirection = h*right + v*forward;
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * speed * Time.deltaTime);

		//方向転換
		moveDirection.y = 0;
		if (moveDirection.sqrMagnitude > 0.001) {
			Vector3 newDir = Vector3.RotateTowards (playerTransform.position, 
			                                        moveDirection, 
			                                        rotSpeed * Time.deltaTime, 
			                                        0.0f);
			playerTransform.rotation = Quaternion.LookRotation (newDir);
		}
	}
	
	public void enemyMove(Transform enemyTransform,
	                      CharacterController controller, 
	                      float h,
	                      float v,
	                      float speed){
		// プレイヤーの移動
		Vector3 moveDirection = new Vector3(h,0,v);
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * speed * Time.deltaTime);
	
		//方向転換
		moveDirection.y = 0;
		if (moveDirection.sqrMagnitude > 0.001) {
			Vector3 newDir = Vector3.RotateTowards (enemyTransform.position, 
			                                        moveDirection, 
			                                        rotSpeed * Time.deltaTime, 
			                                        0.0f);
			enemyTransform.rotation = Quaternion.LookRotation (newDir);
		}
	}

	public void enemyMoveSeach( Transform enemyTransform,
	                           CharacterController enemyCharacterController,
								Transform targetTransform,
	                           	float speed){
		enemyTransform.LookAt (targetTransform);
		Vector3 target = enemyTransform.forward;
		target.y -= gravity * Time.deltaTime;
		enemyCharacterController.Move (target * speed * Time.deltaTime);
	}
}
