using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;

    [Header("DESCRIPTION")]
    public GameObject Panel;
    public Image image_Icon;
    public TextMeshProUGUI name_item;
    public TextMeshProUGUI description_item;

    Item item;

    private void Start()
    {
        setDescriptionActiveState(false);
    }

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
        //OnRemoveItem();
        if (item != null)
        {
            setDescriptionActiveState(true);
            displayDescription();
        }
        //if (item != null)
        //{
        //    Debug.Log(item.name);
        //    item.Use();

        //}
    }

    public void setDescriptionActiveState(bool activeState)
    {
        Panel.gameObject.SetActive(activeState);
        image_Icon.gameObject.SetActive(activeState);
        name_item.gameObject.SetActive(activeState);
        description_item.gameObject.SetActive(activeState);
    }

    void displayDescription()
    {
        image_Icon.sprite = item.Icon;
        name_item.text = "Item Name: " + item.name;
        description_item.text = "Item Description: " + item.description;
    }
}
