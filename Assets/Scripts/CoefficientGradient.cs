using UnityEngine;
using System;

[Serializable]
public class CoefficientGradient {
	[SerializeField]
	private CoefficientPoint[] cps;

	public CoefficientGradient(CoefficientPoint[] newCps){
		cps = newCps;
	}

	public float GetC(float value){
		int len = cps.Length;
		if (value < cps[0].value){
			return Mathf.Lerp(cps[len-1].c, cps[0].c, (value - (cps[len-1].value-Mathf.PI * 2))/(cps[0].value - (cps[len-1].value - Mathf.PI * 2)));
		}
		if (value >= cps[0].value && value < cps[len-1].value){
			for (int i = 0; i < len - 1; i++){
				if (value > cps[i+1].value){
					continue;
				}
				if (value >= cps[i].value && value < cps[i+1].value){
					return Mathf.Lerp(cps[i].c, cps[i+1].c, (value-cps[i].value)/(cps[i+1].value-cps[i].value));
				}
			}
		}
		if(value >= cps[len-1].value){
			return Mathf.Lerp(cps[len-1].c, cps[0].c, (value - cps[len - 1].value) / (cps[0].value + Mathf.PI * 2 - cps[len-1].value));
		}
		Debug.LogWarning("Didn't return a proper Coefficient");
		return 0;
	}
}
