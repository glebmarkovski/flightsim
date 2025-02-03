using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftTelemetry : Telemetry
{
    private Rigidbody rb;
    [SyncVar]
    private float ias;
    [SyncVar]
    private float tas;
    [SyncVar]
    private float asl;
    [SyncVar]
    private float climb;

    public override string GetHudMode()
    {
        return "Aircraft";
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isServer)
        {
            ias = Air.air.TasToIas(transform.position.y, rb.velocity.magnitude);
            tas = rb.velocity.magnitude;
            asl = transform.position.y;
            climb = rb.velocity.y;
        }
    }

    public override float GetValue(string name) => name switch
    {
        "IAS" => ias,
        "TAS" => tas,
        "ASL" => asl,
        "Climb" => climb,
        _ => 0f,
    };
}
