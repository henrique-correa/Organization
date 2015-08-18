using UnityEngine;
using System.Collections;

public class tiro : MonoBehaviour {

	float vel_tiro = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector2.right * vel_tiro * Time.deltaTime);
	
	}
}
