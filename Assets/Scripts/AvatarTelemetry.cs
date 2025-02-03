using Mirror;
using UnityEngine;

public class AvatarTelemetry : Telemetry
{
    private Vector3 previousPosition;
    [SyncVar]
    private float speed;

    public override string GetHudMode()
    {
        return "First Person";
    }

    private void Start()
    {
        previousPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (isServer)
        {
            speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
            previousPosition = transform.position;
        }
    }
    public override float GetValue(string name) => name switch
    {
        "Speed" => speed,
        _ => 0f
    };
}
