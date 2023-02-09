using UnityEngine;

// have usable item and equipment item subclasses
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName = "New Item";
    [TextArea(3, 20)]
    public string itemDescription = "Item Description";

    public Sprite itemIconSprite = null;

    public virtual void Use()
    {
        // use the item
        Debug.Log("Using " + name);
    }
}