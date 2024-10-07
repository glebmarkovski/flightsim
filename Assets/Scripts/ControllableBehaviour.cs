using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableBehaviour : MonoBehaviour
{
	protected VehicleInput input;
	public virtual void SetInput(VehicleInput newInput){
		input = newInput;
	}
}
