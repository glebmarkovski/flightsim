using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Security.Principal;

public class PlayerManager : NetworkBehaviour
{
    private NewNetworkManager manager;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private GameObject avatarPrefab;
    [SyncVar]
    private string username;
    [SyncVar]
    private string uuid;
    private OnlineUIManager ui;
    [SyncVar]
    private bool inGame;
    [SyncVar]
    private bool inVehicle;

    public bool IsInVehicle()
    {
        return inVehicle;
    }

    public bool IsInGame()
    {
        return inGame;
    }

    public PlayerInput GetInput()
    {
        return input;
    }

    public void SetUuid(string uuid)
    {
        this.uuid = uuid;
    }

    public string GetUuid()
    {
        return uuid;
    }

    private void Awake()
    {
        manager = GameObject.Find("Network Manager").GetComponent<NewNetworkManager>();
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            ui = GameObject.Find("UI").GetComponent<OnlineUIManager>();
            ui.SetPlayer(this);
        }
        inGame = false;
    }

    private void Update()
    {
        if (isLocalPlayer && inGame)
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                GlobalDebugSettings.debug = !GlobalDebugSettings.debug;
            }
            if (ui.IsPaused())
            {
                input.ResetAxes();
                input.ResetButtons();
            }
            else
            {
                input.UpdateAxes();
                input.UpdateButtons();
                input.UpdateView();
                input.UpdateZoom();
            }
            CmdSendAxes(input.GetAxes());
            CmdSendButtons(input.GetButtons());
            CmdSendView(input.GetView());
            CmdSendZoom(input.GetZoom());
        }
    }

    [Command]
    private void CmdSendZoom(float zoom)
    {
        input.SetZoom(zoom);
    }

    public void SpawnAvatar()
    {
        CmdSpawnAvatar();
    }

    public override void OnStartClient()
    {
        manager.AddPlayer(this);
        if (isLocalPlayer)
        {
            CmdSendUsername(username);
        }
    }

    [Command]
    public void CmdSendUsername(string username)
    {
        this.username = username;
        uuid = System.Guid.NewGuid().ToString();
    }

    [Command]
    private void CmdSpawnAvatar()
    {
        if (inGame)
        {
            return;
        }
        inGame = true;
        GameObject avatar = Instantiate(avatarPrefab);
        avatar.transform.parent = GameObject.Find("Avatars").transform;
        avatar.GetComponent<AvatarIdentity>().SetUuid(uuid);
        NetworkServer.Spawn(avatar);
    }

    [Server]
    public void EnterVehicle(AvatarIdentity vehicle, GameObject avatar)
    {
        if (!inGame || inVehicle)
        {
            return;
        }
        inVehicle = true;
        vehicle.SetUuid(uuid);
        NetworkServer.Destroy(avatar);
    }

    [Server]
    public void ExitVehicle(Vector3 position)
    {
        if (!inGame || !inVehicle)
        {
            return;
        }
        inVehicle = false;
        GameObject avatar = Instantiate(avatarPrefab, position, Quaternion.identity);
        avatar.transform.parent = GameObject.Find("Avatars").transform;
        avatar.GetComponent<AvatarIdentity>().SetUuid(uuid);
        NetworkServer.Spawn(avatar);
    } 

    [Command]
    private void CmdSendButtons(bool[] values)
    {
        input.SetButtons(values);
    }

    [Command]
    private void CmdSendAxes(float[] values)
    {
        input.SetAxes(values);
    }

    [Command]
    private void CmdSendView(Vector2 view)
    {
        input.SetView(view);
    }    
}
