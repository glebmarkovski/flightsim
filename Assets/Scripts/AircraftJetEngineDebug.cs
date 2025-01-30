using UnityEngine;
using TMPro;

public class AircraftJetEngineDebug : MonoBehaviour
{
	public float thrust;
	public float density;
	public float mach;
	public float alpha;
	public float throttle;
    public float n1;
    public float n2;
    public float n1Thrust;
    public float n2Thrust;

	private TMP_Text values;
	private LineRenderer forceLine;
    private void Awake()
    {
        values = GetComponentInChildren<TMP_Text>();
        forceLine = GetComponentInChildren<LineRenderer>();
    }

    public void Update(){
		values.text = "N1 Thrust: " + n1Thrust.ToString() + "\nN2 Thrust: " + n2Thrust.ToString() + "\nThrust: " + thrust.ToString() + "\nThrottle: " + throttle.ToString() + "\nDensity: " + density.ToString() + "\nAlpha: " + alpha.ToString() + "\nMach: " + mach.ToString() + "\nN1% " + n1.ToString() + "\nN2% " + n2.ToString();
        Vector3[] verteces = { transform.position, transform.position + transform.forward * thrust * 0.00005f };
        forceLine.SetPositions(verteces);
    }
}
