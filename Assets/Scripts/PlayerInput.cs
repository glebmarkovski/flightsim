using UnityEngine;
using System;
[Serializable]
public class PlayerInput {
	[SerializeField]
	private InputAxis[] axes;
	[SerializeField]
	private InputButton[] buttons;
	private Vector2 view;
	[SerializeField]
    private Vector2 viewSensitivity;
	float zoom;
	[SerializeField]
	float zoomSensitivity;

	public PlayerInput()
	{
		axes = new InputAxis[0];
		buttons = new InputButton[0];
		view = new Vector2();
		zoom = 0;
	}

    public void SetView(Vector2 view)
	{
		this.view = view;
	}

	public Vector2 GetView()
	{
		return view; 
	}

	public void ResetAxes()
	{
        for (int i = 0; i < axes.Length; i++)
        {
			axes[i].ResetValue();
        }
    }

    public void ResetButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].ResetValue();
        }
    }

    public void UpdateView()
	{
        view += new Vector2(Input.GetAxis("Mouse X") * viewSensitivity.x, Input.GetAxis("Mouse Y") * viewSensitivity.y);
        view.y = Mathf.Clamp(view.y, -90, 90);
    }

	public virtual float GetAxis(string name) {
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

    public virtual float GetTrim(string name)
	{
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

	public virtual bool GetButtonDown(string name){
		foreach (InputButton button in buttons){
			if(button.name == name){
				return button.ValueDown();
			}
		}
		return false;
	}

	public virtual bool GetButton(string name){
		foreach (InputButton button in buttons){
			if(button.name == name){
				return button.GetValue();
			}
		}
		return false;
	}

	public virtual float GetZoom()
	{
		return zoom;
	}

	public void UpdateZoom()
	{
		zoom = Mathf.Clamp01(zoom + Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity);
	}

	public void SetZoom(float value) {
		zoom = value;
	}
}
