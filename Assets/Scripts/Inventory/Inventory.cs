using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public List<Slot> slots = new List<Slot>();
    public List<Item> craftables = new List<Item>();
    public List<CraftingRecipe> craftRecipes = new List<CraftingRecipe>();
    private int currentCraft;
    [SerializeField] Image craftIcon;
    [SerializeField] TextMeshProUGUI craftName;
    [SerializeField] TextMeshProUGUI craftRecipe;
    [SerializeField] CinemachineBrain brain;

    //FOR TESTING ONLY
    [SerializeField] Item craft1;
    [SerializeField] Item craft2;

    [SerializeField] Item craft3;

    [Header("Keybinds")]
    public KeyCode inventoryKey = KeyCode.E;

    private void Start()
    {
        foreach (Slot itemslot in slots)
        {
            itemslot.InitializeSlot();
        }
        for (int i = 0; i < 4; i++)
        {
            GetItem(craft1);
            GetItem(craft2);
            GetItem(craft3);
        }
        UpdateCraft();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }

    public void GetItem(Item itemToAdd)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Item itemInSlot = slots[i].GetItem();
            if (itemInSlot != null && itemInSlot.item_name == itemToAdd.item_name)
            {
                itemInSlot.quantity += itemToAdd.quantity;
                slots[i].updateData();
                return;
            }
            else if (itemInSlot == null)
            {
                slots[i].SetItem(Instantiate(itemToAdd));
                return;
            }
        }
    }

    private void ToggleInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
        Cursor.lockState = inventory.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = inventory.activeSelf;
        Time.timeScale = Mathf.Abs(Time.timeScale - 1);
        brain.ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineInputProvider>().enabled = !inventory.activeSelf;
    }

    public int GetItemCount(string name)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetItem() != null && slots[i].GetItemName() == name)
            {
                return slots[i].GetItemCount();
            }
        }
        return 0;
    }

    public void RemoveItem(string name, int quantity)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].GetItem() != null && slots[i].GetItemName() == name)
            {
                slots[i].ReduceItem(quantity);
            }
        }
    }

    public void UpdateCraft()
    {
        craftIcon.sprite = craftables[currentCraft].icon;
        craftName.text = craftables[currentCraft].item_name;
        craftRecipe.text = craftables[currentCraft].recipe;
    }

    public void ChangeCraft(int n)
    {
        currentCraft = Mathf.Clamp(currentCraft + n, 0, craftables.Count - 1);
        UpdateCraft();
    }

    public void CraftItem()
    {
        craftRecipes[currentCraft].Craft(this);
        foreach (Slot itemslot in slots)
        {
            itemslot.updateData();
        }
    }
}
