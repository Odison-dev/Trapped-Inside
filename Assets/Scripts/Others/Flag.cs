using System.Collections;
using System.Collections.Generic;
using Trapped_Inside.Tools.Timer;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private TimerManager timerManager;
    private bool IsStay;
    public bool IsDone = false;
    public GameObject canvas;
    public float timer;
    public GameObject constants;
    private Consts c;

    // Start is called before the first frame update
    void Start()
    {
        c = constants.GetComponent<Consts>();
        timerManager = new TimerManager();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
           IsStay = true;
            //Loading();
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            IsStay = false;
            //Loading();
        }
    }

    private void Update()
    {
        //Debug.Log(timer);
        Loading();
        if (IsStay)
        {
            if (timer < c.WinTimer)
            {
                timer += Time.deltaTime;
            }
            
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            
        }
    }
    private void Loading()
    {
        
        if (timer >= c.WinTimer)
        {
            timer = c.WinTimer;
            //print("SSSSSSS");
            IsDone = true;
            canvas.SetActive(true);
        }
        else if (timer <= 0)
        {
            timer = 0;
        }
        //timerManager.Start("PlayerStay", 5f);
        //if (timerManager.IsFinished("PlayerStay"))
        //{
        //    print("SSS");
        //    IsDone = true;
        //}
    }
}
