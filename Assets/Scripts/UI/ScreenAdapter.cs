using UnityEngine;
using UnityEngine.UI;

public class ScreenAdapter : MonoBehaviour
{
    public RectTransform uiElement;

    private void Start()
    {
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        if (scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            float referenceWidth = scaler.referenceResolution.x;
            float referenceHeight = scaler.referenceResolution.y;
            float currentWidth = Screen.width;
            float currentHeight = Screen.height;

            float scaleWidth = currentWidth / referenceWidth;
            float scaleHeight = currentHeight / referenceHeight;
            float scale = Mathf.Min(scaleWidth, scaleHeight);

            uiElement.localScale = new Vector3(scale, scale, 1);
        }
    }
}
