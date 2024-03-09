using Unity.Netcode;

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

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref entityType);
        serializer.SerializeValue(ref behaviorType);
        serializer.SerializeValue(ref weightCapacity);
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
}