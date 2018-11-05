using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Image _icon;
    private InventoryController _inventory;
    private Button _button;
    private Item _item;

    private void Start()
    {
        _icon = GetComponent<Image>();
        _inventory = gameObject.transform.root.gameObject.GetComponent<InventoryController>();
        _button = gameObject.GetComponentInChildren<Button>();
        _button.onClick.AddListener(SlotClicked);
    }

    public bool HaveItem()
    {
        return _item != null;
    }

    public Item GetItem()
    {
        return _item;
    }

    public void AddItem(Item item)
    {
        _item = item;
        _icon.sprite = _item.Image;
        _icon.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _icon.sprite = null;
        _icon.enabled = false;
    }

    private void SlotClicked()
    {
        _inventory.SlotClicked(this, _item);
    }
}