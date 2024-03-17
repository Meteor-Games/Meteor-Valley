using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUp : EntityEffect
{
    // Start is called before the first frame update
    public void ApplyEffect(Entity entity)
    {
        if (!applied)
        {
            entity.EntityData.health += 1;

            applied = true;
        }
    }

    public void RemoveEffect(Entity entity)
    {
        if (applied)
        {
            applied = false;
        }

    }

    // Método para atualizar o efeito (decrementar o tempo restante)
    public void UpdateEffect()
    {
        if (applied)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                // Remover o efeito da entidade
                // Exemplo: entity.RemoveBuffOrDebuff();

                applied = false;
            }
        }
    }
}
