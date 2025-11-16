using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trapped_Inside.Param; 
using UnityEngine.InputSystem;

namespace Trapped_Inside.Input
{
    public class PlayerInputControl : MonoBehaviour
    {
        private PlayerInputController inputcontroller;
        private PlayerParam param;

        //public Vector2 inputdir;


        public void Construct(PlayerParam playerparam)
        {
            param = playerparam;
        }
        private void Awake()
        {
            inputcontroller = new PlayerInputController();

            inputcontroller.PlayerMovement.Move.performed += ctx => param.inputdir = ctx.ReadValue<Vector2>().normalized;
            inputcontroller.PlayerMovement.Move.canceled += _ => param.inputdir = Vector2.zero;
        }

        private void FixedUpdate()
        {

        }

        //private void Update()
        //{
        //    inputdir = inputcontroller.Player.Move.ReadValue<Vector2>();
        //}
        private void OnEnable()
        {
            inputcontroller.Enable();
        }
        private void OnDisable()
        {
            inputcontroller.Disable();
        }
    }
}










    

    

