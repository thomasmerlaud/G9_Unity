using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject _picturesGroupRules; //On charge ici le groupe objet qui contient toutes les images des Règles
	public GameObject _picturesGroupCredits; //On charge ici le groupe objet qui contient toutes les images des Crédits
	
	
	
	private int _readingRules = 0; //numéro de l'image des règles qu'on est en train de lire (0 si non activé)
	private int _readingCredits = 0; //numéro de l'image des crédits qu'on est en train de lire (0 si non activé)
	
	// Ici, notre programme stockera toutes les images des règles du jeu (il peut n'y en avoir qu'une)
	private GameObject[] _picturesRules;
	
	// Ici, notre programme stockera  toutes les images des crédits du jeu (il peut n'y en avoir qu'une)
	private GameObject[] _picturesCredits;
	
	
	
	// Fonction qui se lance au démarrage. Met toutes les images en désactivées pour qu'elles ne s'affichent pas de suite.
	void Start()
	{
		//On crée une variable qui compte combien d'objets enfants possède le groupe qui contient les images des règles.
		int children_group_rules = _picturesGroupRules.transform.childCount;
		
		//On initialise le tableau de l'attribut _picturesRules avec la taille du nombre d'images
		_picturesRules = new GameObject[children_group_rules];
		
		//On parcourt tous les enfants du groupe objet avec une boucle, on les insère dans le tableau, et on les désactive
		for (int i = 0; i < children_group_rules; ++i){
			_picturesRules[i] = _picturesGroupRules.transform.GetChild(i).gameObject;
			_picturesRules[i].SetActive(false);
		}
		
		//On désactive le groupe qui contient les images
		_picturesGroupRules.SetActive(false);
		

		//On fait la même chose pour les crédits
		int children_group_credits = _picturesGroupCredits.transform.childCount;
		_picturesCredits = new GameObject[children_group_credits];
		
		for (int i = 0; i < children_group_credits; ++i){
			_picturesCredits[i] = _picturesGroupCredits.transform.GetChild(i).gameObject;
			_picturesCredits[i].SetActive(false);
		}
		
		_picturesGroupCredits.SetActive(false);
	}

	// Fonction qui se lance à chaque frame.
	void Update()
	{
		
		//Si on est en train de lire les règles
		if (_readingRules > 0)
		{
			//Si on appuie sur le clic de la souris (possibilité de remplacer par l'appui d'une touche)
			if (Input.GetMouseButtonDown(0))
			{
				//Si le numéro de l'image qu'on est en train de lire est inférieur au nombre total d'images (si on n'est pas à la dernière)
				if (_readingRules < _picturesRules.Length)
				{
					Debug.Log(_readingRules + " " + _picturesRules.Length);
					
					//On désactive l'image actuelle
					_picturesRules[_readingRules-1].SetActive(false);
					
					//On augmente de 1 le compteur d'image de _readingRules
					_readingRules++;
					
					//On active l'image suivante
					_picturesRules[_readingRules-1].SetActive(true);
				}
				else
				{
					Debug.Log("end " + _readingRules + " " + _picturesRules.Length);
					
					//On désactive la dernière image
					_picturesRules[_readingRules-1].SetActive(false);
					_picturesGroupRules.SetActive(false);
					
					//On met le compteur à zéro
					_readingRules = 0;
				}
			}
		}
		
		/* FAIRE LA MEME CHOSE POUR LES CREDITS */
	}
	
	
	
	/*** FONCTIONS A ASSOCIER AU CLIC D'UN BOUTON DU MENU ***/
	
	//Fonction qui lance le jeu
	public void PlayGame()
	{
		//Mettre entre guillemets le nom de la scène vers laquelle charger
		//Pour utiliser SceneManager, il faut impérativement rajouter "using UnityEngine.SceneManagement;" en haut du script.
		SceneManager.LoadScene("SceneTest");
	}

	//Fonction qui affiche la première image des règles
	public void ReadRules()
	{
		Debug.Log("Règles"); //Une simple fonction pour voir lors du développement si le clic marche bien, affiche "Règles" dans la console. Peut être retiré, mais utile pour débuguer.
		_picturesGroupRules.SetActive(true);
		_picturesRules[0].SetActive(true);
		_readingRules = 1;
		
	}
	
	//Fonction qui affiche la première image des crédits
	public void ReadCredits()
	{
		_picturesGroupCredits.SetActive(true);
		_picturesCredits[0].SetActive(true);
		_readingCredits = 1;
	}

	//Fonction qui ferme le jeu
	public void QuitGame()
	{
		Application.Quit();
	}
}
