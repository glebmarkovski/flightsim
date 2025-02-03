using UnityEngine;
using Mirror;

public class PlayerVehicle : NetworkBehaviour
{
    private AvatarIdentity identity;
    private PlayerInput input;
    private PlayerManager player;
    private NewNetworkManager manager;
    private PlayerVehicleCamera vehicleCamera;
    private ControllableBehaviour[] controllables;
    private PlayerInput blank;
    private Telemetry telemetry;

    private void Awake()
    {
        telemetry = GetComponent<AircraftTelemetry>();
        vehicleCamera = GetComponentInChildren<PlayerVehicleCamera>();
        blank = new PlayerInput();
        identity = GetComponent<AvatarIdentity>();
        manager = GameObject.Find("Network Manager").GetComponent<NewNetworkManager>();
        controllables = GetComponentsInChildren<ControllableBehaviour>();
        foreach (ControllableBehaviour controllable in controllables)
        {
            controllable.SetInput(blank);
        }
    }

    private void Update()
    {
        vehicleCamera.transform.GetChild(0).gameObject.SetActive(player != null && player.isLocalPlayer);

        if (identity.GetUuid() == "")
        {
            if (player != null)
            {
                player = null;
            }
            if (input != null)
            {
                input = null;
                foreach (ControllableBehaviour controllable in controllables)
                {
                    controllable.SetInput(blank);
                }
            }
        }
        else
        {
            if (player == null)
            {
                input = null;
                foreach (ControllableBehaviour controllable in controllables)
                {
                    controllable.SetInput(blank);
                }
                player = manager.GetPlayerByUuid(identity.GetUuid());
            }

            if (player != null && player.isLocalPlayer)
            {
                GameObject.Find("UI").GetComponent<OnlineUIManager>().SetTelemetry(telemetry);
            }

            if (player != null && input == null)
            {
                input = player.GetInput();
                foreach (ControllableBehaviour controllable in controllables)
                {
                    controllable.SetInput(input);
                }
            }

            if (isServer && input != null)
            {
                if (input.GetButton("Exit"))
                {
                    identity.SetUuid("");
                    player.ExitVehicle(transform.position);
                }
            }
        }
    }
}
