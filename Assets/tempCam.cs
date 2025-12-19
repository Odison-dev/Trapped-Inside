using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempCam : MonoBehaviour
{
    public Camera cam;
    public GameObject consts;
    private float k = 0;
    public float m = .015f;
    // Start is called before the first frame update
    void Start()
    {
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    { 
        k += m * Time.fixedDeltaTime;
        cam.orthographicSize = 5 * Mathf.Exp(-k);
        if (cam.orthographicSize <= 5 * consts.GetComponent<Consts>().AllScale)
        {
            //cam.orthographicSize = 5;
            temp();
            
        }
    }


    private void temp()
    {
        cam.transform.eulerAngles -= new Vector3(0, 0, consts.GetComponent<Consts>().Rotoffset);
        k = 0;
    }
}
