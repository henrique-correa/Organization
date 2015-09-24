using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class gerente : NetworkManager {
	public int[] placar;// = new int[8];
	IList <PlayerController> lista_players = new List<PlayerController>();
	// Use this for initialization
	int _id = 0;

	public override void OnServerAddPlayer(NetworkConnection con , short player_id){
		con.playerControllers [0].gameObject.GetComponent<Jogador_controle> ().id = _id;
		_id++;
		//lista_players.Add(con.playerControllers[0].);

	}
}
