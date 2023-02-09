using System;
using UnityEngine;

public class Hide : MonoBehaviour
{
    private bool canHide = false;
    public bool isHidden { get; private set; } = false;
    private Animator animator;
    private int hidingStage = 0;
    private GameObject hideableObject;
    private Rigidbody2D rb;
    private float hideSpeed = 8f;
    public static event Action onChangeLayer;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canHide)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                // start hiding process - walk to the side of object to "go around"
                hidingStage = 1;
                canHide = false;

                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                animator.SetFloat("horiz", -1);
                animator.SetFloat("lastHoriz", -1);
            }
        }

        if (hidingStage == 2)
        {
            if (Mathf.Abs(transform.position.x - hideableObject.transform.position.x) <= 0.1f)
            {
                hidingStage = 3;
                isHidden = true;

                // necessary?
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (hidingStage == 1)
        {
            rb.velocity = new Vector2(-hideSpeed, 0f);
        }

        if (hidingStage == 2)
        {
            rb.velocity = new Vector2(hideSpeed, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            canHide = true;

            hideableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hideable"))
        {
            canHide = false;

            if (hidingStage == 1)
            {
                // put player on "Hide" layer and sorting layer
                gameObject.layer = LayerMask.NameToLayer("Hide");
                gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Hide");

                // send signal to enemies that can see you to switch layers too
                onChangeLayer?.Invoke();

                // change player direction
                animator.SetFloat("horiz", 1);
                animator.SetFloat("lastHoriz", 1);
                gameObject.transform.localScale = new Vector3(1, 1, 1);

                // set to next hidingStage
                hidingStage = 2;
            }

            else if (hidingStage == 3)
            {
                // put player back on Default layer and sorting layer
                gameObject.layer = LayerMask.NameToLayer("Default");
                gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Default");

                // send signal to enemies that can see you to switch layers too
                onChangeLayer?.Invoke();

                isHidden = false;

                hidingStage = 0;
            }
        }
    }
}