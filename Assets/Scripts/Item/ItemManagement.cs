using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ItemManager : MonoBehaviour
{
    // Dicionário para mapear IDs de itens para os próprios itens
    public Dictionary<int, GameObject> itemsById = new Dictionary<int, GameObject>();

    // Adicione todos os seus itens aqui
    public List<GameObject> items;

    private void Awake()
    {
        // Preencha o dicionário com itens e seus IDs
        foreach (GameObject item in items)
        {
            var _i = item.GetComponent<Item>();
            itemsById.Add(_i.data.ID, item);
        }
    }

    // Função para obter um item com base no ID
    public GameObject GetItemById(int id)
    {
        if (itemsById.ContainsKey(id))
        {
            return itemsById[id];
        }
        else
        {
            Debug.LogWarning("Item with ID " + id + " not found.");
            return null;
        }
    }
}