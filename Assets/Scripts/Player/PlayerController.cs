using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Constants constants;
    
    private void Start()
    {
        constants = new Constants();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * constants.MonoScale;
    }
}
