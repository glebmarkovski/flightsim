using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
	public Airfoil airfoil;
	private void Start(){
		for(float alpha = -Mathf.PI; alpha <= Mathf.PI; alpha += Mathf.PI/90){
			Debug.Log("Alpha: " + alpha + " Cl: " + airfoil.GetCl(alpha, 0) + " Cd: " + airfoil.GetCd(alpha, 0));
		}
	}
}
