using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputController;
    // Start is called before the first frame update
    private Constants constants;
    private Rigidbody2D rb;
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
        transform.localScale = Vector3.one * constants.MonoScale;
        rb.gravityScale = constants.Gravity;
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


    private void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
    }
    private void ApplyMovement()
    {
        
        float scalelevel = Mathf.Log(Mathf.Abs(transform.localScale.x / constants.MonoScale), constants.AllScale);
        velocity = Quaternion.Euler(0, 0, -constants.Rotoffset * scalelevel) * rb.velocity;
        print(scalelevel);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        if (direction != Vector2.zero)
        {
            velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y) ;
        }
        else
        {
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        rb.velocity = Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * velocity;
        print(velocity + "and also gravity  " + rb.gravityScale);
    }

    private void CheckGround()
    {

    }
}
