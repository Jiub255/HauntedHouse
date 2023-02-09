using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class PickUpItem : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemSO;

    public static event Action<ItemSO> onItemPickedUp;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = itemSO.itemIconSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // send signal to InventoryManager and UI
            onItemPickedUp?.Invoke(itemSO);

            // deactivate object
            gameObject.SetActive(false);

            // send signal to AudioManager
        }
    }
}