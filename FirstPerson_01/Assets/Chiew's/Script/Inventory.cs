using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackItem
{
    public int curAmount = 0;
    public Item item;
}

public class Inventory : MonoBehaviour
{
    #region SINGLETON_INVENTORY
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance is creating, Inventory!");
            return;
        }
        instance = this;
    }

    #endregion SINGLETON_INVENTORY

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    public int space = 5;

    public List<StackItem> items = new List<StackItem>();

    public bool Add(Item item)
    {
        if(!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough Room.");
                return false;
            }

            StackItem tempitem = new StackItem();
            tempitem.item = item;
            tempitem.curAmount = 1;
            bool isAdded = false;
            foreach (StackItem si in items)
            {
                if (si.item == tempitem.item)
                {
                    if(si.curAmount + tempitem.curAmount > item.maxStackAmount)
                    {
                        return false;
                    }
                    else
                    {
                        si.curAmount += tempitem.curAmount;
                        isAdded = true;
                    }
                }
            }

            if(isAdded == false)
            {
                items.Add(tempitem);
            }

            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }

        }
        return true;
    }

    public void Remove(Item item)
    {
        foreach (StackItem si in items)
        {
            if (si.item == item)
            {
                if (si.curAmount > 0)
                {
                    si.curAmount --;
                }


                if (si.curAmount <= 0)
                {
                    items.Remove(si);
                    if (onItemChangedCallBack != null)
                    {
                        onItemChangedCallBack.Invoke();
                    }
                    return;
                }
                
            }
        }

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

}
