using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkAnim : NetworkBehaviour {


	public override void OnStartLocalPlayer(){
		transform.FindChild("Corpo").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		transform.FindChild("Pernas").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
	}

	public override void PreStartClient(){
		transform.FindChild("Corpo").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		transform.FindChild("Pernas").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
	}


}
