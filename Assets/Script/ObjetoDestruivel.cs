using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDestruivel : MonoBehaviour {

	private float alturaDopulo = 5;

	private void OnCollisionEnter2D(Collision2D collision){
		ControlaPersonagem personagem = collision.collider.
			gameObject.GetComponent<ControlaPersonagem> ();

		if (collision.gameObject.CompareTag ("Player") && 
						personagem.PosicaoDosPes.transform.position.y > this.transform.position.y) {

			personagem.Rb.AddForce (Vector2.up * alturaDopulo, ForceMode2D.Impulse);
			Destroy (this.gameObject);		
		}
	}
}
