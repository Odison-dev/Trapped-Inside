using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using UnityEditor;

//using Trapped_Inside.Constants;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public PlayerInputController inputController;
    public float SlopeCheckDistance = 1f;
    public LayerMask GroundLayer;
    // Start is called before the first frame update
    private Constants constants;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider;
    public Vector2 direction;
    private Vector2 velocity = Vector2.zero;
    private Vector2 SpaceBase = Vector2.zero;

    //一些判断
    private bool IsGrounded;             //是否落地
    private bool canjump;                //是否能跳跃
    private bool IsOnSlope;              //是否在斜坡上


    private void Awake()
    {
        constants = new Constants();
        inputController = new PlayerInputController();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        transform.localScale = Vector3.one * constants.MonoScale;
        rb.gravityScale = constants.Gravity;

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
        CheckGround();
        ApplyMovement();
        
    }
    private void ApplyMovement()
    {
        
        float scalelevel = Mathf.Log(Mathf.Abs(transform.localScale.x / constants.MonoScale), constants.AllScale);
        velocity = Quaternion.Euler(0, 0, -constants.Rotoffset * scalelevel) * rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        if (direction != Vector2.zero)
        {
            velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y) ;
        }
        //else
        //{
            
        //}
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
        if (hit)
        {
            Debug.DrawRay(hit.point, hit.normal * 100, Color.green);
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        float scalelevel = Mathf.Log(Mathf.Abs(player.transform.localScale.x / constants.MonoScale), constants.AllScale);
        rb.AddForce(Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up * constants.JumpForce * Mathf.Pow(constants.AllScale, scalelevel), ForceMode2D.Impulse);


    }
    private void CheckGround()
    {
       //TODO
    }
}
