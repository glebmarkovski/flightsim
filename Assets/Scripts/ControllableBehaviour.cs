using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ControllableBehaviour : NetworkBehaviour
{
	protected PlayerInput input;
	public virtual void SetInput(PlayerInput newInput){
		input = newInput;
	}
}
