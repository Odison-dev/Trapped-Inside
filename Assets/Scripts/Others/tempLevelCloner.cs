using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class tempLevelCloner : MonoBehaviour
{
   
    private float sidelength = 8;
    public GameObject consts;

    private float scale;
    private Vector3 PosOffset;
    private Vector3 RotOffset;
    public GameObject Level;
    public GameObject Player;
    public GameObject PlayerPre;
    
    private GameObject player_s;
    private GameObject clone_s;

    //private Constants constants;
    private Consts constants;
    // Start is called before the first frame update
    void Start()
    {
        constants = consts.GetComponent<Consts>();
        player_s = Instantiate(PlayerPre);
        player_s.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        SceneManager.MoveGameObjectToScene(player_s, gameObject.scene);
        clone_s = Instantiate(Level.gameObject);
        SceneManager.MoveGameObjectToScene(clone_s, gameObject.scene);
        PosOffset = constants.PosOffset;
        RotOffset = new Vector3(0, 0, constants.Rotoffset);
        scale = constants.AllScale;
        CloneLevel();
    }

    

    // Update is called once per frame
    void Update()
    {
        ClonePlayer();
    }

    private void ClonePlayer()
    {

        //player_s = Instantiate(Player.gameObject);
        TransformGameobj("lower", player_s.transform, Player.transform);
        //Destroy(player_s);


        //player_b = Instantiate(Player.gameObject);
        
        //Destroy(player_b);
    }



    private void CloneLevel()
    {
        TransformGameobj("lower", clone_s.transform, Level.transform);
    }

    public void TransformGameobj(string type, Transform objTransform, Transform originObj)
    {
        if (type == "lower")
        {
            objTransform.transform.position = Quaternion.Euler(0, 0, RotOffset.z) * (originObj.transform.position - Level.transform.position) * scale + PosOffset + Level.transform.position;
            objTransform.transform.localScale = originObj.transform.localScale * scale;
            objTransform.transform.eulerAngles = originObj.transform.eulerAngles + RotOffset;
            //if (objTransform.GetComponent<Rigidbody2D>() != null && originObj.GetComponent<Rigidbody2D>() != null)
            //{

            //    objTransform.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, -RotOffset.z) * originObj.GetComponent<Rigidbody2D>().velocity / scale;
            //}
        }
        else if (type == "upper")
        {
            objTransform.transform.position = Quaternion.Euler(0, 0, -RotOffset.z) * (originObj.transform.position - Level.transform.position) / scale + Level.transform.position - Quaternion.Euler(0, 0, -RotOffset.z) * (PosOffset / scale);
            objTransform.transform.localScale = originObj.transform.localScale / scale;
            objTransform.transform.eulerAngles = originObj.transform.eulerAngles - RotOffset;
            //if (objTransform.GetComponent<Rigidbody2D>() != null && originObj.GetComponent<Rigidbody2D>() != null)
            //{
            //    objTransform.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, RotOffset.z) * originObj.GetComponent<Rigidbody2D>().velocity * scale;
            //}
        }
        //return objTransform;
    }
}
