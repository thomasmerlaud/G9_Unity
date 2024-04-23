using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveLoop : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 5f; //Vitesse de l'objet, modifiable
	[SerializeField] private int ySens = 1; //Le sens de l'objet (1 si en bas, -1 si en haut)
	[SerializeField] private Rigidbody2D rb; //Le rigidbody pour bouger l'obstacle
	private Vector2 movement;
	private int ecart = 3;
	
	//Au démarrage, défini la variable de mouvement
	void Start(){
		movement = new Vector2(0, ySens);
	}

	//A chaque frame, on bouge l'objet via son rigidbody dans le mouvement défini * la vitesse de l'objet moveSpeed * Time.fixedDeltaTime le laps de temps écoulé en 1 frame
	void FixedUpdate() {
		//Si la position absolue y de l'objet dépasse la taille du tableau, on multiplie sa position par -1 pour qu'il se retrouve à l'opposé du tableau.
		if(Mathf.Abs(rb.position.y) > TableauManager.maxHeight){
			rb.position = new Vector2(rb.position. x,rb.position.y * (-1));
		}
		//On bouge le cercle via son rigidbody
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	//Une méthode qui permet d'initialiser les paramètres avec certaines valeurs si besoin
	public void initParameters(float yDelay, float _speed, int _ySens){
		
		//On peut décaler la position d'un certain nombre avec yDelay,
		transform.position = new Vector2(transform.position.x , transform.position.y - (yDelay)%ecart);
		moveSpeed = _speed;
		ySens = _ySens;
		movement = new Vector2(0, ySens);
	}
}
