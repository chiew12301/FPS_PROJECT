using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; //name of item
    public Sprite Icon = null; //icon
    public bool isDefaultItem = false; // is the item default

    public virtual void Use()
    {
        //use the item
        //some thing might happen

        Debug.Log("Using" + name); 
    }

}
