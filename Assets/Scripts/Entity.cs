using System;
using Unity.Netcode;
using UnityEngine;

public class Entity : NetworkBehaviour
{
    // Declaração de NetworkVariable para sincronizar o EntityData
    public NetworkVariable<EntityData> entityData = new NetworkVariable<EntityData>();

    private void Start()
    {
        if (IsLocalPlayer)
        {
            // Inicialize o EntityData se ainda não estiver inicializado
            if (entityData.Value == null)
            {
                entityData.Value = new EntityData();
            }

            // Inicialize o tempo da última regeneração localmente
            entityData.Value.lastRegenTime = Time.time;
        }
    }

    private void Update()
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

    public void TakeDamage(int damage)
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

    public void Move(Vector2 direction)
    {
        // Verifique se o EntityData foi inicializado antes de acessá-lo
        if (entityData.Value != null)
        {
            transform.Translate(direction * entityData.Value.moveSpeed * Time.deltaTime);
        }
    }

    private void Regenerate()
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
}
