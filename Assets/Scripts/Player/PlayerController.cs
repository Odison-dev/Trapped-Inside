using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInputController inputController;
    // Start is called before the first frame update
    private Constants constants;
    private Rigidbody2D rb;
    public Vector2 direction;
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
        ApplyMovement();
    }
    private void ApplyMovement()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //if ()
        if (direction != Vector2.zero)
        {
            
            rb.velocity = new Vector2(direction.x * constants.MaxWalkSpeed, rb.velocity.y);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        
    }
}
