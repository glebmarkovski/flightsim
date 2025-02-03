using UnityEngine;
using Mirror;

public class AircraftRBManipulator : NetworkBehaviour
{
    private Rigidbody rb;
    private AircraftFuelTank[] tanks;
    [SerializeField]
    private float dryMass;
    [SerializeField]
    private Vector3 drycom;
    [SerializeField]
    private Vector3 dryInertiaTensor;
    [SyncVar]
    private float mass;
    [SyncVar]
    private Vector3 com;
    [SyncVar]
    private Vector3 inertiaTensor;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tanks = GetComponentsInChildren<AircraftFuelTank>();
    }

    private void Start()
    {
        if (isServer)
        {
            CalculateProperties();
        }
        SetProperties();
    }

    private void FixedUpdate()
    {
        if (isServer)
        {
            CalculateProperties();
        }
        SetProperties();
    }

    private void CalculateProperties()
    {
        mass = dryMass;
        foreach (AircraftFuelTank tank in tanks)
        {
            mass += tank.GetMass();
        }
        com = drycom * dryMass;
        foreach (AircraftFuelTank tank in tanks)
        {
            com += tank.GetMass() * tank.transform.localPosition;
        }
        com /= mass;
        inertiaTensor = dryInertiaTensor;
        foreach (AircraftFuelTank tank in tanks)
        {
            Vector3 lp = tank.transform.localPosition;
            Vector3 partialTensor = new Vector3(lp.z * lp.z + lp.y * lp.y, lp.x * lp.x + lp.z * lp.z, lp.y * lp.y + lp.x * lp.x);
            inertiaTensor += partialTensor * tank.GetMass();
        }
    }

    private void SetProperties()
    {
        rb.mass = mass;
        rb.centerOfMass = com;
        rb.inertiaTensor = inertiaTensor;
    }
}
