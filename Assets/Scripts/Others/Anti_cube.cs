using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anti_cube : MonoBehaviour
{
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D smooth;
    public GameObject c;
    public GameObject levelCloner;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    private Consts constants;
    private Vector2 gravity;
    private GameObject cs;
    private GameObject cb;

    private void Awake()
    {
        //cs = Instantiate(gameObject);
        //SceneManager.MoveGameObjectToScene(cs, gameObject.scene);
        //cb = Instantiate(gameObject);
        //SceneManager.MoveGameObjectToScene(cb, gameObject.scene);
        constants = c.GetComponent<Consts>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0;
        //gravity = new Vector2 (0, constants.Gravity) * 10;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Circle")
        {
            if (collision.transform.position.z - transform.position.z > 0)
            {
                collider.sharedMaterial = smooth;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collider.sharedMaterial = normal;
    }

    private void FixedUpdate()
    {
        //CloneBoxes();
        float scalelevel = Mathf.Log(Mathf.Abs(transform.localScale.x / constants.MonoScale), constants.AllScale);
        
    }

    private void CloneBoxes()
    {
        levelCloner.GetComponent<LevelCloner>().TransformGameobj("lower", cs.transform, gameObject.transform);
        levelCloner.GetComponent<LevelCloner>().TransformGameobj("upper", cb.transform, gameObject.transform);
    }
}
