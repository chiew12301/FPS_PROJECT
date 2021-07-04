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
        bool isContain = false;
        bool isEnough = true;
        for (int i = 0; i < Materials.Count; i++)
        {
            bool DoIHaveTheItem = false;
            CraftingItem tempmat = Materials[i];
            for(int j = 0; j < Inventory.instance.items.Count; j++)
            {
                StackItem temp = Inventory.instance.items[j];
                Debug.Log("MATITEM " + i + " for " + Results.item.name + " :" + tempmat.item.name);
                Debug.Log("ITEM " + j + " for " + Results.item.name + " :" + temp.item.name);
                if (temp.item.name == tempmat.item.name)
                {
                    isContain = true;
                    DoIHaveTheItem = true;
                    if (temp.curAmount < tempmat.amount)
                    {
                        isEnough = false;
                    }
                }
                else if (temp.item.name != tempmat.item.name)
                {
                    //not the item
                }
            }
            if(DoIHaveTheItem == false)
            {
                isContain = false;
                //means i dont have the item
            }
        }

        if (isEnough == false)
        {
            Debug.Log(Results.item.name + " NOT ENOUGH MATERIAL");
            //not enough
        }

        if (isContain == true && isEnough == true)
        {
            //enough materials
            Debug.Log( Results.item.name + " MATERIAL ENOUGH");
            return true;
        }
        else
        {
            Debug.Log(Results.item.name + " NOT CONTAIN");
            return false;
        }
    }

    public void Craft()
    {
        if (CanCraft() == true)
        {
            //check is it full
            if(Inventory.instance.CheckIsFull() == false && Inventory.instance.CheckStackFull(Results) == false) //check inventorty is full? then stack is full?
            {
                //craft and calculation
                //minus amount in inventory
                foreach(CraftingItem ci in Materials)
                {
                    for (int count = 0; count < ci.amount; count++)
                    {
                        Inventory.instance.Remove(ci.item);
                    }
                }
                //Add Material into inventory (Because inventory has a check system ald)
                Inventory.instance.Add(Results.item);
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
