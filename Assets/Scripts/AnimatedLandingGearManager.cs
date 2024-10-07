using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedLandingGearManager : ControllableBehaviour
{
	private AnimatedLandingGear[] gears;

	private bool extended;

	public bool startExtended = true;

	private void Awake(){
		gears = GetComponentsInChildren<AnimatedLandingGear>();
		foreach (AnimatedLandingGear gear in gears){
			gear.startExtended = startExtended;
		}
		extended = startExtended;
	}

	private void Update(){
		if(!extended && input.GetButton("Gear")){
			foreach(AnimatedLandingGear gear in gears){
				gear.Extend();
			}
			extended = true;
		}
		if(extended && !input.GetButton("Gear")){
			foreach(AnimatedLandingGear gear in gears){
				gear.Retract();
			}
			extended = false;
		}
	}


}
