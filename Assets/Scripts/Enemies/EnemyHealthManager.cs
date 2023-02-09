using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    private int maxHealth = 3;
    [SerializeField]
    private int currentHealth = 3;

    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip deathClip;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        Projectile.onHitVampire += TakeDamage;
    }

    private void OnDisable()
    {
        Projectile.onHitVampire -= TakeDamage;
    }

    private void TakeDamage()
    {
        MasterSingleton.Instance.AudioManager.PlaySoundEffect(hitClip);

        currentHealth--;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        MasterSingleton.Instance.AudioManager.PlaySoundEffect(deathClip);
        gameObject.SetActive(false);
    }
}