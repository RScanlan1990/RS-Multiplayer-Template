using UnityEngine;
using UnityEngine.Networking;

public class InventoryController : NetworkBehaviour
{
    private InventorySlot[] _inventorySlots;

    public override void OnStartLocalPlayer()
    {
       _inventorySlots = gameObject.transform.Find("Canvas")
                                   .transform.Find("Inventory")
                                   .transform.Find("Inventory_Slots").GetComponentsInChildren<InventorySlot>();
    }

    [Command]
    public void CmdPickUpItem(GameObject loot)
    {
        if(!isLocalPlayer) { return; }
        RpcPickUpItem(loot);
    }

    [ClientRpc]
    public void RpcPickUpItem(GameObject loot)
    {
        AddItem(loot.GetComponent<Loot>().Item);
        Destroy(loot);
    }

    private void AddItem(Item item)
    {
        if (item != null)
        {
            foreach (var slot in _inventorySlots)
            {
                if (slot.HaveItem() == false)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }
    }

    private bool DropItem(Item item, Vector3 clickedPosition)
    {
        var heading = clickedPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var dropedPosition = transform.position + (direction * 1.0f);
        Instantiate(item.Graphics, new Vector3(dropedPosition.x, transform.position.y, dropedPosition.z), transform.rotation);
        return true;
    }

    public Item SlotClicked(InventorySlot slot, Item item)
    {
        var savedItem = slot.GetItem();
        if (savedItem != null)
        {
            slot.ClearSlot();
        }

        if (item != null)
        {
            slot.AddItem(item);
        }
        return savedItem;
    }
}