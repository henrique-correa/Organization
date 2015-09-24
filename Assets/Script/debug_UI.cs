using UnityEngine;
using System.Collections;
using  UnityEngine.UI;
using UnityEngine.Networking;

public class debug_UI : NetworkBehaviour {
	GameObject[] j_temp = null;

	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {
		//if (!isLocalPlayer) {
		//	return;
		//}
		//if (j_temp == null) {
			//j_temp = GameObject.FindGameObjectsWithTag ("Player");

			//foreach(GameObject o in j_temp)
			//{
				//if(Network.player){
					//GameObject.Find ("debug_vida").GetComponent<Text> ().text = "vida: " + GetComponent<Jogador_controle> ().vida.ToString ();
					//GameObject.Find ("debug_pontos").GetComponent<Text> ().text = "pontos: " + j_temp.GetComponent<Jogador_controle> ().pontos.ToString ();
					//GameObject.Find ("debug_maleta").GetComponent<Text> ().text = "maleta: " + j_temp.GetComponent<Jogador_controle> ().maleta.ToString ();

				//}	
			//}
		//} //else {

			/*if (!isLocalPlayer) {
			return;
		}*/
			//j_temp = GameObject.FindGameObjectWithTag("Player");

		//}
	}
}
