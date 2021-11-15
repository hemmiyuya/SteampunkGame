using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class QuestUITargetIndicator : MonoBehaviour
{
    [SerializeField]
    private Transform target = default;

    private Camera mainCamera;
    private RectTransform rectTransform;

    private Vector3 offSet = new Vector3(0, 100f,0);

    private void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        target = transform.parent.parent;
    }

    private void LateUpdate()
    {
        float canvasScale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        var pos = mainCamera.WorldToScreenPoint(target.position) - center+offSet;
        if (pos.z < 0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            if (Mathf.Approximately(pos.y, 0f))
            {
                pos.y = -center.y;
            }
        }

        var halfSize = 0.2f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(
            Mathf.Abs(pos.x / (center.x - halfSize.x)),
            Mathf.Abs(pos.y / (center.y - halfSize.y))
        );

        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        rectTransform.anchoredPosition = pos / canvasScale;
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
    }
}
