using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consts : MonoBehaviour
{
    [Header("变换")]
    public float MonoScale = .5f;
    public Vector3 PosOffset = new Vector3(0, 0, 0);
    public float Rotoffset = 90f;
    public float AllScale = .4f;

    [Header("移动相关参数")]
    public float MaxWalkSpeed = 4f;
    public float MaxWalkAngle = 45f;

    [Header("跳跃相关参数")]
    public float Gravity = 1f;
    public float GroundDeceleration = 1.0f;
    public float JumpForce = 6f;
    public float CoyoteTime = .15f;
}
