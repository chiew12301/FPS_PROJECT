using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;

    Item item;

    public void AddItem(Item newItem, int itemAmount)
    {
        item = newItem;

        icon.sprite = item.Icon;
        icon.enabled = true;
        amount.text = itemAmount.ToString();
        amount.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amount.text = "0";
        amount.enabled = false;
    }
    
    public void OnRemoveItem()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        OnRemoveItem();


        //if (item != null)
        //{
        //    Debug.Log(item.name);
        //    item.Use();
            
        //}
    }
}
