using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Jogador_Id : NetworkBehaviour {

	[SyncVar] public string PlayerUniqueIdentity;
	NetworkInstanceId playerNetID;
	Transform myTransform;

	public override void OnStartLocalPlayer(){
		GetNetIdentity ();
		SetIdentity ();
	} 
	// Use this for initialization
	void Awake () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (myTransform.name == "" || myTransform.name == "Jogador(Clone)") {
			SetIdentity();
		}
	
	}

	[Client]
		void GetNetIdentity(){
		playerNetID = GetComponent<NetworkIdentity> ().netId;
		Cmd_TellServerMyIdentity (MakeUniqueIdentity ());
	}


		void SetIdentity(){
			if (!isLocalPlayer) {	
			myTransform.name = PlayerUniqueIdentity;
		} else {
			myTransform.name = MakeUniqueIdentity();
		}
	}

	string MakeUniqueIdentity(){
		string uniqueName = "Player_" + playerNetID.ToString ();
		return uniqueName;
	}


	[Command]
	void Cmd_TellServerMyIdentity(string name){
		PlayerUniqueIdentity = name;
	}
}
