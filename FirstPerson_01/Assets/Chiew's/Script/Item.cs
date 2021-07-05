using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; //name of item
    public Sprite Icon = null; //icon
    public bool isDefaultItem = false; // is the item default
    new public string description = "Item description"; //des of the item
    public int maxStackAmount = 5; //maximum stack

    public virtual void Use()
    {
        //use the item
        //some thing might happen
        //purposely to commit it so it update on github
        Debug.Log("Using" + name); 
    }

}
