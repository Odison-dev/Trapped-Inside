using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;
using System.Drawing;
using System.Runtime.InteropServices;

public class LevelCloner : MonoBehaviour
{
    [Header("变换相关参数")]
    public float scale = .4f;
    public float sidelength = 8;
    public GameObject center;
    public Vector3 RotOffset = Vector3.zero;
    public Vector3 PosOffset = Vector3.zero;
    [Header("关卡对象")]
    public GameObject Level;
    public GameObject Player;
    [Header("渲染")]
    public GameObject RenderCube;

    private Constants constants;
    private GameObject player_s;
    private GameObject player_b;
    private GameObject clone_s;
    private GameObject clone_b;

    //GameObject player_s = Instantiate(Player);


    private void Start()

    {
        player_s = Instantiate(Player.gameObject);
        player_b = Instantiate(Player.gameObject);
        clone_s = Instantiate(Level.gameObject);
        clone_b = Instantiate(Level.gameObject);
        constants = new Constants();
        RenderCube = GameObject.Find("Renderer");
        
        PlaceRenderCube();
        
    }
    private void Update()
    {
        
        CloneLevel();
        ClonePlayer();
        AdjustPlayer();
    }

    private void CloneLevel()
    {

        //小号关卡克隆
        //GameObject clone_s = Instantiate (Level.gameObject);
        clone_s.transform.localScale = Vector3.one * scale;
        clone_s.transform.position = Level.transform.position + PosOffset;
        clone_s.transform.eulerAngles = Level.transform.eulerAngles + RotOffset;

        //大号关卡克隆
        //GameObject clone_b = Instantiate(Level.gameObject);
        clone_b.transform.localScale = Vector3.one / scale;
        clone_b.transform.position = Level.transform.position - PosOffset / scale;
        clone_b.transform.eulerAngles = Level.transform.eulerAngles - RotOffset;
    }

    private void ClonePlayer()
    {

        player_s.transform.localScale = Player.transform.localScale * scale;
        player_s.transform.position = (Player.transform.position - Level.transform.position) * scale + Level.transform.position + PosOffset;
        player_s.transform.eulerAngles = Player.transform.eulerAngles + RotOffset;



        player_b.transform.localScale = Player.transform.localScale / scale;
        player_b.transform.position = (Player.transform.position - Level.transform.position) / scale + Level.transform.position - PosOffset / scale;
        player_b.transform.eulerAngles = Player.transform.eulerAngles + RotOffset;
    }
    
    private void PlaceRenderCube()
    {
       

        RenderCube.transform.localScale = new Vector3(1, 1, 1) * scale * sidelength;
        RenderCube.transform.position = Level.transform.position + PosOffset - Vector3.forward * 5;
        //RenderCube.transform.position = Level.transform.position + PosOffset + new Vector3(PosOffset2.x * Mathf.Sin(RotOffset.z * 2), PosOffset2.y * Mathf.Cos(RotOffset.z * 2), RenderCube.transform.position.z);
        RenderCube.transform.eulerAngles = RotOffset * 2 + Vector3.forward * 180 + Level.transform.eulerAngles + RotOffset;

        
        
    }

    private void AdjustPlayer()
    {
        if (IsCollidedSmall())
        {
            
            Player.transform.position = player_b.transform.position;
            Player.transform.eulerAngles = player_b.transform.eulerAngles;
            Player.transform.localScale = player_b.transform.localScale;
            Player.GetComponent<Rigidbody2D>().velocity /= scale;
        }
    }

    private bool IsCollidedSmall()
    {
        Vector2 distance = Player.transform.position - clone_s.transform.position;
        Vector2 new_vector = new Vector2(distance.x * Mathf.Cos(RotOffset.z) - distance.y * Mathf.Sin(RotOffset.z), distance.x * Mathf.Sin(RotOffset.z) + distance.y * Mathf.Cos(RotOffset.z));
        if (new_vector.magnitude < sidelength * scale / 2)
        {
            return true;
        }
            return false;
    }
}