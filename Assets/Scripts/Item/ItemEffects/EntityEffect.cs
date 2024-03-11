using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class EntityEffect : MonoBehaviour
{
    public string effectName; // Nome do efeito
    public int id; // ID do efeito
    public string description; // Descrição do efeito
    public bool applied;
    public float duration;
    public float timeRemaining;

    //Construtor
    public EntityEffect()
    {
        this.applied = false;
    }

    // Método para aplicar o efeito em uma entidade
    public virtual void ApplyEffect(Entity entity)
    {
        if (!applied)
        {
            // Aplicar o efeito na entidade
            // Exemplo: entity.ApplyBuffOrDebuff();

            applied = true;
        }
    }

    public virtual void RemoveEffect(Entity entity)
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
