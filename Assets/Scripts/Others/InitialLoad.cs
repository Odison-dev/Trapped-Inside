using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitialLoad : MonoBehaviour
{
    public AssetReference perspective;

    private void Awake()
    {
        Addressables.LoadSceneAsync(perspective);
    }
}
