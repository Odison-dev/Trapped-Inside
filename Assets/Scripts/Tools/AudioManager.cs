using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("事件监听")]
    public PlayAudioEvent PlayBGM;
    public PlayAudioEvent PlayChoose;
    public PlayAudioEvent PlayWin;
    public PlayAudioEvent PlayKeyCollected;
    public PlayAudioEvent PlayDoorOpen;

    [Header("音乐与音效")]
    public AudioSource BGM;
    public AudioSource choose;
    public AudioSource win;
    public AudioSource key;
    public AudioSource door;

    private void OnEnable()
    {
        PlayBGM.OnEventRaise += OnBGMEvent;
        PlayChoose.OnEventRaise += OnButtonPressedEvent;
        PlayWin.OnEventRaise += OnWinEvent;
        PlayKeyCollected.OnEventRaise += OnKeyCollectedEvent;
        PlayDoorOpen.OnEventRaise += OnDoorOpenEvent;
    }
    private void OnDisable()
    {
        PlayBGM.OnEventRaise -= OnBGMEvent;
        PlayChoose.OnEventRaise -= OnButtonPressedEvent;
        PlayWin.OnEventRaise -= OnWinEvent;
        PlayKeyCollected.OnEventRaise -= OnKeyCollectedEvent;
        PlayDoorOpen.OnEventRaise -= OnDoorOpenEvent;
    }

    private void OnButtonPressedEvent(AudioClip clip)
    {
        choose.clip = clip;
        //BGM.loop = true;
        choose.Play();
    }

    private void OnBGMEvent(AudioClip clip)
    {
        //AudioClip blank = AudioClip.Create("Blank", 44100 * 2, 1 , 44100, true);
        //BGM.clip = clip + blank;
        BGM.clip = clip;
        BGM.loop = true;
        //BGM.time = 134;
        //print(BGM.time);
        //if (BGM.time >= 136)
        //{
        //    BGM.time = 64;
        //}
        BGM.Play();
    }

    private void OnWinEvent(AudioClip clip)
    {
        win.clip = clip;
        win.Play();
    }

    private void OnKeyCollectedEvent(AudioClip clip)
    {
        key.clip = clip;
        key.Play();
    }

    private void OnDoorOpenEvent(AudioClip clip)
    {
        door.clip = clip;
        door.Play();
    }
}
