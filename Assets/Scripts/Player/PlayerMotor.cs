using System.Collections;
using System.Collections.Generic;
using Trapped_Inside.Param;
using Trapped_Inside.Tools.Timer;
using Trapped_Inside.Tools.FSM;
using UnityEngine;
//using 
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private Constants constants;
    private PlayerParam playerparam;

    private bool IsGrounded; //一个布尔值，标志是否落地

    [SerializeField] private Rigidbody2D rb;
    
    private FSM fsm;
    private Vector2 velocity;
    // Start is called before the first frame update
    private void Awake()
    {
        fsm = new FSM();
        playerparam = new PlayerParam();

        constants = new Constants();
        fsm.AddState(StateType.Idle, new PlayerIdleState(fsm));
        fsm.AddState(StateType.Move, new PlayerMoveState(fsm));
        fsm.SetDefault(new PlayerIdleState(fsm));
        
            
    }

    // Update is called once per frame
    private void Update()
    {
        //if (fsm == null)
        //{
        //    print("fsm is null");
        //}
        //else
        //{
        fsm.OnUpdate();
        Debug.Log(fsm.currentState);
        Move();
        ApplyVelocity();
        //}
            
    }

    private void Move()
    {
        print(playerparam.inputdir);
        //velocity += constants.FallAcceleration‘
        float deceleration = constants.GroundDeceleration;
        if (playerparam.inputdir.x != 0)
        {
            velocity = new Vector2( constants.MaxWalkSpeed * playerparam.inputdir.x, rb.velocity.y);
        }
        else
        {
            print("Not work!");
        }
    }

    private void ApplyVelocity()
    {
        rb.velocity = velocity;
    }

    private void CollisionCheck()
    {
        Physics2D.queriesStartInColliders = false;
    }
    private void HandleJump()
    {

    }
    private void Jump()
    {

    }
}
