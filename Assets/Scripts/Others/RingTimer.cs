using System.Collections;
using System.Collections.Generic;
using Trapped_Inside.Tools.Timer;
using UnityEngine;

public class RingTimer : MonoBehaviour
{
    private TimerManager timerManager;
    // Start is called before the first frame update
    void Start()
    {
        timerManager = new TimerManager();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        Loading();
    }

    private void Loading()
    {

    }
}
