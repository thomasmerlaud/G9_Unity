using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
	public float x; //Rentrer ici la coordonnée x où vous souhaitez téléporter le joueur
	public float y; //Rentrer ici la coordonnée y où vous souhaitez téléporter le joueur
	public int numTableau; //Rentrer ici l'indice du tableau que vous voulez désormais afficher
	
	public void SetPosition(float _x, float _y){
		x = _x;
		y = _y;
	}
	
	public void SetNumTableau(int _num){
		numTableau = _num;
	}
	
    void OnTriggerEnter2D(Collider2D col) {
		//Si le téléporteur entre en collision avec le joueur (objet avec le tag "Player")
        if (col.gameObject.tag == "Player") {
			//On modifie la position de l'objet touché (le joueur) et on le téléporte à x et y
			col.gameObject.GetComponent<PlayerManager>().Teleport(x,y,numTableau); //On récupère le PlayerManager du joueur pour appeler la fonction de téléportation
			
			//On fait apparaître le nouveau tableau selon le numéro entré (numTableau).
			TableauManager.ShowTableau(numTableau);
			
			//Les nouvelles coordonnées du checkpoint sont celles où le joueur vient d'être téléporté
			TableauManager.UpdateCheckpointPosition(x,y);
        }
    }
}
