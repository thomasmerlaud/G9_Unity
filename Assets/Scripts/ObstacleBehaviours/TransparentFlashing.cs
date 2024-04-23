using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentFlashing : MonoBehaviour {

	[SerializeField] private float initialDelay = 0f; // A partir de combien de secondes le clignotement démarre
	[SerializeField] private float goToOpaque = 1f; // Combien de temps dure la transition de transparent à opaque
	[SerializeField] private float opaque = 1f; // Combien de temps il reste opaque
	[SerializeField] private float goToTransparent = 1f; // Combien de temps dure la transition d'opaque à transparent
	[SerializeField] private float transparent = 1f; // Combien de temps il reste transparent
	[SerializeField] private bool startOpaque = true; // Est-ce qu'il commence opaque ?
	// Start is called before the first frame update

	private SpriteRenderer _sprite;
	private int _step = 0; //0 opaque, 1 goToTransparent, 2 transparent, 3 goToOpaque
	private float _timer = 0;

	void Start() {
		_sprite = GetComponent<SpriteRenderer>(); //On récupère le sprite
		_timer = opaque; //On commence le timer à la valeur qu'il dure opaque
		if(!startOpaque){ //S'il ne commence pas opaque
			_sprite.color = new Color (_sprite.color.r, _sprite.color.g, _sprite.color.b, 0); //On met le sprite en transparent (canal RGBA ou A = alpha = transparence (entre 0 et 1)
			_step = 2; //on est à l'étape 2 (transparent)
			_timer = transparent; //On commence le timer à la valeur qu'il dure transparent
		}
	}

	void FixedUpdate() {
		//S'il y a un délai avant qu'il commence à bouger, on attend que ce délai finisse (on enlève Time.fixedDeltaTime, le laps de temps écoulé entre chaque frame, à la variable initialDelay, jusqu'à ce qu'elle atteigne 0)
		if(initialDelay > 0){
			initialDelay = Mathf.Max(0, initialDelay - Time.fixedDeltaTime); //La fonction max avec 0 évite que la variable soit inférieure à 0.
		//Sinon, si c'est le timer qui n'est pas fini, on enlève Time.fixedDeltaTime à chaque frame jusqu'à ce qu'il arrive à 0
		} else if(_timer > 0){
			_timer = Mathf.Max(0, _timer - Time.fixedDeltaTime);
			if(_step == 1){ //Si on est sur l'étape 1, transition d'opaque à transparent, la transparence du sprite est un nombre décimal égal au ratio de la valeur du timer / la valeur de durée de la transition
				_sprite.color = new Color (_sprite.color.r, _sprite.color.g, _sprite.color.b, _timer / goToTransparent);
			} else if (_step == 3){ //Si on est sur l'étape 3, transition de transparent à opaque, la transparence du sprite est un nombre décimal égal à 1 - le ratio de la valeur du timer / la valeur de durée de la transition
				_sprite.color = new Color (_sprite.color.r, _sprite.color.g, _sprite.color.b, 1 - (_timer / goToOpaque));
			}
		} else { // Si le timer vaut 0 (a fini de s'écouler), on passe à l'étape suivante (revient à 0 si on dépasse 4)
			_step = (_step+1)%4;
			_timer = returnTimeStep(); //(la fonction va récupérer quelle est la prochaine valeur à charger dans le timer)
		}
	}

	//Fonction qui retourne quelle est la valeur du timer à charger selon l'étape en cours
	private float returnTimeStep(){
		if(_step == 0){
			return opaque;
		} else if (_step == 1){
			return goToTransparent;
		} else if (_step == 2){
			return transparent;
		} else {
			return goToOpaque;
		}
	}
}
