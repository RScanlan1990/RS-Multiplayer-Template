using System.Collections;
using UnityEngine;

public class Item : ScriptableObject
{
    public enum ItemType
    {
        None,
        Tool,
        Weapon,
    }

    public ItemType Type;
    public bool IsEquipable;
    public int Id;
    public string Name;
    public Sprite Image;
    public GameObject Graphics;
}
