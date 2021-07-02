using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CraftingItem
{
    public Item item;
    [Range(1,5)]
    public int amount;
}

[CreateAssetMenu(fileName = "New Recipes", menuName = "Crafting/Recipes")]
public class CraftingRecipes : ScriptableObject
{
    public List<CraftingItem> Materials;
    public CraftingItem Results;

    public bool CanCraft()
    {
        foreach(StackItem si in Inventory.instance.items)
        {
            foreach(CraftingItem ci in Materials)
            {
                if (si.item.name == ci.item.name)
                {
                    if(si.curAmount >= ci.amount)
                    {
                        //enough materials
                        return true;
                    }
                    else
                    {
                        Debug.Log("NOT ENOUGH MATERIAL");
                        //not enough
                        return false;
                    }
                }
            }
        }
        Debug.Log("NOT ENOUGH MATERIAL OUTER");
        return false;
    }

    public void Craft()
    {
        if (CanCraft() == true)
        {
            //check is it full
            if(Inventory.instance.CheckIsFull() == false && Inventory.instance.CheckStackFull(Results) == false) //check inventorty is full? then stack is full?
            {
                //craft and calculation
                //Add Material into inventory (Because inventory has a check system ald)
                Inventory.instance.Add(Results.item);

                //minus amount in inventory
                foreach (StackItem si in Inventory.instance.items)
                {
                    foreach (CraftingItem ci in Materials)
                    {
                        if (si.item.name == ci.item.name)
                        {
                            if (si.curAmount >= ci.amount)
                            {
                                for (int count = 0; count < ci.amount; count++)
                                {
                                    Inventory.instance.Remove(ci.item);
                                }
                            }
                        }
                    }
                }
                Debug.Log("CRAFTED");
                //update the inventory ui (INVENTORY ADD AND REMOVE PROVIDED ON CALL TO UPDATE UI)
            }
            Debug.Log("FULL STACK/INVENTORY");
            //else is not begin do anything and debug (NOT ENOUGH)
        }
        else
        {
            Debug.Log("NOT ENOUGH MATERIAL XXXX");
            //debug/show not enough/unable to craft
        }
    }

}
