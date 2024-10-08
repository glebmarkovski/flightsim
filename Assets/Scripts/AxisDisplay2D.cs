using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisDisplay2D : ControllableBehaviour
{
	public string xAxis;
	public string yAxis;
	public RectTransform main;
	public RectTransform trim;
	public Vector2 rangeOfMotion;

	private void Awake(){
		main = transform.GetChild(0).GetComponent<RectTransform>();
		trim = transform.GetChild(1).GetComponent<RectTransform>();
	}

	private void Update(){
		main.anchoredPosition = new Vector2(input.GetAxis(xAxis)*rangeOfMotion.x, input.GetAxis(yAxis)*rangeOfMotion.y);
		trim.anchoredPosition = new Vector2(input.GetTrim(xAxis)*rangeOfMotion.x, input.GetTrim(yAxis)*rangeOfMotion.y);
	}
}
