public class GasFlow{
	public float velocity;
	public float pressure;
	public float temperature;
	public float mfr;

	public GasFlow(float newPressure, float newTemperature, float newVelocity, float newMfr = 0){
		velocity = newVelocity;
		pressure = newPressure;
		temperature = newTemperature;
		mfr = newMfr;
	}
}
