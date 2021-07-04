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
        AssignToSlots();
    }

    void AssignToSlots()
    {
        for(int i = 0; i < maxSlots; i++)
        {
            if(listOfReceipes[i] != null)
            {
                if(listOfReceipes[i].CanCraft() == true)
                {
                    slots[i].AddItem(listOfReceipes[i].Results.item, listOfReceipes[i].Results.amount);
                    slots[i].GetComponentInChildren<Button>().interactable = true;
                }
            }
            else
            {
                slots[i].GetComponentInChildren<Button>().interactable = false;
            }
        }
        for (int i = 0; i < maxSlots; i++)
        {
            if (listOfReceipes[i] != null)
            {
                if (listOfReceipes[i].CanCraft() == false)
                {
                    slots[i].AddItem(listOfReceipes[i].Results.item, listOfReceipes[i].Results.amount);
                    slots[i].GetComponentInChildren<Button>().interactable = false;
                }
            }
            else
            {
                slots[i].GetComponentInChildren<Button>().interactable = false;
            }
        }
    }

}
