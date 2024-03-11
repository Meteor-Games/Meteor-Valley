using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpeed: MonoBehaviour
{
    public Item data;
    // Start is called before the first frame update
    void Start()
    {
        data = this.GetComponent<Item>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.TryGetComponent<Entity>(out var entity))
        {
            if (!entity.EntityData.pickItens)
            {
                return;
            }

            if(entity.EntityData.iventory != null)
            {
                // Clona o objeto e adiciona ao inventário do jogador
                entity.EntityData.iventory.Add(data.data);
                entity.ApplyEffects();
            }
            else
            {
                entity.EntityData.iventory = new()
                {
                    // Clona o objeto e adiciona ao inventário do jogador
                    data.data
                };
            }
          
            // Remove o objeto do chão
            Destroy(this.gameObject);
        }
    }
}
