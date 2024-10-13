using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftFuelTank : MonoBehaviour
{
	[SerializeField]
	private float capacity;
	[SerializeField]
	private float content;
	[SerializeField]
	private float density;

	public bool drainFuel(float volume){
		if (content > volume){
			content -= volume;
			return true;
		}
		else{
			return false;
		}
	}

	public float GetMass(){
		return content * density;
	}
}
