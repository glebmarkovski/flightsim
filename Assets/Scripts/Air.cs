using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
	public static Air air;

	public float surfaceTemp;
	public float tropopause;

	private void Awake(){
		Air.air = this;
	}

	public float Pressure(float altitude){
		return 101325*Mathf.Pow(1-0.0000225577f*altitude, 5.25588f);
	}

	public float Temperature(float altitude){
		if (altitude < 0) {
			return surfaceTemp;	
		}
		else if (altitude < tropopause){
			return Mathf.Lerp(surfaceTemp, 223.15f, altitude/tropopause);
		}
		else{
			return Mathf.Lerp(223.15f, 273.15f, (altitude-tropopause)/(50000-tropopause));
		}
	}

	public float Density(float altitude){
		return 0.02896f*Pressure(altitude)/8.314f/Temperature(altitude);
	}

	public float Viscosity(float altitude){
		return 0.00001789f * Mathf.Pow(Temperature(altitude)/288.15f, 0.7f);
	}

	public float Mach(float altitude){
		return Mathf.Sqrt(1.4f * 8.314f * Temperature(altitude) / 0.02896f);
	}

	public float TasToIas(float altitude,float TAS)
	{
		return Mach(0) * Mathf.Sqrt(5 * (Mathf.Pow(0.5f * Density(altitude) * TAS * TAS / Pressure(0) + 1, 0.28571428571f) - 1));
	}
}
