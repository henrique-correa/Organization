using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gerente : NetworkManager {
	int p_count = 0;

	public override void OnServerAddPlayer(NetworkConnection con , short playerControllerId){
		if (p_count == 0) {
			Vector2 spawn_pos = new Vector2(-2.57f , -2.55f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			//SpriteRenderer[] s = player.gameObject.GetComponentsInChildren<SpriteRenderer>();
			player.gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.blue;
			player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			//s[2].color = Color.blue;
			//s[3].color = Color.blue;

			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);

			p_count++;
			return;
		}
		if (p_count == 1) {
			Vector2 spawn_pos = new Vector2(-2.34f , 2.46f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			//SpriteRenderer[] s2 = player.gameObject.GetComponentsInChildren<SpriteRenderer>();
			//s2[2].color = Color.red;
			//s2[3].color = Color.red;
			player.gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.red;
			//player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			//con.Send(MsgType.Rpc , )
			p_count++;
			return;
		}

		//lista_players.Add(con.playerControllers[0].);

	}
}
