using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;

[System.Serializable]
public enum EntityType
{
    Mob,
    NPC,
    Player
}
[System.Serializable]
public enum BehaviorType
{
    Passive,
    Aggressive,
    Undefined
}


[System.Serializable]
public enum ElementType
{
    Fire,
    Water,
    Earth,
    Air
}

[System.Serializable]
public class EntityData : INetworkSerializable
{
    // Informações adicionais da entidade
    public EntityType entityType;
    public BehaviorType behaviorType;
    public float weightCapacity;
    public float weight;

    // Atributos comuns a todas as entidades
    public int health = 100;
    public int maxHealth = 100;
    public int mana = 100;
    public int maxMana = 100;
    public int energy = 100;
    public int maxEnergy = 100;
    public float moveSpeed = 5f;
    public int healthRegenAmount = 2; // Quantidade de saúde recuperada por segundo
    public int manaRegenAmount = 2; // Quantidade de mana recuperada por segundo
    public int energyRegenAmount = 2; // Quantidade de energia recuperada por segundo
    public float regenInterval = 1f; // Intervalo de tempo entre cada recuperação de saúde, mana e energia

    public float lastRegenTime; // Guarda o tempo da última recuperação de saúde, mana e energia
    public bool pickItens;
    public List<ItemData> iventory;
    
    public EntityData()
    {
        iventory = new List<ItemData>();
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref entityType);
        serializer.SerializeValue(ref pickItens);
        serializer.SerializeValue(ref behaviorType);
        serializer.SerializeValue(ref weightCapacity);
        serializer.SerializeValue(ref weight);
        serializer.SerializeValue(ref health);
        serializer.SerializeValue(ref maxHealth);
        serializer.SerializeValue(ref mana);
        serializer.SerializeValue(ref maxMana);
        serializer.SerializeValue(ref energy);
        serializer.SerializeValue(ref maxEnergy);
        serializer.SerializeValue(ref moveSpeed);
        serializer.SerializeValue(ref healthRegenAmount);
        serializer.SerializeValue(ref manaRegenAmount);
        serializer.SerializeValue(ref energyRegenAmount);
        serializer.SerializeValue(ref regenInterval);
        serializer.SerializeValue(ref lastRegenTime);
    }

    public EntityData Clone()
    {
        EntityData clone = new();
        clone.entityType = this.entityType;
        clone.behaviorType = this.behaviorType;
        clone.weightCapacity = this.weightCapacity;
        clone.weight = this.weight;
        clone.health = this.health;
        clone.maxHealth = this.maxHealth;
        clone.mana = this.mana;
        clone.maxMana = this.maxMana;
        clone.energy = this.energy;
        clone.maxEnergy = this.maxEnergy;
        clone.moveSpeed = this.moveSpeed;
        clone.healthRegenAmount = this.healthRegenAmount;
        clone.manaRegenAmount = this.manaRegenAmount;
        clone.energyRegenAmount = this.energyRegenAmount;
        clone.regenInterval = this.regenInterval;
        clone.lastRegenTime = this.lastRegenTime;
        clone.pickItens = this.pickItens;

        // Clonando a lista de inventory
        clone.iventory = new List<ItemData>();
        foreach (ItemData item in this.iventory)
        {
            clone.iventory.Add(item.Clone());
        }

        return clone;
    }
}