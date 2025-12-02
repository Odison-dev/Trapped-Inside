using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransform : MonoBehaviour
{
    public SceneLoadEvent loadEvent;
    public GameScene targetScene;
    public Vector3 posToGo;

    public void Trans()
    {
        loadEvent.LoadRequestEvent(targetScene, posToGo, true);
    }
}
