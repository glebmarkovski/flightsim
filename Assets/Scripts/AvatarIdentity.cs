using Mirror;

public class AvatarIdentity: NetworkBehaviour
{
    [SyncVar]
    private string uuid = "";

    public void SetUuid(string uuid)
    {
        this.uuid = uuid;
    }

    public string GetUuid()
    {
        return uuid;
    }
}
