using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
	//Si lors d'un niveau, il faut réinitialiser la position de certains obstacles : mettre un tableauReinit à votre tableau et mettre la liste des FollowingPlayerMove à réinitialiser
	//Mettre le tableauReinit dans tous les objets avec un impact dans le tableau
	[SerializeField] private TableauReinit tableauReinit = null;
	
	//Si un objet rentre en collision avec l'obstacle
    void OnTriggerEnter2D(Collider2D col) {
		//Si l'obstacle entre en collision avec le joueur (objet avec le tag "Player")
        if (col.gameObject.tag == "Player") {
			
			//S'il faut réinitialiser des obstacles lorsqu'on perd, uniquement s'il y a un tableauReinit
			if(tableauReinit != null){
				tableauReinit.Reinit();
			}
			
			//On change la position du joueur et on le téléporte aux coordonnées sauvegardées dans le TableauManager dans la variable checkpointPosition.
            col.gameObject.transform.position = TableauManager.GetCheckpointPosition();
			
			//On augmente de 1 le compteur de morts
			col.gameObject.GetComponent<PlayerManager>().AddDeath(); //On récupère le PlayerManager du joueur pour ajouter la mort
			
			//On immobilise le joueur pendant 0.5 s
			PlayerManager.SetFreeze(0.5f);
        }
    }
}
