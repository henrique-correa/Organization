using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public struct placar {
	public string nomeJogador;
	public int pontosJogador;
}

public class gerente : NetworkManager {
	public int p_count = 0;
	bool novoJogo = true;
	public bool jogoFinalizado;
	public placar[] Tabela = new placar[8]; 



	void Update(){
		if (jogoFinalizado == true) {
			Debug.Log ("SERVER: jogo acabou");
			Network.Disconnect(3);
		}
		//if (Tabela != null) {
			Debug.Log (Tabela [0].nomeJogador + " " + Tabela [0].pontosJogador);
		Debug.Log (Tabela [1].nomeJogador + " " + Tabela [1].pontosJogador);
		Debug.Log (Tabela [2].nomeJogador + " " + Tabela [2].pontosJogador);
		Debug.Log (Tabela [3].nomeJogador + " " + Tabela [3].pontosJogador);

		//}
		/*if (novoJogo == true) {
			GameObject m = Instantiate (Resources.Load ("maleta"), new Vector3 (-0.40f, -0.14f, 0.0f), Quaternion.identity) as GameObject;
			NetworkServer.Spawn (m);
			novoJogo = false;
		}*/
	}

	public override void OnServerAddPlayer(NetworkConnection con , short playerControllerId){
		if (p_count == 0) {
			Vector2 spawn_pos = new Vector2(-2.57f , -2.55f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 1";
			temp.pontosJogador = 0;
			Tabela[0] = temp;


			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().Cmd_set_cor(Color.blue);
			//player.gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.blue;

			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);


			return;
		}
		if (p_count == 1) {
			Vector2 spawn_pos = new Vector2(-2.34f , 2.46f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 2";
			temp.pontosJogador = 0;
			Tabela[1] = temp;

			p_count++;

			//player.gameObject.GetComponent<Jogador_controle>().Cmd_set_cor(Color.red);
			//player.gameObject.transform.FindChild("Corpo").GetComponent<SpriteRenderer>().color = Color.red;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);

			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);

			return;
		}
		if (p_count == 2) {
			Vector2 spawn_pos = new Vector2(2.40f , 2.59f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 3";
			temp.pontosJogador = 0;
			Tabela[2] = temp;

			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().Cmd_set_cor(Color.green);
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);

			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);


			return;
		}
		if (p_count == 3) {
			Vector2 spawn_pos = new Vector2(2.42f , -2.45f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp;
			temp.nomeJogador = "Jogador 4";
			temp.pontosJogador = 0;
			Tabela[3] = temp;

			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			//p_count++;
			return;
		}
		if (p_count == 4) {
			Vector2 spawn_pos = new Vector2(0.06f , -2.62f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);

			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 5";
			temp.pontosJogador = 0;
			Tabela[4] = temp;

			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			//p_count++;
			return;
		}
		if (p_count == 5) {
			Vector2 spawn_pos = new Vector2(-2.67f , -0.2f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);

			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 6";
			temp.pontosJogador = 0;
			Tabela[5] = temp;

			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			//p_count++;
			return;
		}
		if (p_count == 6) {
			Vector2 spawn_pos = new Vector2(0.06f , 2.55f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 7";
			temp.pontosJogador = 0;
			Tabela[6] = temp;
			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			//p_count++;
			return;
		}
		if (p_count == 7) {
			Vector2 spawn_pos = new Vector2(2.42f , -0.28f);
			GameObject player = (GameObject)Instantiate(base.playerPrefab, spawn_pos , Quaternion.identity);
			player.gameObject.GetComponent<Jogador_controle>().id = p_count;
			placar temp; 
			temp.nomeJogador = "Jogador 8";
			temp.pontosJogador = 0;
			Tabela[7] = temp;
			p_count++;
			//player.gameObject.GetComponent<Jogador_controle>().id = con.connectionId;
			//Debug.Log ("player ID " + player.gameObject.GetComponent<Jogador_controle>().id);
			NetworkServer.AddPlayerForConnection(con , player , playerControllerId);
			//p_count++;
			return;
		}
		//if (p_count == 1) {
			
		//}

		//lista_players.Add(con.playerControllers[0].);

	}
}
