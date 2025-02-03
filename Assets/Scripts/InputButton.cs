using UnityEngine;
using System;
[Serializable]
public class InputButton{
	public string name;
	public bool isToggle;
	private bool down;
	[SerializeField]
	private bool value;
	public string key;

	public bool GetValue(){
		return value;
	}

	public void ResetValue()
	{
		if (isToggle)
		{
			return;
		}
		value = false;
		down = false;
	}

	public void SetValue(bool value)
	{
		down = value && !this.value;
		this.value = value;
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
				value = false;
			}
		}
		else{
			down = Input.GetKeyDown(key);
			if(!isToggle){
				value = Input.GetKey(key);
			}
			if(isToggle & down){
				value = !value;
			}
		}
	}
}
