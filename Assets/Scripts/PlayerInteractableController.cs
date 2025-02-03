    using UnityEngine;
using Mirror;

public class PlayerInteractableController : NetworkBehaviour
{
    private Transform cameraPivot;
    [SerializeField]
    private float range;
    [SerializeField]
    private LayerMask layerMask;
    private AvatarIdentity identity;
    private PlayerInput input;
    private PlayerManager player;
    private NewNetworkManager manager;

    private void Awake()
    {
        cameraPivot = transform.GetChild(0);
        identity = GetComponent<AvatarIdentity>();
        manager = GameObject.Find("Network Manager").GetComponent<NewNetworkManager>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = manager.GetPlayerByUuid(identity.GetUuid());
        }

        if (player != null && input == null)
        {
            input = player.GetInput();
        }

        if (isServer && input != null)
        {
            if (input.GetButton("Enter"))
            {
                RaycastHit hit;
                //Debug.Log("Attempting to Interact");
                if (Physics.Raycast(cameraPivot.position, cameraPivot.forward, out hit, range, layerMask))
                {
                    //Debug.Log("Hit Something");
                    AvatarIdentity vehicle = hit.collider.GetComponentInParent<AvatarIdentity>();
                    if (vehicle != null && vehicle.GetUuid() == "")
                    {
                        //Debug.Log("Interacting");
                        player.EnterVehicle(vehicle, gameObject);
                    }
                }
            }
        }
    }
}
