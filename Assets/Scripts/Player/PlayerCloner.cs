using System.Collections;
using System.Collections.Generic;
using Trapped_Inside.Param;
using UnityEngine;

public class PlayerCloner : MonoBehaviour
{
    //private Constants constants;
    public float scale;
    // Start is called before the first frame update
    private void Start()
    {
        transform.localScale = Vector3.one * scale;
    }
}
