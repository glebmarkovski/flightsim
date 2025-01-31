using UnityEngine;
using System;
[Serializable]
public class PlayerInput {
	[SerializeField]
	private InputAxis[] axes;
	[SerializeField]
	private InputButton[] buttons;
	private Vector2 view;
    public Vector2 viewSensitivity;

    public void SetView(Vector2 view)
	{
		this.view = view;
	}

	public Vector2 GetView()
	{
		return view; 
	}

	public void UpdateView()
	{
        view += new Vector2(Input.GetAxis("Mouse X") * viewSensitivity.x, Input.GetAxis("Mouse Y") * viewSensitivity.y);
        view.y = Mathf.Clamp(view.y, -90, 90);
    }

	public float GetAxis(string name) {
		foreach (InputAxis axis in axes) {
			if (axis.name == name) {
				return axis.GetValue();
			}
		}
		return 0;
	}

	public float[] GetAxes(){
		float[] values = new float[axes.Length];
		for (int i = 0; i < axes.Length; i++)
		{
			values[i] = axes[i].GetValue();
		}
		return values;
	}

	public void SetAxes(float[] values)
	{
		for (int i = 0; i < axes.Length; i++)
		{
			axes[i].SetValue(values[i]);
		}
	}

	public bool[] GetButtons()
	{
        bool[] values = new bool[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            values[i] = buttons[i].GetValue();
        }
        return values;
    }

	public void SetButtons(bool[] values)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetValue(values[i]);
		}
	}

        public float GetTrim(string name){
		foreach (InputAxis axis in axes){
			if (axis.name == name){
				return axis.GetTrim();
			}
		}
		return 0;
	}

	public void UpdateAxes(){
		foreach (InputAxis axis in axes){
			axis.UpdateValue(true);
		}
	}

	public void UpdateButtons(){
		foreach (InputButton button in buttons){
			button.UpdateValue(true);
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
				return button.GetValue();
			}
		}
		return false;
	}
}
