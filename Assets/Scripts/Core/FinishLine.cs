using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D col) {
		//Si la ligne de fin rentre en collision avec le joueur (objet avec le tag "Player")
        if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerManager>().FinishLine();
			//On arrête le chronomètre

			//On immobilise le joueur pendant 5 s
			PlayerManager.SetFreeze(5f);
			StartCoroutine(EndGame()); //On lance une coroutine, la fonction qui permet d'attendre 5 secondes puis de revenir à l'écran titre
        }
    }
	
	//Permet d'attendre 5 secondes puis de revenir à l'écran titre
	private IEnumerator EndGame()
	{
		yield return new WaitForSeconds( 5.0f );
		Debug.Log("Fin");
		SceneManager.LoadScene("TitleMenu");
	}
}
