using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveLoopInit : MonoBehaviour {

	[SerializeField] private float yDelay = 0;
	[SerializeField] private float speed = 1f;
	[SerializeField] private int ySens = 1;

	// A mettre sur un objet contenant plusieurs cercles en vertical move loop, permet de régler le décalage avec la variable yDelay ainsi que le sens et la vitesse de tous les objets
	void Start() {
		foreach(Transform child in gameObject.transform) {
			child.gameObject.GetComponent<VerticalMoveLoop>().initParameters(yDelay, speed, ySens);
		}
	}
}
