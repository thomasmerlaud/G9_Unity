using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau : MonoBehaviour
{
	
	[SerializeField] private GameObject teleporterLeft = null; //Contient le téléporteur de gauche
	[SerializeField] private GameObject teleporterRight = null; //Contient le téléporteur de droite
	
	public GameObject GetTeleporterLeft(){
		return teleporterLeft;
	}
	
	public GameObject GetTeleporterRight(){
		return teleporterRight;
	}
}
