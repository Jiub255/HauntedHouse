using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    [SerializeField]
    private float lifetimeLength = 2f;
    private float lifetimeTimer;

    private Rigidbody2D rb;

    public AudioClip hitClip;

    public static event Action onHitVampire;

    private void OnEnable()
    {
        lifetimeTimer = lifetimeLength;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lifetimeTimer -= Time.deltaTime;

        if (lifetimeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Setup(Vector2 direction, Vector3 orientation)
    {
        rb.velocity = direction.normalized * speed;
        transform.rotation = Quaternion.Euler(orientation);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            if (collision.CompareTag("Vampire"))
            {
                onHitVampire?.Invoke();

                MasterSingleton.Instance.AudioManager.PlaySoundEffect(hitClip);
            }

            gameObject.SetActive(false);
            Debug.Log("deactivated projectile");
        }
    }
}