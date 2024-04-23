using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerAnimated : MonoBehaviour
{

	//Singleton, permet de n'avoir qu'une seule et unique instance de l'objet

	public static PlayerManagerAnimated instance;
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
	
	[SerializeField] private float moveSpeed = 5f; //On définit ici la vitesse du character. Vous pouvez la modifier. 5f = le nombre 5 en float (décimal).
	[SerializeField] private Rigidbody2D _rb; //On place ici le rigidbody du character
	private Vector2 movement;
	private Animator animator;
		
	void Start(){
		animator = GetComponent<Animator>(); //On charge l'animator de l'objet dans notre script
	}
	

    // Fonction qui se lance à chaque frame.
    void Update() {
		
		//On récupère si les touches de directions horizontales et verticales sont pressées, cela donne un nombre entre 0 (pas pressé) et 1 (pressé).
        movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		
		if(movement != Vector2.zero){ //Si le joueur bouge, on partage les variables à l'animator pour qu'il bouge le sprite en conséquence
			animator.SetFloat("moveX", movement.x);
			animator.SetFloat("moveY", movement.y);
			animator.SetBool("moving", true);
		} else {
			animator.SetBool("moving", false);
		}

		//Si la valeur récupérée est supérieure à 0, ça veut dire que la touche est pressée.
		bool isMovingHorizontal = Mathf.Abs(movement.x) > 0;
		bool isMovingVertical = Mathf.Abs(movement.y) > 0;

		//On évite que le joueur bouge horizontalement ET verticalement.
		if (Mathf.Abs(movement.x) > 0)
        {
            isMovingHorizontal = true;
            isMovingVertical = false;
        }

		//S'il se déplace verticalement, la priorité est au déplacement vertical
        if (Mathf.Abs(movement.y) > 0)
        {
            isMovingHorizontal = false;
            isMovingVertical = true;
        }

		//On définit le vecteur de mouvement en fonction des données précédentes.
        if (isMovingHorizontal)
        {
            movement = Vector2.right * movement.normalized.x;
        }
        else if (isMovingVertical)
        {
            movement = Vector2.up * movement.normalized.y;
        }
		
		QuitGame();
    }

	void FixedUpdate() {
		
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
		//Si freeze vaut 0, le personnage n'est pas gelé. On le déplace via son rigidbody d'une valeur égale à sa position + le vecteur mouvement défini dans Update * la vitesse moveSpeed * le laps de temps écoulé Time.fixedDeltaTime)
		else
		{
			_rb.MovePosition(_rb.position + movement * moveSpeed * Time.fixedDeltaTime);
		}

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
