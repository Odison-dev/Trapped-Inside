using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Trapped_Inside.Tools.FSM;
using Trapped_Inside.Tools.Timer;

public class PlayerIdleState : IState
{
    
     
    private FSM fsm;
    public PlayerIdleState(FSM fsm)
    {
        this.fsm = fsm;

    }

   
    public void OnEnter()
    {
         //ûд
    }
    public void OnUpdate()
    {
       
        
        fsm.SwitchState(StateType.Move);
        
    }
    public  void OnExit()
    {
        
        //ûд
    }

}


