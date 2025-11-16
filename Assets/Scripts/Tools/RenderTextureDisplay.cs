// 创建显示Render Texture的RawImage：
// 1. Hierarchy右键 → UI → Canvas
// 2. 在Canvas下右键 → UI → Raw Image
// 3. 将Render Texture分配给Raw Image：
using UnityEngine;
using UnityEngine.UI;

public class RenderTextureDisplay : MonoBehaviour
{
    public UltimateRenderPrecision renderPrecision;
    public RawImage displayImage;

    void Start()
    {
        if (renderPrecision != null && displayImage != null)
        {
            displayImage.texture = renderPrecision.GetRenderTexture();

            // 根据缩放调整RawImage大小
            RectTransform rt = displayImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(renderPrecision.baseWidth / 4, renderPrecision.baseHeight / 4);
        }
    }
}