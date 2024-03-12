using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Entity entity;
    public Slider slider;

    void Start()
    {
        if (entity == null)
        {
            Debug.LogError("A entidade não foi atribuída à barra de vida!");
            return;
        }

        if (slider == null)
        {
            Debug.LogError("O Slider não foi atribuído à barra de vida!");
            return;
        }
        

    }

    void Update()
    {
        if (entity == null)
        {
            Destroy(gameObject);
            return;
        }

        float maxHealth = entity.EntityData.maxHealth; // Supondo que 'maxHealth' seja o valor máximo da vida
        float currentHealth = entity.EntityData.health; // Supondo que 'health' seja a vida atual

        if (maxHealth <= 0)
        {
            Debug.LogError("O valor máximo de saúde é menor ou igual a zero. Verifique os valores da entidade.");
            return;
        }

        slider.value = ((currentHealth / maxHealth)*100);
    }
}
