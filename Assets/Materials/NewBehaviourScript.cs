using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
   // Start is called before the first frame update
    public Texture Texture;
    public Material Material;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        Shader.SetGlobalTexture("New Render Texture1", Texture);
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material = Material;
    }
}
