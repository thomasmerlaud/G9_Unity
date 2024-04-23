using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauReinit : MonoBehaviour
{
	[SerializeField] private FollowingPlayerMove[] followingReinit;
	
	public void Reinit(){
		for(int i = 0; i < followingReinit.Length; i++){
			followingReinit[i].reinitPosition();
		}
	}
}
