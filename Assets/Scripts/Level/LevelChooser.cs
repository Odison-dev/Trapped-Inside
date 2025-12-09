using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelChooser : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEvent loadEvent;
    [Header("事件广播")]
    public VoidEvent intoTheLevel;

    [Header("关卡参数")]
    public GameScene levelToGo;
    public Vector3 posToGo = Vector3.zero;
    //public Animator transition;

    private void OnEnable()
    {
        intoTheLevel.OnEventRaised += IntoTheGame;
        //intoTheLevel.OnEventRaised += 
    }

    private void OnDisable()
    {
        intoTheLevel.OnEventRaised -= IntoTheGame;
    }

    public void IntoTheGame()
    {
        //transition.SetTrigger("Start");
        loadEvent.LoadRequestEvent(levelToGo, posToGo, true);
    }
}
