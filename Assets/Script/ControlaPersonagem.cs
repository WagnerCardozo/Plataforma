using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaPersonagem : MonoBehaviour {
	[SerializeField] private float velocidade = 5;
	[SerializeField] private float alturaDoPulo = 6;
	
	private bool estaVivo;
	private bool viradoParaDireita;
	private bool estaNoChao;
	[SerializeField] private float raio;

	[SerializeField] private Transform posicaoDosPes;
	[SerializeField] private LayerMask mascaraChao;
	private Rigidbody2D rb;
	private Animator anim;

	public Transform PosicaoDosPes
	{
		get
		{ 
			return posicaoDosPes;
		}
	}

	public Rigidbody2D Rb
	{
		get
		{
			return rb;
		}
	}


	void Awake(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start () {
		viradoParaDireita = true;
		estaVivo = true;
	}
	
	// Update is called once per frame
	void Update () {
		estaNoChao = Physics2D.OverlapCircle (posicaoDosPes.position, raio, mascaraChao);

		anim.SetFloat("velocidade", Mathf.Abs(rb.velocity.x));
		anim.SetBool("estaNoChao", estaNoChao);
		
		if(Input.GetButtonDown("Jump")){
			Pular();
		}
		
		ReiniciarJogo();
		
		//print(estaNoChao);
	}
	
	void FixedUpdate(){
		
		if(estaVivo == true){
			float movHorizontal = Input.GetAxis("Horizontal");
			rb.velocity = new Vector2(movHorizontal * velocidade, rb.velocity.y);
			VirarPersonagem(movHorizontal);
		}
	}
	
	void VirarPersonagem(float movHorizontal){
		if(viradoParaDireita && movHorizontal < 0 || !viradoParaDireita && movHorizontal > 0){
			transform.localScale = new Vector3(	transform.localScale.x * -1, 
												transform.localScale.y,
												transform.localScale.z);
			
			viradoParaDireita = !viradoParaDireita;
		}
	}
	
	void Pular(){		
		if(estaNoChao == true && estaVivo == true){
			rb.AddForce(Vector2.up * alturaDoPulo, ForceMode2D.Impulse);
		}		
	}

	private void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (posicaoDosPes.position, raio);
	}
	
	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag("morte-instantanea")){
			Morreu();
		}
	} 
	
	private void Morreu(){
		rb.velocity = Vector2.zero;
		anim.SetTrigger("morreu");
		estaVivo = false;
	}
	
	private void ReiniciarJogo(){
		if(Input.GetKeyDown("r") && estaVivo == false){
			SceneManager.LoadScene(0);
		}
	}
}