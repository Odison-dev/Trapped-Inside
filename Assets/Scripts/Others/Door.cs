using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence; 

public class Door : MonoBehaviour
{
    //private Sequence sequence = DOTween.Sequence();
    public AudioDefination audioDefination;
    public GameObject targetKey;
    private BoxCollider2D collider;
    private bool open = false;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        //  正确：在Awake中初始化
        //if (!DOTween.)
        //{
        //DOTween.Init();
        //}
    }

    private void Update()
    {
        print(open);
        if (targetKey.GetComponent<Key>().got)
        {
            open = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.gameObject.name == "Circle")
        {
            if (open)
            {
                audioDefination.PlayAudio();
                print("sss");
                StartCoroutine(Unlocked());
                StopCoroutine(Unlocked());
            }
        }

        
    }
    

    private IEnumerator Unlocked()
    {
        Sequence sequence = DOTween.Sequence();
        collider.enabled = false;
        sequence.Append(transform.DOScaleY(0f, .5f));
        yield return new WaitForSeconds(.5f);
        yield return null;
    }
}
