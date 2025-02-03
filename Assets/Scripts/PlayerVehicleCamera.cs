using Mirror;
using UnityEngine;

public class PlayerVehicleCamera : ControllableBehaviour
{
    private Transform vehicleCamera;
    [SerializeField]
    private float minDistance;
    [SerializeField]
    private float maxDistance;

    private void Awake()
    {
        vehicleCamera = transform.GetChild(0);
    }

    private void Update()
    {
        if (isServer)
        {
            transform.rotation = Quaternion.Euler(input.GetView().y, input.GetView().x, 0);
            vehicleCamera.localPosition = new Vector3(0, 0, -Mathf.Lerp(minDistance, maxDistance, input.GetZoom()));
        }
    }
}
