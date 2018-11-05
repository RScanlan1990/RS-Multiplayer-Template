using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable {

    public Item Item;
    public Vector3 HandOffset;
    public Vector3 HandRotationOffset;

    public override void Interact(GameObject player)
    {
        base.Interact(player);
        PickUp(player);
    }

    private void PickUp(GameObject player)
    {
        player.GetComponent<InventoryController>().CmdPickUpItem(this.gameObject);
    }
}
