using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

	//Singleton, permet de n'avoir qu'une seule et unique instance de l'objet

	public static PlayerManager instance;
	public static GameObject player;
	
	void Awake(){
		if(instance == null){
			instance = this;
		}
	}
	
	public static GameObject GetPlayer(){
		if(player == null){
			player = GameObject.FindWithTag("Player");
		}
		return player;
	}
	
	private static float freeze = 0; //Si cette valeur est supérieure à 0, le personnage ne peut pas bouger pendant le laps de temps indiqué en secondes.

	public static void SetFreeze(float val){
		freeze = val;
	}
	
	
	[SerializeField] private HUD hud; //on joint le hud du canvas
	[SerializeField] private AudioManager audioManager; 
	
	//Variables attributs  du joueur.
	
	private int nbDeath = 0; //Enregistre le nombre de morts.
	private float timerGame = 0;
	private bool endTimer = false;
	
	/* [ADDED] */
	public bool hasKey = false;
	
	public void AddKey(){
		hasKey = true;
	}
	
	public void UseKey(){
		hasKey = false;
	}
	/* [ADDED] */
	
	//Ajoute 1 au compteur de morts
	public void AddDeath(){
		nbDeath++;
		if(hud != null){ //On édite le HUD
			hud.updateDeathText(nbDeath);
		}
		audioManager.PlaySFX(audioManager.damageSFX); //Joue le bruitage de dégâts
	}

	//On récupère le nombre de morts
	public int GetNbDeath(){
		return nbDeath;
	}
	
	//On récupère le nombre de morts
	public void Teleport(float _x, float _y, int _numTableau){
		transform.position = new Vector2(_x,_y);
		if(hud != null){ //On édite le HUD
			hud.updateLevelText(_numTableau);
		}
	}
	
	public void FinishLine(){
		audioManager.PlaySFX(audioManager.finishSFX); //Joue le bruitage de fanfare de fin
		audioManager.StopMusic(); //On arrête la musique
		StopTimer();
	}
	
	public void StopTimer(){
		endTimer = true;
	}
	
	[SerializeField] private float _moveSpeed = 5f; //On définit ici la vitesse du character. Vous pouvez la modifier. 5f = le nombre 5 en float (décimal).
	[SerializeField] private Rigidbody2D _rb; //On place ici le rigidbody du character
	private Vector2 _movement;
	

    // Fonction qui se lance à chaque frame.
    void Update() {
		
		
    }

	void FixedUpdate() {
		//On récupère si les touches de directions horizontales et verticales sont pressées, cela donne un nombre entre 0 (pas pressé) et 1 (pressé).
        _movement.x = Input.GetAxisRaw("Horizontal");
		_movement.y = Input.GetAxisRaw("Vertical");

		//Si la valeur récupérée est supérieure à 0, ça veut dire que la touche est pressée.
		bool isMovingHorizontal = Mathf.Abs(_movement.x) > 0;
		bool isMovingVertical = Mathf.Abs(_movement.y) > 0;

		//On évite que le joueur bouge horizontalement ET verticalement.
		if (Mathf.Abs(_movement.x) > 0)
        {
            isMovingHorizontal = true;
            isMovingVertical = false;
        }

		//S'il se déplace verticalement, la priorité est au déplacement vertical
        if (Mathf.Abs(_movement.y) > 0)
        {
            isMovingHorizontal = false;
            isMovingVertical = true;
        }

		//On définit le vecteur de mouvement en fonction des données précédentes.
        if (isMovingHorizontal)
        {
            _movement = Vector2.right * _movement.normalized.x;
        }
        else if (isMovingVertical)
        {
            _movement = Vector2.up * _movement.normalized.y;
        }
		
		//Si le chronomètre n'est pas arrêté, on ajoute le laps de temps écoulé au chronomètre et on actualise le HUD
		if(!endTimer){
			timerGame += Time.fixedDeltaTime;
			if(hud != null){ //On édite le HUD
				hud.updateTimer(timerGame);
			}
		}
		
		//Si le personnage est gelé, (si la variable freeze est supérieure à 0), on diminue la variable freeze du laps de temps écoulé, mesuré par Time.fixedDeltaTime).
		if(freeze > 0){
			freeze = Mathf.Max(0, freeze - Time.fixedDeltaTime);
		}
		//Si freeze vaut 0, le personnage n'est pas gelé. On le déplace via son rigidbody d'une valeur égale à sa position + le vecteur mouvement défini dans Update * la vitesse _moveSpeed * le laps de temps écoulé Time.fixedDeltaTime)
		else
		{
			_rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
		}
		
		QuitGame();

	}
	
	//Fonction qui ferme le jeu (seulement lorsque le jeu est build)
	public void QuitGame()
	{
		//Si on appuie sur la touche Echap, ça ferme le jeu
		if (Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log("quit game");
			Application.Quit();
		}
	}
}
