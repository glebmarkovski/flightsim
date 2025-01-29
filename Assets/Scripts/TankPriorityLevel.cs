using System;
using UnityEngine;
[Serializable]
public class TankPriorityLevel{
	[SerializeField]
	public AircraftFuelTank[] tanks;

	public bool GetFuel(float volume){
		int total = tanks.Length;
		while (volume > 0.1f && total > 0){
			int newTotal = total;
			foreach (AircraftFuelTank tank in tanks){
				if(tank.GetFuel(volume / total)){
						volume -= volume / newTotal;
				}
				else{
					total --;
				}
			}
		}
		if(volume <= 0.1f){
			return true;
		}
		return false;
	}
}
