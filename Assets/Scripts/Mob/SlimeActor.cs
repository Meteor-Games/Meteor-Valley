using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeActor : Entity
{
    // Start is called before the first frame update

    public float scaleMulti = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        // Calcula a escala com base na quantidade de itens no inventário
        float scaleMultiplier = 1f + ((float)this.EntityData.iventory.Count * scaleMulti);
        Vector3 newScale = Vector3.one * scaleMultiplier;

        // Define a nova escala do objeto
        transform.localScale = newScale;
    }
}
