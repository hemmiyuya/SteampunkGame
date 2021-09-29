using UnityEngine;

public class PinSystemOverlay : MonoBehaviour
{

    private Camera targetCamera; // âfÇ¡ÇƒÇ¢ÇÈÇ©îªíËÇ∑ÇÈÉJÉÅÉâÇ÷ÇÃéQè∆
    private Transform canvas;
    [SerializeField]
    private GameObject pinPrefab;
    private GameObject pinUI;

    void Start()
    {
        targetCamera = Camera.main;
        canvas = GameObject.Find("Canvas").transform;
        pinUI = Instantiate(pinPrefab, canvas);
        pinUI.transform.SetParent(canvas);
        Vector2 screenPosition = GetScreenPosition(transform.position);
        Vector2 localPosition = GetCanvasLocalPosition(screenPosition);
        pinUI.transform.localPosition = localPosition;
    }

    void LateUpdate()
    {
        if (pinUI)
        {
            Vector2 screenPosition = GetScreenPosition(transform.position);
            Vector2 localPosition = GetCanvasLocalPosition(screenPosition);
            pinUI.transform.localPosition = localPosition;

        }
    }

    private Vector2 GetCanvasLocalPosition(Vector2 screenPosition)
    {
        return new Vector2(canvas.transform.InverseTransformPoint(screenPosition).x, canvas.transform.InverseTransformPoint(screenPosition).y + 100);
    }

    private Vector2 GetScreenPosition(Vector3 worldPosition)
    {
        return RectTransformUtility.WorldToScreenPoint(targetCamera, worldPosition);
    }

    public void PinDestroy()
    {
        pinUI.SetActive(false);
    }

    public void PinSet()
    {
        pinUI.SetActive(true);
    }
}