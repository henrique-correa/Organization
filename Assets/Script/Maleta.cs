using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Maleta : NetworkBehaviour {

	public bool verdadeira;

	// Use this for initialization
	void Start () {
		int temp = Random.Range (0, 100);
		if (temp <= 49) {
			Debug.Log (temp);
			verdadeira = true;
		} else {
			Debug.Log (temp);
			verdadeira = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
