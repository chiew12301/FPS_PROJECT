using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParents;
    public GameObject inventoryUI;
    public GameObject CraftingUI_OBJ;
    public GameObject inventoryUIForButton;
    public GameObject[] buttonObject;

    [Header("This is for Quest Pointer Objects")]
    public GameObject[] questObjects;

    public GameObject PauseMenuCanvas;

    public Animator canvas_animator;

    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        inventoryUI.SetActive(false);
        CraftingUI_OBJ.SetActive(false);
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
        if(PauseManager.instance.getUISTATE() == PAUSEUI.NONEPAUSE || PauseManager.instance.getUISTATE() == PAUSEUI.INVENTORYUI)
        {
            if(PauseManager.instance.getIsPlayingAni() == false)
            {
                if (Input.GetKeyDown(KeyCode.I))
                {
                    for (int i = 0; i < buttonObject.Length; i++)
                    {
                        buttonObject[i].gameObject.SetActive(!buttonObject[i].gameObject.activeSelf);
                    }
                    inventoryUI.SetActive(!inventoryUI.activeSelf);
                    CraftingUI_OBJ.SetActive(!CraftingUI_OBJ.activeSelf);
                    if (CraftingUI_OBJ.activeSelf == true)
                    {
                        CraftingUI_OBJ.GetComponent<CraftingUI>().AssignToSlots();
                        for (int j = 0; j < questObjects.Length; j++)
                        {
                            questObjects[j].SetActive(false);
                        }
                        PauseManager.instance.ChangeUISTATE(PAUSEUI.INVENTORYUI);
                    }
                    if (inventoryUI.activeSelf == false)
                    {
                        for (int i = 0; i < slots.Length; i++)
                        {
                            slots[i].setDescriptionActiveState(false);
                        }
                        for (int j = 0; j < questObjects.Length; j++)
                        {
                            questObjects[j].SetActive(true);
                        }
                        PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE);
                    }
                }
                if(PauseManager.instance.getUISTATE() == PAUSEUI.INVENTORYUI)
                {
                    if(Input.GetKeyDown(KeyCode.Escape))
                    {
                        for (int i = 0; i < buttonObject.Length; i++)
                        {
                            buttonObject[i].gameObject.SetActive(!buttonObject[i].gameObject.activeSelf);
                        }
                        inventoryUI.SetActive(!inventoryUI.activeSelf);
                        CraftingUI_OBJ.SetActive(!CraftingUI_OBJ.activeSelf);
                        if (inventoryUI.activeSelf == false)
                        {
                            for (int i = 0; i < slots.Length; i++)
                            {
                                slots[i].setDescriptionActiveState(false);
                            }
                            for (int j = 0; j < questObjects.Length; j++)
                            {
                                questObjects[j].SetActive(true);
                            }
                        }
                        PauseManager.instance.ChangeUISTATE(PAUSEUI.NONEPAUSE);
                    }
                }
            }
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
