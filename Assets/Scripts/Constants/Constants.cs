using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : ScriptableObject
{
    [Header("变换")]
    public float MonoScale = 5f;
    public Vector3 PosOffset = new Vector3 (0, 0, 0);
    public float Rotoffset = 0f;
    public float AllScale = .33f;

    [Header("移动相关参数")]
    public float MaxWalkSpeed = 40f;

    [Header("跳跃相关参数")]
    public float Gravity = 10f;
    public float GroundDeceleration = 1.0f;
    public float Jumpspeed = 6f;
    public float CoyoteTime = .15f;
}
