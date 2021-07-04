using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amount;
    public Button craft_Button;

    [Header("DESCRIPTION")]
    public Image image_Icon;
    public TextMeshProUGUI name_item;
    public TextMeshProUGUI description_item;


    public CraftingUI craftingUI_OBJ;

    public bool forUI = false;
    Item item;
    CraftingRecipes craftingRes;

    // Start is called before the first frame update
    void Start()
    {
        setDescriptionActiveState(false);
    }

    private void Update()
    {
        if(item !=null)
        {
            if(name_item.gameObject.activeSelf == true)
            {
                if (name_item.text == "Item Name: " + item.name)
                {
                    craft_Button.gameObject.SetActive(true);
                }
                else
                {
                    craft_Button.gameObject.SetActive(false);
                }
            }
            else
            {
                craft_Button.gameObject.SetActive(false);
            }

        }
        else
        {
            craft_Button.gameObject.SetActive(false);
        }
    }

    public void AddItem(Item newItem, int itemAmount, CraftingRecipes cr)
    {
        item = newItem;
        craftingRes = cr;
        icon.sprite = item.Icon;
        icon.enabled = true;
        amount.text = itemAmount.ToString();
        amount.enabled = true;
        //craft_Button.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        craftingRes = null;
        icon.sprite = null;
        icon.enabled = false;
        amount.text = "0";
        amount.enabled = false;
        craft_Button.interactable = false;
        craft_Button.gameObject.SetActive(false);
    }

    public void OnButtonPress()
    {
        if(item!=null)
        {
            setDescriptionActiveState(true);
            displayDescription();
        }
    }

    public void CraftButtonPress()
    {
        if(item != null)
        {
            craftingRes.Craft();
            craftingUI_OBJ.AssignToSlots();
        }
    }

    public void setDescriptionActiveState(bool activeState)
    {
        image_Icon.gameObject.SetActive(activeState);
        name_item.gameObject.SetActive(activeState);
        description_item.gameObject.SetActive(activeState);
    }

    void displayDescription()
    {
        if(item!=null)
        {
            image_Icon.sprite = item.Icon;
            name_item.text = "Item Name: " + item.name;
            description_item.text = "Item Description: " + item.description;
        }
    }

    public string getItemName()
    {
        return item.name;
    }
}
