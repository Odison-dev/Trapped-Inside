using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeCanvas : MonoBehaviour
{
    public Animator In;
    public Animator Out;
    [Header("ÊÂ¼þ¼àÌý")]
    public SceneTransition transition;

    public Image fadeIn;


    private void OnEnable()
    {
        transition.OnEventRaised += OnFadeEvent;
    }

    private void OnDisable()
    {
        transition.OnEventRaised -= OnFadeEvent;
    }
    private void OnFadeEvent(Color endColor, float duration, bool InOrOut)
    {
        fadeIn.DOBlendableColor(endColor, duration);
    }
}
