using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    public GameObject levelcloner;
    private Consts constants;
    public GameObject consts;
    //public GameObject Level;
    private void Start()
    {
        constants = consts.GetComponent<Consts>();
        transform.position += constants.PosOffset;
        transform.eulerAngles += new Vector3(0, 0, constants.Rotoffset);
        transform.localScale *= constants.AllScale * .99f;
        //transform.eulerAngles = levelcloner.GetComponent<LevelCloner>().clone_s.transform.eulerAngles;
        //transform.localScale = levelcloner.GetComponent<LevelCloner>().clone_s.transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //levelcloner.GetComponent<LevelCloner>().adb(collision.gameObject);
        if (collision.gameObject.name == "Circle" || collision.gameObject.name == "Anti-cube")
        {
            levelcloner.GetComponent<LevelCloner>().adb(collision.gameObject);
        }
    }
}
