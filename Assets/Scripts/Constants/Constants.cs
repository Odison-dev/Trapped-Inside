using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : ScriptableObject
{
    [Header("缩放")]
    public float MonoScale = .4f;

    [Header("移动相关参数")]
    public float MaxWalkSpeed = 14f;

    [Header("跳跃相关参数")]
    public float FallAcceleration = 1f;
    public float GroundDeceleration = 1.0f;
    public float Jumpspeed = 6f;
    public float CoyoteTime = .15f;
}
