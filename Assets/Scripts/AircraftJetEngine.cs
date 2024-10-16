using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftJetEngine : MonoBehaviour
{
	private Rigidbody rb;
	[SerializeField]
	private float takeOffThrust;
	[SerializeField]
	private TankPriorityLevel[] fuel;

	[SerializeField]
	private JetEngineFan fan;

	[SerializeField]
	private JetEngineExhaust fanExhaust;

	[SerializeField]
	private JetEngineCompressor compressor1;

	[SerializeField]
	private JetEngineCompressor compressor2;

	[SerializeField]
	private JetEngineCombustor combustor;

	[SerializeField]
	private JetEngineTurbine turbine2;

	[SerializeField]
	private JetEngineTurbine turbine1;

	[SerializeField]
	private JetEngineExhaust turbineExhaust;

	[SerializeField]
	private float n1moi;

	[SerializeField]
	private float n2moi;

	private float n1;
	private float n2;

	private void Awake(){
		rb = GetComponentInParent<Rigidbody>();
	}

	private void FixedUpdate(){
		float airspeed = Vector3.Dot(rb.velocity, transform.forward);
		JetEngineGasFlow fanGas = fan.Gas(Air.Pressure(), Air.Temperature(), airspeed, n1);
		JetEngineGasFlow
		JetEngineGasFlow compressor1Gas = compressor1.Gas(intakeGas, n1);
		JetEngineGasFlow compressor2Gas = compressor2.Gas(compressor1Gas, n2);
		JetEngineGasFlow combustorGas = combustor.Gas(compressor2Gas);
		JetEngineGasFlow turbine2Gas = turbine2.Gas(combustorGas, n2);
		JetEngineGasFlow turbine1Gas = turbine1.Gas(turbine2Gas, n1);
		JetEngineGasFlow turbineExhaustGas = turbineExhaust.Gas(turbine1Gas, atmosphericGas);

		thrust = exhaustGas.velocity * exhaustGas.mfr + fanGas.velocity * fanGas.mfr - (intakeGas.pressure - atmosphericGas.pressure) * intake.area; 

		rb.AddForceAtPosition(thrust * transform.Forward, transform.position);

		float n1torque = fan.Torque(intakeGas, n1) + compressor1.Torque(intakeGas, n1) + turbine1.Torque(turbine2Gas, n1);
		n1 += n1torque/n1moi;
		float n2torque = compressor2.Torque(compressor1Gas, n2) + turbine2.Torque(combustorGas, n2);
		n2 += n2torque/n2moi;
	}

	private bool GetFuel(float volume){
		foreach(TankPriorityLevel fuelLevel in fuel){
			if(fuelLevel.GetFuel(volume)) {
				return true;
			}
		}
		return false;
	}
}
