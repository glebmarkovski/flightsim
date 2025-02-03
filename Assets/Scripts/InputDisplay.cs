using UnityEngine;

public class InputDisplay : MonoBehaviour
{
    [SerializeField]
    private string axis;
    private RectTransform main;
    private RectTransform trim;
    [SerializeField]
    private float rangeOfMotion;
    private OnlineUIManager manager;
    [SerializeField]
    bool enableTrim;
    [SerializeField]
    bool vertical;

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
        if (vertical)
        {
            main.anchoredPosition = new Vector2(0, manager.GetInput().GetAxis(axis) * rangeOfMotion);
        }
        else
        {
            main.anchoredPosition = new Vector2(manager.GetInput().GetAxis(axis) * rangeOfMotion, 0);
        }
        
        if (enableTrim)
        {
            if (vertical)
            {
                trim.anchoredPosition = new Vector2(0, manager.GetInput().GetTrim(axis) * rangeOfMotion);
            }
            else
            {
                trim.anchoredPosition = new Vector2(manager.GetInput().GetTrim(axis) * rangeOfMotion, 0);
            }
        }
    }
}
