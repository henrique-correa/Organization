using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Socar : NetworkBehaviour {

	public string Id_pai;
	// Use this for initialization
	void Start () {
		Id_pai = transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D col){

	}
}
