using UnityEngine;
using TMPro;

public class AircraftJetEngineDebug : MonoBehaviour
{
	public float thrust;
	public float density;
	public float mach;
	public float alpha;
	public float throttle;

	private TMP_Text values;
	private LineRenderer forceLine;
    private void Awake()
    {
        values = GetComponentInChildren<TMP_Text>();
        forceLine = GetComponentInChildren<LineRenderer>();
    }

    public void Update(){
		values.text = "Thrust: " + thrust.ToString() + "\nThrottle: " + throttle.ToString() + "\nDensity: " + density.ToString() + "\nAlpha: " + alpha.ToString() + "\nMach: " + mach.ToString();
        Vector3[] verteces = { transform.position, transform.position + transform.forward * thrust * 0.00005f };
        forceLine.SetPositions(verteces);
    }
}
