using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class tiro : NetworkBehaviour {

	float vel_tiro = 1.0f;
	public int tiro_id;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!isServer) {
			return;
		} else {

			transform.Translate (Vector2.right * vel_tiro * Time.deltaTime);
		}
	
	}
}
