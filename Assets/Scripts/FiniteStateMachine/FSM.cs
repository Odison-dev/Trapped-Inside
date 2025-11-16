using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trapped_Inside.Tools.FSM
{

    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Die,
    }
    public interface IState
    {
        void OnEnter();
        void OnExit();
        void OnUpdate();
    }
    public class FSM

    {
        public IState currentState;
        public Dictionary<StateType, IState> states;
        

        public FSM()
        {
            this.states = new Dictionary<StateType, IState>();
        }

        public void OnUpdate()
        {
            currentState.OnUpdate();
        }
        public void OnEnter()
        {
            currentState.OnEnter();
        }
        public void OnExit()
        {
            currentState.OnExit();
        }

        public void SetDefault(IState state)
        {
            currentState = state;
        }
        public void AddState(StateType statetype, IState state)
        {
            states.Add(statetype, state);
        }

        public void SwitchState(StateType statetype)
        {
            if (!states.ContainsKey(statetype))
            {
                return;
            }
            if (currentState != null)
            {
                currentState.OnExit();
            }
            currentState = states[statetype];
            currentState.OnEnter();
        }
        public void OnFixUpdate()
        {
            //currentState.OnUpdate();
        }
        //public void OnCheck()
        //{
        //    currentState.on
        //}
    }

    
}

