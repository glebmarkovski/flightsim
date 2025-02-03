using UnityEngine;

public class InputDisplay2D : MonoBehaviour
{
    [SerializeField]
    private string xAxis;
    [SerializeField]
    private string yAxis;
    private RectTransform main;
    private RectTransform trim;
    [SerializeField]
    private Vector2 rangeOfMotion;
    private OnlineUIManager manager;
    [SerializeField]
    bool enableTrim;

    private void Awake()
    {
        manager = GetComponentInParent<OnlineUIManager>();
        if (enableTrim)
        {
            main = transform.GetChild(2).GetComponent<RectTransform>();
            trim = transform.GetChild(1).GetComponent<RectTransform>();
        }
        else
        {
            main = transform.GetChild(1).GetComponent<RectTransform>();
        }
    }

    private void Update()
    {
        if (manager.GetInput() == null)
        {
            return;
        }
        main.anchoredPosition = new Vector2(manager.GetInput().GetAxis(xAxis) * rangeOfMotion.x, manager.GetInput().GetAxis(yAxis) * rangeOfMotion.y);
        if (enableTrim)
        {
            trim.anchoredPosition = new Vector2(manager.GetInput().GetTrim(xAxis) * rangeOfMotion.x, manager.GetInput().GetTrim(yAxis) * rangeOfMotion.y);
        }
    }
}
