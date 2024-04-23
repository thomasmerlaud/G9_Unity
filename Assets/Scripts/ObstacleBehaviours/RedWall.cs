using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWall : MonoBehaviour
{
	[SerializeField] private GameObject waypointBottom; //Définit le bord bas de la zone limite via un point objet
	[SerializeField] private GameObject waypointTop; //Définit le bord haut de la zone limite via un point objet
	
	[SerializeField] private float delay = 0;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float ecart = 4f;
	
	[SerializeField] private float xSens = 0f;
	[SerializeField] private float ySens = 1f;
	private float step = 0;
	private bool asc = true;

	void Awake(){
		foreach(Transform child in gameObject.transform) {
			child.gameObject.GetComponent<BoundMove>().initParameters(new Vector2(0, step), new Vector2(xSens, ySens), speed, waypointBottom, waypointTop);
			if(asc){
				step += delay;
				if(step > ecart){
					asc = false;
					step -= delay*2;
				}
			} else {
				step -= delay;
				if(step < 0){
					asc = true;
					step += delay*2;
				}
			}
		}
	}
}
