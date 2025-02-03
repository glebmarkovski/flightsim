using TMPro;
using UnityEngine;

public class TelemetryDisplay : MonoBehaviour
{
    private OnlineUIManager manager;
    [SerializeField]
    private string axisName;
    private TMP_Text text;
    [SerializeField]
    private string units;
    [SerializeField]
    private string format;

    private void Awake()
    {
        manager = GetComponentInParent<OnlineUIManager>();
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(manager.GetTelemetry() == null)
        {
            return;
        }
        text.text = manager.GetTelemetry().GetValue(axisName).ToString(format) + " " + units;
    }
}
