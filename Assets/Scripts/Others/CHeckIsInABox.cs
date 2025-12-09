using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CheckIsInABox : MonoBehaviour
{
    
    
    
    public GameObject levelcloner;
    private float timecount;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //levelcloner.GetComponent<LevelCloner>().adb();
            //timecount = 0;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //Debug.Log(collision.gameObject.name);
            levelcloner.GetComponent<LevelCloner>().ads(collision.gameObject);
        }
        
    }
    

}
