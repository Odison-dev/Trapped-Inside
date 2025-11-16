using UnityEngine;

public class UltimateRenderPrecision : MonoBehaviour
{
    [Header("��������")]
    public int baseWidth = 1920;
    public int baseHeight = 1080;
    [Tooltip("��Ⱦ����Ŀ��Render Texture")]
    public RenderTexture targetRenderTexture;

    [Header("��������")]
    [Range(1, 8)] public int msaaLevel = 4;
    public bool enableHDR = true;
    public FilterMode textureFilter = FilterMode.Trilinear;
    [Range(0, 16)] public int anisoLevel = 9;

    [Header("�߼�����")]
    public bool enableSuperSampling = false;
    [Range(1.0f, 2.5f)] public float superSamplingFactor = 1.5f;
    public bool enableMipMaps = true;
    [Range(-1.0f, 1.0f)] public float mipMapBias = -0.3f;

    [Header("������Ϣ")]
    [SerializeField] private RenderTexture highPrecisionRT;
    [SerializeField] private int actualWidth;
    [SerializeField] private int actualHeight;
    [SerializeField] private string currentFormat;

    private Camera targetCamera;

    void Start()
    {
        InitializeSystem();
    }

    void InitializeSystem()
    {
        // ��ȡ������
        targetCamera = GetComponent<Camera>();
        if (targetCamera == null)
        {
            Debug.LogError("UltimateRenderPrecision��Ҫ����Camera�����ϣ�");
            return;
        }

        CreateUltimateRenderTexture();
        ApplyQualitySettings();

        Debug.Log($"�߾���Render Texture�������: {actualWidth}x{actualHeight}, MSAA{msaaLevel}, ��ʽ:{currentFormat}");
    }

    void CreateUltimateRenderTexture()
    {
        // ����ʵ�ʷֱ���
        actualWidth = enableSuperSampling ?
            Mathf.RoundToInt(baseWidth * superSamplingFactor) : baseWidth;
        actualHeight = enableSuperSampling ?
            Mathf.RoundToInt(baseHeight * superSamplingFactor) : baseHeight;

        // ѡ��RenderTexture��ʽ
        RenderTextureFormat format = enableHDR ?
            RenderTextureFormat.ARGBHalf : RenderTextureFormat.ARGB32;
        currentFormat = format.ToString();

        // ������������Render Texture
        highPrecisionRT = new RenderTexture(actualWidth, actualHeight, 24, format);

        // ������������
        highPrecisionRT.antiAliasing = msaaLevel;
        highPrecisionRT.filterMode = textureFilter;
        highPrecisionRT.useMipMap = enableMipMaps;
        highPrecisionRT.autoGenerateMips = enableMipMaps;
        highPrecisionRT.anisoLevel = anisoLevel;
        highPrecisionRT.mipMapBias = mipMapBias;

        // ��������
        if (targetRenderTexture != null)
        {
            // ���ָ�����ⲿRenderTexture��ʹ���ⲿ��
            targetCamera.targetTexture = targetRenderTexture;
        }
        else
        {
            // ����ʹ���Լ�������
            targetCamera.targetTexture = highPrecisionRT;
        }

        // ���������������Ⱦ
        targetCamera.allowMSAA = true;
        targetCamera.allowHDR = enableHDR;
    }

    void ApplyQualitySettings()
    {
        QualitySettings.antiAliasing = msaaLevel;
        QualitySettings.globalTextureMipmapLimit = 0; // �����������
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        QualitySettings.shadowResolution = ShadowResolution.High;
    }

    // ��̬��������
    public void UpdateSettings()
    {
        if (highPrecisionRT != null)
        {
            highPrecisionRT.Release();
        }
        CreateUltimateRenderTexture();
        ApplyQualitySettings();
    }

    // ��ȡ��ǰRenderTexture���������ű�ʹ�ã�
    public RenderTexture GetRenderTexture()
    {
        return targetRenderTexture != null ? targetRenderTexture : highPrecisionRT;
    }

    // ����RenderTexture���ļ������ڵ��ԣ�
    [ContextMenu("���浱ǰ֡")]
    public void SaveCurrentFrame()
    {
        if (highPrecisionRT == null) return;

        Texture2D tex = new Texture2D(highPrecisionRT.width, highPrecisionRT.height, TextureFormat.RGBA32, false);
        RenderTexture.active = highPrecisionRT;
        tex.ReadPixels(new Rect(0, 0, highPrecisionRT.width, highPrecisionRT.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/SavedFrame.png", bytes);
        Debug.Log("֡�ѱ���: " + Application.dataPath + "/SavedFrame.png");

        DestroyImmediate(tex);
    }

    void OnDestroy()
    {
        if (highPrecisionRT != null)
        {
            highPrecisionRT.Release();
            DestroyImmediate(highPrecisionRT);
        }
    }

    // Inspector��ť
    [ContextMenu("Ӧ��������")]
    private void ApplySettingsContext()
    {
        UpdateSettings();
    }

    [ContextMenu("����Ĭ������")]
    private void ResetToDefault()
    {
        baseWidth = 1920;
        baseHeight = 1080;
        msaaLevel = 4;
        enableHDR = true;
        textureFilter = FilterMode.Trilinear;
        enableSuperSampling = false;
        superSamplingFactor = 1.5f;

        UpdateSettings();
    }
}