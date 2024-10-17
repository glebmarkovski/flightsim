using UnityEngine;
using System;
[Serializable]
public class JetEngineFan{

	[SerializeField]
	private float intakeArea;

	private float finalArea;

	private float toPressureRatio;
	private float toRotation;
	private float toMfr;
	private float toAsymptoteIntakeSpeed;

	private float intakeSpeed;

	public JetEngineGasFlow Gas(float airspeed, float staticIntakePressure, float intakeDensity, float intakeTemperature, float rotation){ 
		float dynamicIntakePressure = (airspeed-intakeSpeed) * (airspeed-intakeSpeed) * density * 0.5f;
		float mfr = intakeSpeed * intakeArea * density;
		float asymptoteIntakeSpeed = toAsymptoteIntakeSpeed * rotation / toRotation;
		float pressureRatio = toPressureRatio * (asymptoteIntakeSpeed - intakeSpeed) / asymptoteIntakeSpeed;
		float 
	}
}
