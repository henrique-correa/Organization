using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
    Animator animatorCorpo, animatorPernas; // Animator dos objetos Corpo e Pernas;
    GameObject corpo, pernas;

	// Use this for initialization
	void Start () {
        corpo = gameObject.transform.FindChild("Corpo").gameObject;
        pernas = gameObject.transform.FindChild("Pernas").gameObject;
        animatorCorpo = corpo.GetComponent<Animator>();
        animatorPernas = pernas.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButton(0)){
            // Rotacionar para seguir o mouse;
            animatorCorpo.SetInteger("estado", 1);
			GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = true;

        } else if(Input.GetMouseButton(1)){
            animatorCorpo.SetInteger("estado", 2);
        } else{
            //if(!animatorCorpo.GetCurrentAnimatorStateInfo(0).IsName("atirando 1"))
            animatorCorpo.SetInteger("estado", 0);
			GameObject.FindWithTag ("spawn_tiro").GetComponent<SpriteRenderer> ().enabled = false;
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            animatorPernas.SetBool("andando", true);

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
