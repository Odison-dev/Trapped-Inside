using System;

//using Trapped_Inside.Constants;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //private TimerManager timerManager;
    public GameObject player;
    public GameObject GroundDetector;
    public PlayerInputController inputController;
    public float SlopeCheckDistance = 1f;
    public LayerMask GroundLayer;
    public GameObject consts;
    public Transform playerTrans;
    // Start is called before the first frame update
    //private Constants constants;
    private Consts constants;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    //private CompositeCollider2D collider;
    private CapsuleCollider2D collider;

    public Vector2 direction;
    public float faceDir = 1;
    private Vector2 velocity = Vector2.zero;
    private Vector2 SpaceBase = Vector2.zero;
    private Vector2 a = Vector2.zero;
    private float k = 0f;

    private float slopeangle;
    private Vector2 slopeNormalPerp;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
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
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputController = new PlayerInputController();
        //GroundDetector = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        //collider = GetComponent<CompositeCollider2D>();
        collider = GetComponent<CapsuleCollider2D>();
        transform.localScale = new Vector3 (faceDir, 1, 1) * constants.MonoScale;
        rb.gravityScale = constants.Gravity;

        //playerTrans = gameObject.AddComponent<Transform>();
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
        if (collision.gameObject.CompareTag("bar") || collision.gameObject.CompareTag("player"))
        {
            
            Vector3 normal = collision.GetContact(0).normal;
            SlopeCheck(normal);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsOnSlope = false;
        collider.sharedMaterial = normal;
    }


    private void FixedUpdate()
    {
        //CheckGround();
        ApplyMovement();
        
    }
    private void ApplyMovement()
    {
        
        //print(Mathf.Abs(transform.localScale.x / constants.MonoScale));
        
            float scalelevel = Mathf.Log(Mathf.Abs(transform.localScale.x / constants.MonoScale), constants.AllScale);
            velocity = Quaternion.Euler(0, 0, -constants.Rotoffset * scalelevel) * rb.velocity;
        //velocity = Quaternion.Euler(0, 0, 0) * rb.velocity;
        a = new Vector2(direction.x * k * Mathf.Abs(transform.localScale.x / constants.MonoScale), 0);
            velocity += a;

        if (direction.x > 0) { faceDir = 1; }
        else if (direction.x < 0) { faceDir = -1; }
        transform.localScale = new Vector3(faceDir,1, 1) * Mathf.Abs(transform.localScale.x / constants.MonoScale) * constants.MonoScale;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (GroundDetector.GetComponent<GroundDetect>().IsGrounded && !IsOnSlope)
            {
                if (direction != Vector2.zero)
                {
                    velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y);
                }
                //else
                //{

                //}
            }

            else if (GroundDetector.GetComponent<GroundDetect>().IsGrounded && IsOnSlope)
            {
                //print("ssssss");
                if (slopeangle < 0)
                {
                    if (direction.x > 0)
                    {
                        velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y);
                    }
                }
                else if (slopeangle > 0)
                {
                    if (direction.x < 0)
                    {
                        velocity = new Vector2(direction.x * constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale), velocity.y);
                    }
                }

            }
            else
            {
                if (direction.x != 0)
                {
                    if (velocity.x * direction.x > 0) { k = .4f; }
                    else { k = .8f; }
                    if (Math.Abs(velocity.x) >= constants.MaxWalkSpeed * Mathf.Abs(transform.localScale.x / constants.MonoScale))
                    {
                        velocity.x = constants.MaxWalkSpeed * direction.x * Mathf.Abs(transform.localScale.x / constants.MonoScale);
                    }
                    else
                    {


                    }
                }

            }
            rb.velocity = Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * velocity;
        
        
        
       
    }

    //private
    private void ApplyPhysicsMaterial(string type)
    {
        if (type == "OnWall")
        {
            collider.sharedMaterial = smooth;
        }
        else if (type == "OnGround")
        {
            collider.sharedMaterial = normal;
        }
    }

    
    private void SlopeCheckHorizontal(Vector2 pos)
    {

    }
    
    private void SlopeCheck(Vector3 normalVector)
    {
        normalVector = normalVector.normalized;
        //float slopeangle;
        if (Vector3.Cross(transform.up, normalVector).normalized.z == transform.forward.z)
        {
            slopeangle = Vector3.Angle(transform.up, normalVector);
        }
        else if (Vector3.Cross(transform.up, normalVector).normalized.z == -transform.forward.z)
        {
            slopeangle = -Vector3.Angle(transform.up, normalVector);
        }
        else
        {
            slopeangle = 0f;
        }
        if (MathF.Abs(slopeangle) > 45)
        {
            IsOnSlope = true;
            collider.sharedMaterial = smooth;
        }
        else
        {
            IsOnSlope = false;
            collider.sharedMaterial = normal;
        }
        
            
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        float scalelevel = Mathf.Log(Mathf.Abs(player.transform.localScale.x / constants.MonoScale), constants.AllScale);
        if (GroundDetector.GetComponent<GroundDetect>().IsGrounded)
        {
            //print(constants.JumpForce * Mathf.Pow(constants.AllScale, scalelevel));
            rb.AddForce(Quaternion.Euler(0, 0, constants.Rotoffset * scalelevel) * Vector2.up * constants.JumpForce * Mathf.Pow(constants.AllScale, scalelevel), ForceMode2D.Impulse);
        }


    }
    
}
