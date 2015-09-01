using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Jogador_move : NetworkBehaviour {


	Vector2 mouse_look;
	Vector3 dir;
	public GameObject tiro_spawn;
	public int vida;
	public int pontos;
	public bool maleta;
	public int cor;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		} else {

			//WASD move o jogador de acordo com as coordenadas do MUNDO

			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector2.up * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (Vector2.left * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (Vector2.down * Time.deltaTime, Space.World);
			}
			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector2.right * Time.deltaTime, Space.World);
			}


			// rotaciona o jogador em direçao ao mouse

			mouse_look = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			dir = new Vector3 (mouse_look.x, mouse_look.y, 0.0f) - transform.position;
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);


			if (Input.GetMouseButton (0)) {
				Cmd_atirar ();
			}
		}
	
	}

	[Command]
	void Cmd_atirar(){
		GameObject t = Instantiate (Resources.Load ("tiro"),tiro_spawn.transform.position, gameObject.transform.rotation) as GameObject;
		t.GetComponent<tiro>().tiro_id = 1;
		NetworkServer.Spawn (t);

		//NetworkServer.SpawnObjects ();


	}


}
