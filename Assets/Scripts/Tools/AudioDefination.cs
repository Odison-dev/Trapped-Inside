using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public PlayAudioEvent PlayAudioEvent;
    public AudioClip AudioClip;
    public bool playOnEnable;
    

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        PlayAudioEvent.OnEventRaise(AudioClip);
    }    
}
