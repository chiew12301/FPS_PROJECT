using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public Transform slotParents;

    public int maxSlots = 10;
    public List<CraftingRecipes> listOfReceipes;

    CraftingSlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = slotParents.GetComponentsInChildren<CraftingSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        //AssignToSlots();
    }

    public void AssignToSlots()
    {
        for(int i = 0; i < maxSlots; i++)
        {
            slots[i].ClearSlot();
            if(listOfReceipes[i] != null)
            {
                slots[i].AddItem(listOfReceipes[i].Results.item, listOfReceipes[i].Results.amount, listOfReceipes[i]);
                if (listOfReceipes[i].CanCraft() == true)
                {
                    if (Inventory.instance.CheckIsFull() == false && Inventory.instance.CheckStackFull(listOfReceipes[i].Results) == false)
                    {
                        slots[i].craft_Button.interactable = true;
                        //slots[i].GetComponentInChildren<Button>().interactable = true;
                    }
                    else
                    {
                        slots[i].craft_Button.interactable = false;
                        //slots[i].GetComponentInChildren<Button>().interactable = false;
                    }
                }
                else
                {
                    slots[i].craft_Button.interactable = false;
                    //slots[i].GetComponentInChildren<Button>().interactable = false;
                }
            }
            else
            {
                slots[i].craft_Button.interactable = false;
                //slots[i].GetComponentInChildren<Button>().interactable = false;
                slots[i].ClearSlot();
            }
        }
        //for (int i = 0; i < maxSlots; i++)
        //{
        //    if (listOfReceipes[i] != null)
        //    {
        //        if (listOfReceipes[i].CanCraft() == false)
        //        {
        //            slots[i].AddItem(listOfReceipes[i].Results.item, listOfReceipes[i].Results.amount, listOfReceipes[i]);
        //            slots[i].GetComponentInChildren<Button>().interactable = false;
        //        }
        //    }
        //    else
        //    {
        //        slots[i].GetComponentInChildren<Button>().interactable = false;
        //        slots[i].ClearSlot();
        //    }
        //}
    }
    
}
