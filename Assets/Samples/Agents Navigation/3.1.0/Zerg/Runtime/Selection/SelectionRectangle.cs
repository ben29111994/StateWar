using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.Mathematics;

public class SelectionRectangle : MonoBehaviour
{
    public RectTransform RectTransform;
    public CanvasScaler canvasScaler;
    Rect m_Rect;

    public void Show(Rect rect)
    {
        //Debug.LogError("2");
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        Vector2 size = rect.size;
        //Vector2 position = new Vector2(rect.center.x * canvasScaler.referenceResolution.x / Screen.width, rect.center.y * canvasScaler.referenceResolution.y / Screen.height);
        Vector2 position = rect.center;

        m_Rect.center = position;
        m_Rect.size = size;

        RectTransform.position = position;
        RectTransform.sizeDelta = size;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
