using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("事件监听")]
    public SceneLoadEvent loadEvent;
    public GameScene firstloadscene;
    public VoidEvent Level1;

    [Header("事件广播")]
    public SceneTransition transition;


    [SerializeField] private GameScene currentLoadedScene;
    private GameScene sceneToLoad;
    private Vector3 positionToGo;
    private bool sceneTransition;
    private bool loading;
    public float fadeTime;
     
    //public float fadeTimeIn;
    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstloadscene.sceneReference, LoadSceneMode.Additive);
        currentLoadedScene = firstloadscene;
        currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void OnEnable()
    {
        loadEvent.LoadRequestEvent += OnLoadRequestEvent;
        //Level1.OnEventRaised += ;
    }

    private void OnDisable()
    {
        loadEvent.LoadRequestEvent -= OnLoadRequestEvent;
        //Level1.OnEventRaised -=;
    }

    private void Level()
    {

    }


    private void OnLoadRequestEvent(GameScene location, Vector3 posToGo, bool fadeScreen)
    {
        if (loading)
        {
            return;
        }
        loading = true;
        sceneToLoad = location;
        positionToGo = posToGo;
        this.sceneTransition = fadeScreen;

        if (currentLoadedScene != null)
        {
            Debug.Log("xxx");
            StartCoroutine(UnloadPreviousScene());
        }


        //Debug.Log(sceneToLoad.sceneReference.SubObjectName);
    }

    private IEnumerator UnloadPreviousScene()
    {
        if (sceneTransition)
        {
            transition.SceneTransIn(fadeTime);
        }
        yield return new WaitForSeconds(fadeTime);
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        
        LoadNewScene();
        
    }

    private void LoadNewScene()
    {
        var loadingOption = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadedScene = sceneToLoad;
        if (sceneTransition)
        {
            transition.SceneTransOut(fadeTime);
        }

        loading = false;
    }
}