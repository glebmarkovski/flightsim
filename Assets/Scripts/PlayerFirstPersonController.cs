using Mirror;
using UnityEngine;

public class PlayerFirstPersonController : NetworkBehaviour
{
    Transform cameraPivot;
    CharacterController characterController;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float strafeSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float g;
    private float yVelocity;
    private AvatarIdentity identity;
    private PlayerInput input;
    private PlayerManager player;
    private NewNetworkManager manager;
    private Telemetry telemetry;

    private void Awake()
    {
        telemetry = GetComponent<AvatarTelemetry>();
        cameraPivot = transform.GetChild(0);
        characterController = GetComponent<CharacterController>();
        identity = GetComponent<AvatarIdentity>();
        manager = GameObject.Find("Network Manager").GetComponent<NewNetworkManager>();
    }

    void Update()
    {
        cameraPivot.transform.GetChild(0).gameObject.SetActive(player != null && player.isLocalPlayer);

        if (player == null)
        {
            player = manager.GetPlayerByUuid(identity.GetUuid());
        }

        if (player != null && player.isLocalPlayer)
        {
            GameObject.Find("UI").GetComponent<OnlineUIManager>().SetTelemetry(telemetry);
        }

        if (player != null && input == null)
        {
            input = player.GetInput();
        }

        if (isServer && input != null)
        {
            transform.localRotation = Quaternion.Euler(0, input.GetView().x, 0);
            cameraPivot.localRotation = Quaternion.Euler(input.GetView().y, 0, 0);
            if (characterController.isGrounded)
            {
                yVelocity = -1f;
                if (input.GetButton("Jump"))
                {
                    yVelocity = jumpSpeed;
                }
            }
        }
    }
    private void FixedUpdate()
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
            yVelocity -= Time.fixedDeltaTime * g;
            characterController.Move((input.GetAxis("Walk")
                * walkSpeed
                * transform.forward
                + input.GetAxis("Strafe")
                * strafeSpeed
                * transform.right
                + transform.up
                * yVelocity) * Time.fixedDeltaTime);
        }
    }
}
