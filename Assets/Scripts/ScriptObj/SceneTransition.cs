using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Events/SceneTransition")]
public class SceneTransition : ScriptableObject
{
    public UnityAction<Color, float, bool> OnEventRaised;

    public Animator In;
    public Animator Out;
    public void SceneTransIn(float duration)
    {
        RaiseEvent(Color.black, duration, true);
    }
    public void SceneTransOut(float duration)
    {
        RaiseEvent(Color.clear, duration, false);
    }


    public void RaiseEvent(Color target, float duration, bool InOrOut)
    {
        OnEventRaised?.Invoke(target, duration, InOrOut);
    }    
}
