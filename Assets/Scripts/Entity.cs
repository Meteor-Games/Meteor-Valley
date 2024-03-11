using System;
using Unity.Netcode;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Entity : NetworkBehaviour
{

    // Declaração de NetworkVariable para sincronizar o EntityData
    protected EntityData DefautEntityData;
    protected NetworkVariable<EntityData> entityData = new(new EntityData());
    public EntityData EntityData;

    protected void Start()
    {
        if (IsLocalPlayer)
        {
            entityData.Value.lastRegenTime = Time.time;
        }
        EntityData = this.entityData.Value;
        DefautEntityData = EntityData.Clone();
    }

    public void ApplyEffects()
    {
        EntityData.iventory.ForEach(item =>
        {
            item.effects.ForEach(effect => effect.ApplyEffect(this));
        });
    }


    protected void Update()
    {
        if (!IsLocalPlayer)
        {
            return;
        }

        // Verifique se o EntityData foi inicializado antes de acessá-lo
        if (entityData.Value != null && Time.time - entityData.Value.lastRegenTime >= entityData.Value.regenInterval)
        {
            Regenerate();
            entityData.Value.lastRegenTime = Time.time;
        }
    }

    protected void TakeDamage(int damage)
    {
        // Verifique se o EntityData foi inicializado antes de acessá-lo
        if (entityData.Value != null)
        {
            entityData.Value.health -= damage;
            if (entityData.Value.health <= 0)
            {
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " died.");
        NetworkObject.Destroy(gameObject);
    }

    protected void Move(Vector2 direction)
    {
        // Verifique se o EntityData foi inicializado antes de acessá-lo
        if (entityData.Value == null)
        {
            return;
        }
        transform.Translate(entityData.Value.moveSpeed * Time.deltaTime * direction);
    }

    protected void Regenerate()
    {
        // Verifique se o EntityData foi inicializado antes de acessá-lo
        if (entityData.Value != null)
        {
            entityData.Value.health += entityData.Value.healthRegenAmount;
            entityData.Value.health = Mathf.Clamp(entityData.Value.health, 0, entityData.Value.maxHealth);
            entityData.Value.mana += entityData.Value.manaRegenAmount;
            entityData.Value.mana = Mathf.Clamp(entityData.Value.mana, 0, entityData.Value.maxMana);
            entityData.Value.energy += entityData.Value.energyRegenAmount;
            entityData.Value.energy = Mathf.Clamp(entityData.Value.energy, 0, entityData.Value.maxEnergy);
        }
    }

    protected void weightRecalc()
    {
        var weight = 0f;
        foreach (ItemData item in this.EntityData.iventory)
        {
            weight += (float)item.weight;
        }
        this.EntityData.weight = weight;
    }

    protected float getSpeed()
    {
        var speed = 0f;
        if ((this.EntityData.weightCapacity - this.EntityData.weight) < 0)
        {
            speed -= 1;
        }
        return speed;
    }
}
