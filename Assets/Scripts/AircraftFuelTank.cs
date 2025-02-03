using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftFuelTank : NetworkBehaviour
{
	[SerializeField]
	private float capacity;
	[SyncVar]
	private float content;
	[SerializeField]
	private float density;

    private void Start()
    {
		if (isServer)
		{
			content = capacity;
		}
    }

    [Server]
	public bool GetFuel(float volume){
		if (content > volume){
			content -= volume;
			return true;
		}
		else{
			return false;
		}
	}

	[Server]
	public float GetMass(){
		return content * density;
	}
}
