using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIncressedLow : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.TryGetComponent<Entity>(out var entity))
        {
            if (!entity.EntityData.pickItens)
            {
                return;
            }

            var ret = entity.EntityData.inventory.AddItem(data.Clone(), 1);
            if (ret == true)
            {
                entity.ApplyEffects();
                // Remove o objeto do chão
                Destroy(this.gameObject);
            }

        }
    }
}
