using UnityEngine; 

public class OnlineUIManager : MonoBehaviour
{
    private bool paused;
    private GameObject pauseUI;
    private GameObject outOfGameUI;
    private GameObject cameraBG;
    private GameObject huds;
    private PlayerManager player;
    private Telemetry telemetrySource;
    private PlayerInput input;

    public PlayerInput GetInput()
    {
        return input;
    }

    public Telemetry GetTelemetry()
    {
        return telemetrySource;
    }

    public void SetTelemetry(Telemetry telemetry)
    {
        telemetrySource = telemetry;
    }

    public void SetPlayer(PlayerManager player)
    {
        this.player = player;
        input = player.GetInput();
    }

    private void Awake()
    {
        pauseUI = transform.GetChild(1).gameObject;
        outOfGameUI = transform.GetChild(0).gameObject;
        cameraBG = transform.GetChild(2).gameObject;
        huds = transform.GetChild(3).gameObject;
    }

    private void Start()
    {
        paused = false;
    }
    public bool IsPaused()
    {
        return paused; 
    }

    public void Spawn()
    {
        player.SpawnAvatar();
    }

    public void Resume()
    {
        paused = false;
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }

        if (player.IsInGame())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
            }
            if (paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if(telemetrySource != null)
                {
                    foreach (Transform hud in huds.transform)
                    {
                        hud.gameObject.SetActive(hud.gameObject.name == telemetrySource.GetHudMode());
                    }
                }
            }
        }
        else
        {
            paused = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        outOfGameUI.SetActive(!player.IsInGame());
        cameraBG.SetActive(!player.IsInGame());
        pauseUI.SetActive(paused);
        huds.SetActive(player.IsInGame());
    }
}
