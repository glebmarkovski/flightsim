using Mirror;
using UnityEngine;

public class Telemetry : NetworkBehaviour
{
    public virtual string GetHudMode()
    {
        return "";
    }

    public virtual float GetValue(string name)
    {
        return 0f;
    }
}
