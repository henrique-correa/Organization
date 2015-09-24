using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Tiro : NetworkBehaviour {

	float vel_tiro = 1.0f;
	public int tiro_id;
	public int dano = 10;
	public int pontos_por_dano = 10;


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
			var g = col.gameObject.GetComponent<Jogador_controle>();
			if(g != null){
				g.dano(dano);

				//gerente.singleton.GetComponent<gerente>().placar[tiro_id] += pontos_por_dano;//g.add_pontos(pontos_por_dano);
			}
			Destroy(gameObject);


		}
	}
}
