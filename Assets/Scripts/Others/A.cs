using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A : MonoBehaviour
{
    public GameObject levelcloner;
    private Constants constants;
    //public GameObject Level;
    private void Start()
    {
        constants = new Constants();
        transform.position += constants.PosOffset;
        transform.eulerAngles += new Vector3(0, 0, constants.Rotoffset);
        transform.localScale *= constants.AllScale * .99f;
        //transform.eulerAngles = levelcloner.GetComponent<LevelCloner>().clone_s.transform.eulerAngles;
        //transform.localScale = levelcloner.GetComponent<LevelCloner>().clone_s.transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            levelcloner.GetComponent<LevelCloner>().adb();
        }
    }
}
