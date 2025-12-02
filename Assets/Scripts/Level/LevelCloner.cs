using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ComponentModel.Design;

public class LevelCloner : MonoBehaviour
{
    [Header("变换相关参数")]
    //public float scale = .4f;
    private float scale;
    private float sidelength = 8;
    public GameObject consts;
    //public Vector3 RotOffset = Vector3.zero;
    //public Vector3 PosOffset = Vector3.zero;
    private Vector3 PosOffset;
    private Vector3 RotOffset;
    [Header("关卡对象")]
    public GameObject Level;
    public GameObject Player;
    public GameObject PlayerPre;
    [Header("渲染")]
    public GameObject RenderCube;

    //private Constants constants;
    private Consts constants;

    //public GameObject check;

    //委托
    public delegate void LevelTransform();
    public static event LevelTransform TransformLevel;
    private GameObject player_s;
    public GameObject clone_s;
    private GameObject player_b;
    public GameObject clone_b;
    //GameObject player_s = Instantiate(Player);


    private void Start()
    {
        constants = consts.GetComponent<Consts>();
        PosOffset = constants.PosOffset;
        RotOffset = new Vector3(0, 0, constants.Rotoffset);
        scale = constants.AllScale;

        player_s = Instantiate(PlayerPre);
        player_s.GetComponent<Rigidbody2D>().isKinematic = true;

        player_b = Instantiate(PlayerPre);
        //player_b.GetComponent<Rigidbody2D>().isKinematic = true;

        clone_s = Instantiate(Level.gameObject);
        clone_b = Instantiate(Level.gameObject);
        
        RenderCube = GameObject.Find("Renderer");
        CloneLevel();
        ClonePlayer();
        PlaceRenderCube();
        
    }
    private void Update()
    {
        AdjustPlayer();
    }
    //
    private void CloneLevel()
    {

        //小号关卡克隆
        //GameObject clone_s = Instantiate (Level.gameObject);
        TransformGameobj("lower", clone_s.transform, Level.transform);

        //大号关卡克隆
        //GameObject clone_b = Instantiate(Level.gameObject);
        TransformGameobj("upper", clone_b.transform, Level.transform);
    }

    private void ClonePlayer()
    {
        //GameObject player_s = Instantiate(Player.gameObject);
        TransformGameobj("lower", player_s.transform, Player.transform);


        //GameObject player_b = Instantiate(Player.gameObject);
        TransformGameobj("upper", player_b.transform, Player.transform);
    }
    
    private void PlaceRenderCube()
    {
        GameObject rc1 = Instantiate(RenderCube);
        rc1.transform.localScale = Vector3.one * scale * sidelength;
        rc1.transform.position = Level.transform.position + PosOffset - Vector3.forward * 5;
        rc1.transform.eulerAngles = RotOffset + Vector3.forward * 180 + Level.transform.eulerAngles;


        RenderCube.transform.localScale = Vector3.one / scale / scale * sidelength;
        RenderCube.transform.position = (Level.transform.position - Quaternion.Euler(0, 0, -RotOffset.z) * PosOffset / scale) - Quaternion.Euler(0, 0, -RotOffset.z * 2) * PosOffset / Mathf.Pow(scale, 2) + Vector3.forward * sidelength * 10;
        RenderCube.transform.eulerAngles = -RotOffset * 2 + Vector3.forward * 180 + Level.transform.eulerAngles;
    }

    private void AdjustPlayer()
    {
        TransformGameobj("lower", player_s.transform, Player.transform);
        TransformGameobj("upper", player_b.transform, Player.transform);
        //if (check.GetComponent<CheckIsInABox>().enter)
        //{

        //    TransformGameobj("upper", Player.transform, Player.transform);
        //    Player.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, -RotOffset.z) * Player.GetComponent<Rigidbody2D>().velocity / scale;
        //    Player.GetComponent<Rigidbody2D>().gravityScale /= scale;
        //    Physics2D.gravity = Quaternion.Euler(0, 0, -RotOffset.z) * Physics2D.gravity;
        //    //Player.transform = player_b.transform; 
        //}
        //if (check.GetComponent<CheckIsInABox>().exit)
        //{
        //TransformGameobj("lower", Player.transform, Player.transform);
        //Player.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, RotOffset.z) * Player.GetComponent<Rigidbody2D>().velocity * scale;
        //Player.GetComponent<Rigidbody2D>().gravityScale *= scale;
        //Physics2D.gravity = Quaternion.Euler(0, 0, RotOffset.z) * Physics2D.gravity;
        //}
    }

    public void adb()
    {
        TransformGameobj("upper", Player.transform, Player.transform);
        Player.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, -RotOffset.z) * Player.GetComponent<Rigidbody2D>().velocity / scale;
        Player.GetComponent<Rigidbody2D>().gravityScale /= scale;
        Physics2D.gravity = Quaternion.Euler(0, 0, -RotOffset.z) * Physics2D.gravity;
    }
    public void ads()
    {
        TransformGameobj("lower", Player.transform, Player.transform);
        Player.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, RotOffset.z) * Player.GetComponent<Rigidbody2D>().velocity * scale;
        Player.GetComponent<Rigidbody2D>().gravityScale *= scale;
        Physics2D.gravity = Quaternion.Euler(0, 0, RotOffset.z) * Physics2D.gravity;
    }


    private bool IsInSmall()
    {
        //Vector2 distance = Player.transform.position - clone_s.transform.position;
        //Vector2 new_vector = new Vector2(distance.x * Mathf.Cos(RotOffset.z) - distance.y * Mathf.Sin(RotOffset.z), distance.x * Mathf.Sin(RotOffset.z) + distance.y * Mathf.Cos(RotOffset.z));
        //if (Mathf.Abs(Mathf.Acos(Vector2.Dot(distance, clone_s.transform.up) / (distance.magnitude * clone_s.transform.up.magnitude))) < 45)
        //{
        //    if (Mathf.Abs(Vector2.Dot(distance, clone_s.transform.up)) < sidelength * scale / 2) { return true; }


        //}
        //if (Mathf.Abs(Mathf.Acos(Vector2.Dot(distance, clone_s.transform.right) / (distance.magnitude * clone_s.transform.right.magnitude))) < 45)
        //{
        //    if (Mathf.Abs(Vector2.Dot(distance, clone_s.transform.right)) < sidelength * scale / 2) { return true; }
        //}
        ////return false;
        ////if (check.gameOb)
        return false;
        //return check.GetComponent<CheckIsInABox>().enter;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private bool IsInBig()
    {
        Vector2 distance = Player.transform.position - Level.transform.position;
        if (distance.x < -sidelength / 2)
        {
            return true;
        }
        //print(Mathf.Sin(90));

        return false;
    }

    public void TransformGameobj(string type, Transform objTransform,Transform originObj)
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