using UnityEngine;
using System;

[Serializable]
public class Airfoil
{
	public Airfoil(AlphaPoint[] newApsl, AlphaPoint[] newApsd){
		apsl = newApsl;
		apsd = newApsd;
	}

	public float GetCl(float alpha, float theta){
		int len = apsl.Length;
		if (alpha < apsl[0].alpha){
			return Mathf.Lerp(apsl[len-1].GetC(theta), apsl[0].GetC(theta), (alpha - (apsl[len-1].alpha-Mathf.PI * 2))/(apsl[0].alpha - (apsl[len-1].alpha - Mathf.PI * 2)));
		}
		if (alpha >= apsl[0].alpha && alpha < apsl[len-1].alpha){
			for (int i = 0; i < len - 1; i++){
				if (alpha > apsl[i+1].alpha){
					continue;
				}
				if (alpha >= apsl[i].alpha && alpha < apsl[i+1].alpha){
					return Mathf.Lerp(apsl[i].GetC(theta), apsl[i+1].GetC(theta), (alpha-apsl[i].alpha)/(apsl[i+1].alpha-apsl[i].alpha));
				}
			}
		}
		if(alpha >= apsl[len-1].alpha){
			return Mathf.Lerp(apsl[len-1].GetC(theta), apsl[0].GetC(theta), (alpha - apsl[len - 1].alpha) / (apsl[0].alpha + Mathf.PI * 2 - apsl[len-1].alpha));
		}
		Debug.LogWarning("Didn't return a proper Coefficient");
		return 0;
	}
	
	public float GetCd(float alpha, float theta){
		int len = apsd.Length;
		if (alpha < apsd[0].alpha){
			return Mathf.Lerp(apsd[len-1].GetC(theta), apsd[0].GetC(theta), (alpha - (apsd[len-1].alpha-Mathf.PI * 2))/(apsd[0].alpha - (apsd[len-1].alpha - Mathf.PI * 2)));
		}
		if (alpha >= apsd[0].alpha && alpha < apsd[len-1].alpha){
			for (int i = 0; i < len - 1; i++){
				if (alpha > apsd[i+1].alpha){
					continue;
				}
				if (alpha >= apsd[i].alpha && alpha < apsd[i+1].alpha){
					return Mathf.Lerp(apsd[i].GetC(theta), apsd[i+1].GetC(theta), (alpha-apsd[i].alpha)/(apsd[i+1].alpha-apsd[i].alpha));
				}
			}
		}
		if(alpha >= apsd[len-1].alpha){
			return Mathf.Lerp(apsd[len-1].GetC(theta), apsd[0].GetC(theta), (alpha - apsd[len - 1].alpha) / (apsd[0].alpha + Mathf.PI * 2 - apsd[len-1].alpha));
		}
		Debug.LogWarning("Didn't return a proper Coefficient");
		return 0;
	}

	public AlphaPoint[] apsl;
	public AlphaPoint[] apsd;
}
