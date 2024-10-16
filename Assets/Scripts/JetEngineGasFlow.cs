public class JetEngineGasFlow{
	private float velocity;

	public float Velocity{ 
		get; 
	}

	private float area;

	public float Area{
		get;
	}

	private float mfr;

	public float Mfr{
		get;
	}

	private float pressure;

	public float Pressure{
		get;
	}

	private float temperature;

	public float Temperature{
		get;
	}

	const private float R = 8.314f;
	const private float M = 28.96f;

	public JetEngineGasFlow(float newVelocity, float newArea, float newMfr, float newPressure, float newTemperature){
		velocity = newVelocity;
		pressure = newPressure;
		mfr = newMfr;
		temperature = newTemperature;
		area = newArea;

		if(velocity == 0){
			velocity = (mfr * R * temperature) / (M * pressure * area);
		}
		if(area == 0){
			area = (mfr * R * temperature) / (M * pressure * velocity);
		}
		if(mfr == 0){
			mfr = (M * pressure * area * velocity) / (R * temperature);
		}
		if(pressure == 0){
			pressure = (mfr * R * Temperature) / (M * area * velocity);
		}
		if(temperature == 0){
			temperature = (pressure * area * velocity * M) / (mfr * R);
		}
	}

	public float Density(){
		return mfr / (area * velocity);
	}
}
