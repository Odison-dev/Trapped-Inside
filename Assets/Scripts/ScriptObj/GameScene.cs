using UnityEngine;
using UnityEngine.AddressableAssets;


[CreateAssetMenu(menuName = "Game scene/GameScene")]
public class GameScene : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}
