using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MovePlayer : NetworkBehaviour {
    Animator animatorCorpo, animatorPernas; // Animator dos objetos Corpo e Pernas;
    GameObject corpo, pernas;
	int nEstado;
	//AnimatorControllerParameter[] anParam;

	// Use this for initialization
	void Start () {
        corpo = gameObject.transform.FindChild("Corpo").gameObject;
        pernas = gameObject.transform.FindChild("Pernas").gameObject;
        animatorCorpo = corpo.GetComponent<Animator>();
        animatorPernas = pernas.GetComponent<Animator>();
		corpo.GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		pernas.GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		//transform.FindChild("Corpo").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		//transform.FindChild("Pernas").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);



	}
	
	// Update is called once per frame
	void Update () {
		//anParam = transform.FindChild("Corpo").GetComponent<NetworkAnimator> ().animator.parameters;
		//Debug.Log ("Animaçao PARAM array pos 0: " + anParam [0].name);
		//transform.FindChild("Corpo").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		//transform.FindChild("Pernas").GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
		/*if (!isLocalPlayer) {
			return;
		}*/


		Cmd_syncAnim ();


	}
	//[Command]
	public void Cmd_syncAnim(){
		if(Input.GetMouseButton(0)){
			// Rotacionar para seguir o mouse;
			nEstado = 1;

			animatorCorpo.SetInteger("estado", nEstado);

			//corpo.GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
			GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = true;
			
		} else if(Input.GetMouseButton(1)){
			nEstado = 2;
			animatorCorpo.SetInteger("estado", nEstado);
			//corpo.GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
		} else{
			nEstado = 0;
			//if(!animatorCorpo.GetCurrentAnimatorStateInfo(0).IsName("atirando 1"))
			animatorCorpo.SetInteger("estado", nEstado);
			//corpo.GetComponent<NetworkAnimator>().SetParameterAutoSend(0,true);
			GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = false;
		}
		
		
		if (Input.GetKey(KeyCode.A))
		{
			animatorPernas.SetBool("andando", true);
			//GetComponent<NetworkAnimator> ().SetParameterAutoSend (0, true);
			
			// Código para mover; // Pode-se rotacionar as pernas;
			pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 180.0f);
		} else if(Input.GetKey(KeyCode.D)){
			animatorPernas.SetBool("andando", true);
			
			// Código para mover;
			pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 0.0f);
		} else if (Input.GetKey(KeyCode.W)){
			animatorPernas.SetBool("andando", true);
			
			// Código para mover;
			pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 270.0f);
		} else if (Input.GetKey(KeyCode.S)){
			animatorPernas.SetBool("andando", true);
			
			// Código para mover;
			pernas.transform.eulerAngles = new Vector3(pernas.transform.eulerAngles.x, pernas.transform.eulerAngles.y, 90.0f);
		} else{
			animatorPernas.SetBool("andando", false);
		}

	}

}
