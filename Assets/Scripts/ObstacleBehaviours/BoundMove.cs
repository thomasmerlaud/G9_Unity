using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundMove : MonoBehaviour {
	[SerializeField] private GameObject waypointBottomLeft; //Définit le bord bas gauche de la zone limite via un point objet
	[SerializeField] private GameObject waypointTopRight; //Définit le bord haut droite de la zone limite via un point objet
	
	[SerializeField] private float moveSpeed = 5f; //Vitesse
	[SerializeField] private int xSens = 0; //sens x : 1 droite, -1 gauche
	[SerializeField] private int ySens = 1; //sens y : 1 haut, -1 bas
	[SerializeField] private Rigidbody2D rb;
	
	private float maxLeft; //Définit la limite gauche de la zone de rebond
	private float maxRight; //Définit la limite droite de la zone de rebond
	private float maxTop; //Définit la limite haut de la zone de rebond
	private float maxBottom; //Définit la limite bas de la zone de rebond
	
	private Vector2 sens; //Le sens de l'obstacle lorsqu'il bouge
	
	//On se sert des positions x et y des waypoints pour définir les limites max de la zone de rebond
	void Start(){
		maxLeft = waypointBottomLeft.transform.position.x;
		maxRight = waypointTopRight.transform.position.x;
		maxTop = waypointTopRight.transform.position.y;
		maxBottom = waypointBottomLeft.transform.position.y;
		
		Debug.Log(maxLeft + ", " + maxRight + ", " + maxTop + ", " + maxBottom + ", ");
		
		sens = new Vector2(xSens, ySens);
	}

	void FixedUpdate() {
		Debug.Log(rb.position);
		//Si la position x va vers la droite et dépasse la borne droite de la zone de rebond, on change le sens en x vers la gauche
		
		if(rb.position.x > maxRight && sens.x > 0){
			rb.position = new Vector2(maxRight, rb.position.y);
			sens.x = sens.x * (-1);
		}
		
		//Si la position x va vers la gauche et dépasse la borne gauche de la zone de rebond, on change le sens en x vers la droite
		if(rb.position.x < maxLeft && sens.x < 0){
			rb.position = new Vector2(maxLeft, rb.position.y);
			sens.x = sens.x * (-1);
		}
		
		//Si la position y va vers le haut et dépasse la borne haute de la zone de rebond, on change le sens en y vers le bas
		if(rb.position.y > maxTop && sens.y > 0){
			rb.position = new Vector2(rb.position.x, maxTop);
			sens.y = sens.y * (-1);
		}
		
		//Si la position y va vers le bas et dépasse la borne basse de la zone de rebond, on change le sens en y vers le haut
		if(rb.position.y < maxBottom && sens.y < 0){
			rb.position = new Vector2(rb.position.x, maxBottom);
			sens.y = sens.y * (-1);
		}

		rb.MovePosition(rb.position + sens * moveSpeed * Time.fixedDeltaTime);
	}

	public void initParameters(Vector2 _delay, Vector2 _sens, float _speed, GameObject _waypointBottomLeft, GameObject _waypointTopRight){
		moveSpeed = _speed;
		xSens = (int) _sens.x;
		ySens = (int) _sens.y;
		sens = _sens;
		
		transform.position = new Vector2(transform.position.x + _delay.x, transform.position.y + _delay.y);
		waypointBottomLeft = _waypointBottomLeft;
		waypointTopRight = _waypointTopRight;
	}
}
