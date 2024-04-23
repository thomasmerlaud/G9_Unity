using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpaqueNext : MonoBehaviour {

	[SerializeField] private float distance = 2f; //Distance à partir duquel si le joueur est proche, l'objet commence à s'afficher
	[SerializeField] private float marge = 0.25f; //Distance maximale où l'objet est opaque

	private SpriteRenderer sprite;
	private GameObject player;
	private bool isFullOpaque = false; //Si l'objet est dans un état pleinement opaque
	private bool isFullTransparent = false; //Si l'objet est dans un état pleinement transparent

	void Start() {
		sprite = GetComponent<SpriteRenderer>(); //On récsupère le sprite de l'objet
		player = PlayerManager.GetPlayer(); //On récupère l'objet Joueur
	}

	// Update is called once per frame
	void Update() {
		float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position); //Calcule la distance entre le joueur et l'objet
		if(distanceFromPlayer < distance){ //Si le joueur est assez proche de l'objet
			if(distanceFromPlayer > distance - marge){ //Si le joueur est entre la distance max et la marge
				if(isFullOpaque){ //On met à faux la variable d'état pleinement opaque
					isFullOpaque = false;
				}
				if(isFullTransparent){ //On met à faux la variable d'état pleinement transparent
					isFullTransparent = false;
				}
				//L'opacité du sprite est proportionnel à la position du joueur entre la distance max et la marge
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, (distance - distanceFromPlayer)/(distance - marge));
			} else {
				//Sinon, dans le cas où le joueur a dépassé la marge, on met l'opacité à 1 (opaque)
				if(!isFullOpaque){
					sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 1);
					isFullOpaque = true; //L'objet est désormais pleinement opaque (évite d'appliquer tout le temps la condition quand le joueur est proche)
					isFullTransparent = false;
				}
			}
		} else {
			//Sinon, dans le cas où le joueur a dépassé la distance, on met l'opacité à 0 (transparence)
			if(!isFullTransparent){
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, 0);
				isFullTransparent = true; //L'objet est pleinement transparent (évite d'appliquer tout le temps la condition quand le joueur est loin)
				isFullOpaque = false;
			}
		}
	}
}
