using UnityEngine;
using System;
[Serializable]
public class VehicleInput{
	[SerializeField]
	private InputAxis[] axes;
	[SerializeField]
	private InputButton[] buttons;

	bool active;

	public void SetActive(bool state){
		active = state;
	}

	public float GetAxis(string name){
		foreach (InputAxis axis in axes){
			if (axis.name == name){
				return axis.GetValue();
			}
		}
		return 0;
	}

	public void UpdateAxes(){
		foreach (InputAxis axis in axes){
			axis.UpdateValue(active);
		}
	}

	public void UpdateButtons(){
		foreach (InputButton button in buttons){
			button.UpdateValue(active);
		}
	}

	public bool GetButtonDown(string name){
		foreach (InputButton button in buttons){
			if(button.name == name){
				return button.ValueDown();
			}
		}
		return false;
	}

	public bool GetButton(string name){
		foreach (InputButton button in buttons){
			if(button.name == name){
				return button.Value();
			}
		}
		return false;
	}
}
