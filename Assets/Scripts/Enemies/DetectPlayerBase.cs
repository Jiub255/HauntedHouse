using System.Collections;
using UnityEngine;

public abstract class DetectPlayerBase : MonoBehaviour, IDetectPlayer
{
    public bool canSeePlayer = false;
    public bool justSawPlayer = false;
    public bool justStoppedSeeingPlayer = false;
    public Transform playerTransform;
    public float moveSpeed = 1f;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("DetectPlayerBase detected player");

            playerTransform = collision.transform;

            if (!collision.gameObject.GetComponent<Hide>().isHidden)
            {
                OnDetectPlayer();
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Hide>())
            {
                OnStopDetectingPlayer();
            }
        }
    }

    public virtual void PlayerNearAction()
    {

    }

    public virtual void PlayerFarAction()
    {
        
    }

    public virtual void OnDetectPlayer()
    {
        Debug.Log("DetectPlayerBase OnDetectPlayer called");
        canSeePlayer = true;
        justSawPlayer = true;
        StartCoroutine(JustSawPlayer());
    }

    public virtual void OnStopDetectingPlayer()
    {
        Debug.Log("DetectPlayerBase OnStopDetectingPlayer called");
        canSeePlayer = false;
        justStoppedSeeingPlayer = true;
        StartCoroutine(JustStoppedSeeingPlayer());
    }

    IEnumerator JustSawPlayer()
    {
        yield return new WaitForEndOfFrame();
        justSawPlayer = false;
    }

    IEnumerator JustStoppedSeeingPlayer()
    {
        yield return new WaitForEndOfFrame();
        justStoppedSeeingPlayer = false;
    }
}