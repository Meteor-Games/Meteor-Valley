using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

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
public class BuffDebuff
{
    public string name;
    public int value;

    // Função para aplicar o buff/debuff a uma entidade (GameObject)
    public virtual void ApplyEntity(GameObject entity)
    {
        //Entity data = entity.GetComponent<Entity>();
        // Implementação padrão (vazia)
        // Subclasses podem sobrescrever esta função para fornecer um comportamento específico
    }
}

[System.Serializable]
public class ItemData
{
    public int ID;
    public string itemName;
    public float weight;
    public List<BuffDebuff> buffs;
    public List<BuffDebuff> debuffs;
    public RarityType rarity;
    public int defaultSellValue;
    public int defaultBuyValue;
    public EquipSlot equipSlot;
}

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemData data;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}