using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B : MonoBehaviour
{
    public GameObject levelcloner;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle"  || collision.gameObject.name == "Anti-cube")
        {
            levelcloner.GetComponent<LevelCloner>().ads(collision.gameObject);
        }
        //levelcloner.GetComponent<LevelCloner>().ads(collision.gameObject);
    }
}
