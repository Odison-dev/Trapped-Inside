using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/PlayAudioEvent")]
public class PlayAudioEvent : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaise;

    public void RaiseEvent(AudioClip clip)
    {
        OnEventRaise?.Invoke(clip);
    }
}
