using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.NetworkInformation;
using Trapped_Inside.Tools.Timer;
using Unity.Mathematics;
using UnityEditor;

//using Trapped_Inside.Constants;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    //private TimerManager timerManager;
    public GameObject player;
    public GameObject GroundDetector;
    public PlayerInputController inputController;
    public float SlopeCheckDistance = 1f;
    public LayerMask GroundLayer;
    public GameObject consts;
    // Start is called before the first frame update
    //private Constants constants;
    private Consts constants;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D collider;

    public Vector2 direction;
    private Vector2 velocity = Vector2.zero;
    private Vector2 SpaceBase = Vector2.zero;

    private float slopeangle;
    private Vector2 slopeNormalPerp;

    [Header("光滑物理材质")]
    public PhysicsMaterial2D smooth;

    //一些判断
    private bool IsGrounded;             //是否落地
    private bool canjump;                //是否能跳跃
    private bool IsOnSlope;              //是否在斜坡上


    private void Awake()
    {
        //timerManager = new TimerManager();
        //timerManager.Start("check", 5f);s
        constants = consts.GetComponent<Consts>();
        inputController = new PlayerInputController();
        //GroundDetector = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        transform.localScale = Vector3.one * constants.MonoScale;
        rb.gravityScale = constants.Gravity;

        //IsGrounded = GroundDetector.GetComponent<GroundDetect>().IsGrounded;

        inputController.PlayerMovement.Jump.started += Jump;
    }


    
    // Update is called once per frame

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }
    void Update()
    {
        direction = inputController.PlayerMovement.Move.ReadValue<Vector2>();
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bar"))
        {
            
            Vector3 closestPos = collision.collider.ClosestPoint(transform.position);
            SlopeCheckVertical(closestPos);
        }
        
    }

    
    private void FixedUpdate()
    {
        //CheckGround();
        ApplyMovement();
        
    }
    private void ApplyMovement()
    {
        
        float scalelevel = Mathf.Log(Mathf.Abs(transform.localScale.x / constants.MonoScale), constants.AllScale);
        velocity = Quaternion.Euler(0, 0, -constants.Rotoffset * scalelevel) * rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        if (IsGrounded)
        {
            if (direction != Vector2.zero)
            {
                velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y);
            }
            //else
            //{
                
            //}
        }
        
        else
        {
            if (direction != Vector2.zero)
            {
                velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y);
            }
            else
            {
                if (velocity.x > 0)
                {
                    //velocity.x = 3;
                }
            }
        }
            rb.velocity = Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * velocity;
       
    }

    //private
    

    private void SlopeCheck()
    {
        //Vector2 pos = new Vector2(0, collider.size.y / 2 * constants.MonoScale);
        Vector2 pos = transform.position;
        SlopeCheckVertical(pos);
    }
    private void SlopeCheckHorizontal(Vector2 pos)
    {

    }
    private void SlopeCheckVertical(Vector2 pos)
    {
        float scalelevel = Mathf.Log(Mathf.Abs(player.transform.localScale.x / constants.MonoScale), constants.AllScale);

        RaycastHit2D hit = Physics2D.Raycast(pos, Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.down, SlopeCheckDistance * constants.MonoScale, GroundLayer);
        
        {
            if (hit)
            {
                slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
                //if (hit.normal.)
                if (Vector3.Cross(transform.up, hit.normal).normalized == -transform.forward)
                {
                    slopeangle = Vector2.Angle(hit.normal, Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up) * -1;
                }
                else if (Vector3.Cross(transform.up, hit.normal).normalized == transform.forward)
                {
                    slopeangle = Vector2.Angle(hit.normal, Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up);
                }
                //slopeangle = Vector2.Angle(hit.normal, Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up);
                Debug.DrawRay(hit.point, hit.normal * 100, Color.green);
                if (slopeangle != 0)
                {
                    IsOnSlope = true;

                }
                else
                {
                    IsOnSlope = false;
                }
            }
        }
            
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        float scalelevel = Mathf.Log(Mathf.Abs(player.transform.localScale.x / constants.MonoScale), constants.AllScale);
        if (GroundDetector.GetComponent<GroundDetect>().IsGrounded)
        {
            
            rb.AddForce(Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up * constants.JumpForce * Mathf.Pow(constants.AllScale, scalelevel), ForceMode2D.Impulse);
        }


    }
    
}
