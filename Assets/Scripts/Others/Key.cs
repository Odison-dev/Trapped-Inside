using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class Key : MonoBehaviour
{
    private GameObject player;
    private Tweener tweener;
    public AudioDefination audioDefination;
   
    public GameObject constants;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    public bool got = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        //spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        //spriteRenderer.size = new Vector2(.2f, .3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            got = true;
            StartCoroutine(CollectedBehaviours());
            StopCoroutine(CollectedBehaviours());

        }
    }

    private IEnumerator CollectedBehaviours()
    {
        Sequence sequence = DOTween.Sequence();
        boxCollider.enabled = false;
        audioDefination.PlayAudio();
        //spriteRenderer.enabled = false;
        sequence.Append(transform.DOScaleY(0f, .3f));
        sequence.Append(transform.DOScaleX(0f, .4f));
        yield return new WaitForSeconds(.7f);
        Destroy(gameObject);
        yield return null;
    }
}

    
