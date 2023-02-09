using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    [SerializeField]
    private float timerLength = 0.4f;
    private float timer;
    private bool canThrow = true;

    private Transform player;

    private InventoryManager inventoryManager;

    private void Start()
    {
        player = gameObject.GetComponent<PlayerMovement>().gameObject.transform;
        inventoryManager = gameObject.GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canThrow)
            {
                ItemAmount garlicItemAmount = 
                    inventoryManager.ItemToItemAmount(inventoryManager.GetItemByName("Garlic"));

                if (garlicItemAmount != null
                    && garlicItemAmount.amount > 0)
                {
                    CreateProjectile();
                    canThrow = false;
                    timer = timerLength;
                }
            }
        }

        if (!canThrow)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                canThrow = true;
            }
        }
    }

    public void CreateProjectile()
    {
        GameObject garlicProjectile = MasterSingleton.Instance.ObjectPool.GetPooledObject("Garlic");

        if (garlicProjectile != null)
        {
            garlicProjectile.transform.position = player.position;
            garlicProjectile.transform.rotation = player.rotation;
            garlicProjectile.SetActive(true);
            inventoryManager.RemoveItem(inventoryManager.GetItemByName("Garlic"));
        }

        Vector2 directionVector = new Vector2(player.transform.localScale.x, 0);
        garlicProjectile.GetComponent<Projectile>().Setup(directionVector, Vector3.zero);
    }
}