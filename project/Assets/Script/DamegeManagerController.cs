using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamegeManagerController : MonoBehaviour {

	private Text damegeText;
	private float time = 0.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1);
		damegeText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up*Time.deltaTime*50);
		time += Time.deltaTime;
		damegeText.color = new Color(damegeText.color.r, damegeText.color.g, damegeText.color.b, 1.5F - time);
	}
}
