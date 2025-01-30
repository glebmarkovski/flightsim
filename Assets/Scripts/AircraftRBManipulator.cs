using UnityEngine;

public class AircraftRBManipulator : MonoBehaviour
{
    private Rigidbody rb;
    private AircraftFuelTank[] tanks;
    [SerializeField]
    private float dryMass;
    [SerializeField]
    private Vector3 drycom;
    [SerializeField]
    private Vector3 dryInertiaTensor;
    private float mass;
    private Vector3 com;
    private Vector3 inertiaTensor;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tanks = GetComponentsInChildren<AircraftFuelTank>();
    }

    private void Start()
    {
        SetProperties();
    }

    private void FixedUpdate()
    {
        SetProperties();
    }

    private void SetProperties()
    {
        mass = dryMass;
        foreach(AircraftFuelTank tank in tanks)
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
        rb.mass = mass;
        rb.centerOfMass = com;
        rb.inertiaTensor = inertiaTensor;
    }
}
