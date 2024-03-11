using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncressedSpeed : EntityEffect
{
    public float speed = 2;

    public override void ApplyEffect(Entity entity)
    {
        if (!this.applied)
        {
            entity.EntityData.moveSpeed += 2;
            this.applied = true;
        }
       

    }

    public override void RemoveEffect(Entity entity)
    {
        if (this.applied)
        {
            entity.EntityData.moveSpeed -= speed;
            this.applied = false;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.applied = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
