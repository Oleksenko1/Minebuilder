using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HotbarManager : MonoBehaviour
{
    public event Action OnItemChanged;

    [SerializeField] private List<BlockSO> blockItems;
    [SerializeField] private Transform prefabHotbarItem;
    [SerializeField] private Transform hotbar;
    [SerializeField] private HotbarTooltip tooltip;

    private List<HotbarItem> hotbarItems = new List<HotbarItem>();

    private HotbarItem currentlySelected;
    private int selectedID;
    private void Awake()
    {
        SetupItems();

        PlayerInput.OnHotbarPressed += GetHotkeyInput;
    }
    private void Start()
    {
        ChooseNewItem(0);
    }
    private void SetupItems()
    {
        for (int i = 0; i < blockItems.Count; i++)
        {
            var newItem = Instantiate(prefabHotbarItem, hotbar);

            HotbarItem newHotbar = newItem.GetComponent<HotbarItem>();
            newHotbar.SetIconSprite(blockItems[i].icon);
            newHotbar.SetHotkeyText((i + 1).ToString());

            hotbarItems.Add(newHotbar);
        }
    }
    private void GetHotkeyInput(KeyCode keyCode)
    {
        int idOfItem = -1;

        switch (keyCode)
        {
            case KeyCode.Alpha1:
                idOfItem = 0;
                break;
            case KeyCode.Alpha2:
                idOfItem = 1;
                break;
            case KeyCode.Alpha3:
                idOfItem = 2;
                break;
            case KeyCode.Alpha4:
                idOfItem = 3;
                break;
            case KeyCode.Alpha5:
                idOfItem = 4;
                break;
            case KeyCode.Alpha6:
                idOfItem = 5;
                break;
            case KeyCode.Alpha7:
                idOfItem = 6;
                break;
            case KeyCode.Alpha8:
                idOfItem = 7;
                break;
            case KeyCode.Alpha9:
                idOfItem = 8;
                break;
        }

        // If id of item is selected succesfully - choose needed item
        if(idOfItem != -1 && idOfItem < hotbarItems.Count)
        {
            ChooseNewItem(idOfItem);
        }
    }
    private void ChooseNewItem(int id)
    {
        // Does nothing if items is selected already
        if (currentlySelected == hotbarItems[id]) return;

        // Deselect last choosen item
        if(currentlySelected != null) currentlySelected.DeselectItem();

        // Choose and select new item
        currentlySelected = hotbarItems[id];
        currentlySelected.SelectItem();

        selectedID = id;

        // Show tooltip with block name
        tooltip.UpdateTooltip(blockItems[selectedID].blockName);

        OnItemChanged?.Invoke();
    }
    public BlockSO GetCurrentItem()
    {
        return blockItems[selectedID];
    }
}
