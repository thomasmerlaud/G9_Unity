using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauManager : MonoBehaviour
{

	//Singleton, permet de n'avoir qu'une seule et unique instance de l'objet

	public static TableauManager instance;

	void Awake(){
		if(instance == null){
			instance = this;
		}
	}
	
	

	public static int maxHeight = 6; //hauteur des niveaux
	public static int maxWidth = 12; //largeur des niveaux

	[SerializeField] static GameObject[] tableaux; //Contient l'ensemble des tableaux de la scène
	
	[SerializeField] private int startTableau = 1; //Le premier tableau de démarrage
	public static int activeTableau; //Mémorise le numéro du tableau actuel
	public static int lastTableau = 0; //Mémorise le numéro du tableau final
	private static Vector2 checkpointPosition = new Vector2 (0f, 0f); //Mémorise à quel endroit téléporter le joueur s'il se fait toucher par un obstacle

	// Fonction qui se lance au démarrage. Prend tous les objets enfants du TableauManager et les range dans l'attribut tableaux.
	void Start() {
		activeTableau = startTableau;
		int children = transform.childCount;
		tableaux = new GameObject[children];
		lastTableau = children - 1;
		for (int i = 0; i < children; ++i){
			tableaux[i] = transform.GetChild(i).gameObject;
			
			//On fait en sorte que le téléporteur gauche du niveau redirige vers le niveau précédent (ne marche pas si une salle a plusieurs téléporteurs)
			GameObject teleporterLeft = tableaux[i].GetComponent<Tableau>().GetTeleporterLeft();
			if(teleporterLeft != null){
				teleporterLeft.GetComponent<Teleport>().SetNumTableau(i-1);
				teleporterLeft.GetComponent<Teleport>().SetPosition(7.5f,0);
			}
			//On fait en sorte que le téléporteur droite du niveau redirige vers le niveau suivant
			GameObject teleporterRight = tableaux[i].GetComponent<Tableau>().GetTeleporterRight();
			if(teleporterRight != null){
				teleporterRight.GetComponent<Teleport>().SetNumTableau(i+1);
				teleporterRight.GetComponent<Teleport>().SetPosition(-7.5f,0);
			}
			
			//Met tous les tableaux désactivés
			tableaux[i].SetActive(false);
		}
		//Active le tableau d'indince activeTableau (le niveau actuel)
		tableaux[activeTableau].SetActive(true);
	}

	//Fonction qui active le tableau d'indice index, active l'ancien tableau actuel, et met à jour la variable activeTableau
	public static void ShowTableau(int index){
		tableaux[index].SetActive(true);
		tableaux[activeTableau].SetActive(false);
		activeTableau = index;
	}

	//Fonction qui met à jour la position du checkpoint en fonction des paramètres x et y donnés
	public static void UpdateCheckpointPosition(float x, float y){
		checkpointPosition.x = x;
		checkpointPosition.y = y;
	}

	//Fonction qui retourne la position du checkpoint sauvegardée
	public static Vector2 GetCheckpointPosition(){
		return checkpointPosition;
	}
}
