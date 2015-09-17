using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Tiro : NetworkBehaviour {

	float vel_tiro = 1.0f;
	public int tiro_id;


	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2.0f);
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right * vel_tiro * Time.deltaTime);

	
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.collider.gameObject.tag == "tiro") {
			Destroy(gameObject);
		}
		if (col.collider.gameObject.tag == "Player") {
			col.gameObject.GetComponent<Jogador_controle>().vida -= 10;


		}
	}
}
