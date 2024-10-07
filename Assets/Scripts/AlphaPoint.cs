using UnityEngine;
using System;

[Serializable]
public class AlphaPoint {

	public float alpha;

	[SerializeField]
	private ThetaPoint[] tps;

	public AlphaPoint(ThetaPoint[] newTps){
		tps = newTps;
	}

	public float GetC(float theta){
		int len = tps.Length;
		if (theta < tps[0].theta){
			return Mathf.Lerp(tps[len-1].c, tps[0].c, (theta - (tps[len-1].theta-Mathf.PI * 2))/(tps[0].theta - (tps[len-1].theta - Mathf.PI * 2)));
		}
		if (theta >= tps[0].theta && theta < tps[len-1].theta){
			for (int i = 0; i < len - 1; i++){
				if (theta > tps[i+1].theta){
					continue;
				}
				if (theta >= tps[i].theta && theta < tps[i+1].theta){
					return Mathf.Lerp(tps[i].c, tps[i+1].c, (theta-tps[i].theta)/(tps[i+1].theta-tps[i].theta));
				}
			}
		}
		if(theta >= tps[len-1].theta){
			return Mathf.Lerp(tps[len-1].c, tps[0].c, (theta - tps[len - 1].theta) / (tps[0].theta + Mathf.PI * 2 - tps[len-1].theta));
		}
		Debug.LogWarning("Didn't return a proper Coefficient");
		return 0;
	}
}
