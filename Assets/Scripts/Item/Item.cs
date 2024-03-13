using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Unity.VisualScripting;

[System.Serializable]
public enum EquipSlot
{
    NotEquipable,
    Head,
    Chest,
    Legs,
    Boots,
    Weapon,
    Ring,
    Backpack,
    Mount
}

[System.Serializable]
public enum RarityType
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[System.Serializable]
public class ItemData
{
    public int ID = -1;
    public string itemName;
    public float weight;
    public List<EntityEffect> effects;
    public RarityType rarity;
    public int defaultSellValue;
    public int defaultBuyValue;
    public EquipSlot equipSlot;
    public float maxStack = 1;
    public ItemData()
    {
        effects = new();
    }
    public ItemData Clone()
    {
        ItemData clone = new()
        {
            ID = this.ID,
            itemName = this.itemName,
            weight = this.weight,
            rarity = this.rarity,
            defaultSellValue = this.defaultSellValue,
            defaultBuyValue = this.defaultBuyValue,
            equipSlot = this.equipSlot,
            // Clonando a lista de buffs
            effects = new List<EntityEffect>()
        };
        foreach (EntityEffect effect in this.effects)
        {
            clone.effects.Add(effect);
        }

        return clone;
    }
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemData data;
}