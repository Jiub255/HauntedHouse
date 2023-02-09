using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    private bool playerInRange = false;
    private bool opened = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInRange = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (opened)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Change scene
                GetComponent<SceneTransition>().ChangeScene();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                opened = true;
                animator.SetTrigger("Opened");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponentInParent<BoxCollider2D>().enabled = true;
            }
        }
    }
}