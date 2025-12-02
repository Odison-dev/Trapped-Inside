using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/SceneLoadEvent")]
public class SceneLoadEvent : ScriptableObject
{
    public UnityAction<GameScene, Vector3, bool> LoadRequestEvent;
    private void RaiseLoadRequestEvent(GameScene location, Vector3 posToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(location, posToGo, fadeScreen);
    }
}
