using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class RingTimer : MonoBehaviour
{
    public Image ring;
    public GameObject flag;
    public GameObject constants;
    // Start is called before the first frame update
    void Start()
    {
        //ring = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        ring.fillAmount = flag.GetComponent<Flag>().timer / constants.GetComponent<Consts>().WinTimer;
    }
}
