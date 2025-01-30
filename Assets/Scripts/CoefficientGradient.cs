using UnityEngine;
using System;

[Serializable]
public class CoefficientGradient {
	[SerializeField]
	private CoefficientPoint[] points;

	public float x;

	public CoefficientGradient(CoefficientPoint[] newCps){
		points = newCps;
	}

	public float GetZ(float y){
		int len = points.Length;
		if (y < points[0].y){
			return Mathf.Lerp(points[len-1].z, points[0].z, (y - (points[len-1].y-Mathf.PI * 2))/(points[0].y - (points[len-1].y - Mathf.PI * 2)));
		}
		if (y >= points[0].y && y < points[len-1].y){
			for (int i = 0; i < len - 1; i++){
				if (y > points[i+1].y){
					continue;
				}
				if (y >= points[i].y && y < points[i+1].y){
					return Mathf.Lerp(points[i].z, points[i+1].z, (y-points[i].y)/(points[i+1].y-points[i].y));
				}
			}
		}
		if(y >= points[len-1].y){
			return Mathf.Lerp(points[len-1].z, points[0].z, (y - points[len - 1].y) / (points[0].y + Mathf.PI * 2 - points[len-1].y));
		}
		Debug.LogWarning("Didn't return a proper Coefficient");
		return 0;
	}
}
