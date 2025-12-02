using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public SceneLoadEvent loadEvent;
    public GameScene sceneToGo;
    public Vector3 pos;

    public Animator transition;


    //public void 
    public void TriggerAction()
    {
        Debug.Log("changing scenes");
        
        loadEvent.LoadRequestEvent(sceneToGo, pos, true);
        if (transition != null)
        {
            transition.SetTrigger("Start");
        }
        //this.GetComponent<Button>().interactable = false;
    }
}
