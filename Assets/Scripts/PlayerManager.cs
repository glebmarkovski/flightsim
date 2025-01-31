using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    private NewNetworkManager manager;

    public GameObject avatarPrefab;

    public PlayerInput input;

    private void Update()
    {
        if (isLocalPlayer)
        {
            input.UpdateAxes();
            input.UpdateButtons();
            input.UpdateView();
            CmdSendAxes(input.GetAxes());
            CmdSendButtons(input.GetButtons());
            CmdSendView(input.GetView());
        }
    }

    public void Spawn()
    {
        if (isLocalPlayer)
        {
            CmdSpawn();
        }
    }

    [Command]
    private void CmdSpawn()
    {
        GameObject avatar = Instantiate(avatarPrefab);
        avatar.name = gameObject.name + " avatar";
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
