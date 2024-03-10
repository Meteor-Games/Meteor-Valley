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

    // Update is called once per frame
    void Update()
    {
        // Nenhuma ação necessária no Update()
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var entity = collision.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            if (!entity.EntityData.pickItens)
            {
                return;
            }

            if(entity.EntityData.iventory != null)
            {
                // Clona o objeto e adiciona ao inventário do jogador
                entity.EntityData.iventory.Add(data.data);
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
