using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParents;
    public GameObject inventoryUI;
    public GameObject CraftingUI;
    public GameObject inventoryUIForButton;
    public GameObject[] buttonObject;

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        inventoryUI.SetActive(false);
        CraftingUI.SetActive(false);
        for (int i = 0; i < buttonObject.Length; i++)
        {
            buttonObject[i].gameObject.SetActive(false);
        }
        slots = itemParents.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            for (int i = 0; i < buttonObject.Length; i++)
            {
                buttonObject[i].gameObject.SetActive(!buttonObject[i].gameObject.activeSelf);
            }
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            CraftingUI.SetActive(!CraftingUI.activeSelf);
            if (inventoryUI.activeSelf == false)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].setDescriptionActiveState(false);
                }
            }     
        }

        if(inventoryUI.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void OnInventoryButton()
    {
        if(inventoryUIForButton.activeSelf == false)
        {
            inventoryUIForButton.SetActive(true);
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i].item, inventory.items[i].curAmount);
            }
            else
            {
                slots[i].ClearSlot();
            }
                
        }
    }
}
