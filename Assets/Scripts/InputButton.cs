using UnityEngine;
using System;
[Serializable]
public class InputButton{
	public string name;
	public bool isToggle;
	private bool down;
	[SerializeField]
	private bool val;
	public string key;

	public bool Value(){
		return val;
	}

	public bool ValueDown(){
		return down;
	}

	public void UpdateValue(bool active){
		if(!active){
			if(isToggle){
				return;
			}
			else{
				val = false;
			}
		}
		else{
			down = Input.GetKeyDown(key);
			if(!isToggle){
				val = Input.GetKey(key);
			}
			if(isToggle & down){
				val = !val;
			}
		}
	}
}
