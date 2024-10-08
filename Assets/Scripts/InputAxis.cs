using UnityEngine;
using System;
[Serializable]
public class InputAxis{
	public string name;

	public float max;
	public float min;
	public float sensitivity;
	public float trim;
	public float trimSensitivity;
	public bool sticky;

	public string pos;
	public string neg;
	public string trimPos;
	public string trimNeg;

	private float val;

	public float GetValue(){
		return val; 
	}

	public float GetTrim(){
		return trim; 
	}

	public void UpdateValue(bool active){
		if(active){
			if(Input.GetKey(trimPos)){
				trim = Mathf.Clamp(trim + trimSensitivity * Time.deltaTime, min, max);
			}
			if(Input.GetKey(trimNeg)){
				trim = Mathf.Clamp(trim - trimSensitivity * Time.deltaTime, min, max);
			}
		}
		float desiredValue;
		if (active && Input.GetKey(pos)){
			desiredValue = max;
		}
		else if(active && Input.GetKey(neg)){
			desiredValue = min;
		}
		else if(sticky){
			desiredValue = val;
		}
		else{
			desiredValue = trim;
		}
		val = Mathf.Clamp(val + Mathf.Clamp(desiredValue - val, -sensitivity * Time.deltaTime, sensitivity * Time.deltaTime), min, max);
	}
}
