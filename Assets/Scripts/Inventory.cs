using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[Serializable]
public class Inventory
{
    protected int numberOfSlots; // Quantidade de slots no invent�rio
    [SerializeField]
    protected List<ItemSlot> slots; // Lista de slots de itens
    public float weight = 0;

    public Inventory(int numberOfSlots = 10)
    {
        this.numberOfSlots = numberOfSlots;
        this.slots = new List<ItemSlot>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            var slot = new ItemSlot();
            slots.Add(slot);
        }
    }

    public List<ItemSlot> GetSlots()
    {
        return slots;
    }
    public void iventoryUpdate()
    {
        weight = 0;
        slots.ForEach(slot =>
        {
            if (!slot.IsEmpty())
            {
                weight += (slot.GetQuantity() * slot.GetItem().weight);
            }
            
        });

    }

    // Adicionar um item ao invent�rio em um slot dispon�vel
    public bool AddItem(ItemData item, float quantity = 1)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItem(item, quantity);
                return true;
            }
        }
        iventoryUpdate();
        return false; // Retorna falso se n�o houver slots dispon�veis
    }

    // Verificar se h� slots dispon�veis no invent�rio
    public bool HasAvailableSlot()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsEmpty())
            {
                return true;
            }
        }
        return false;
    }

    // Remover um item do invent�rio
    public void RemoveItem(ItemData item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.GetItem() == item)
            {
                slot.RemoveItem();
                iventoryUpdate();
                return;
            }
        }
    }

    // Mover um item de um slot para outro no invent�rio
    public void MoveItem(int fromSlotIndex, int toSlotIndex)
    {
        if (fromSlotIndex < 0 || fromSlotIndex >= slots.Count ||
            toSlotIndex < 0 || toSlotIndex >= slots.Count)
        {
            Debug.LogError("�ndice de slot inv�lido.");
            return;
        }

        ItemSlot fromSlot = slots[fromSlotIndex];
        ItemSlot toSlot = slots[toSlotIndex];

        if (fromSlot.IsEmpty())
        {
            Debug.LogError("O slot de origem est� vazio.");
            return;
        }

        toSlot.AddItem(fromSlot.GetItem());
        fromSlot.RemoveItem();
    }

    public void MoveItemBetweenInventories(Inventory fromInventory, int fromSlotIndex, Inventory toInventory, int toSlotIndex)
    {
        // Verificar se os �ndices de slot fornecidos s�o v�lidos
        if (fromSlotIndex < 0 || fromSlotIndex >= fromInventory.slots.Count ||
            toSlotIndex < 0 || toSlotIndex >= toInventory.slots.Count)
        {
            Debug.LogError("�ndice de slot inv�lido.");
            return;
        }

        // Obter os slots de origem e destino
        ItemSlot fromSlot = fromInventory.slots[fromSlotIndex];
        ItemSlot toSlot = toInventory.slots[toSlotIndex];

        // Verificar se o slot de origem est� vazio
        if (fromSlot.IsEmpty())
        {
            Debug.LogError("O slot de origem est� vazio.");
            return;
        }

        // Verificar se h� espa�o no slot de destino
        if (!toSlot.IsEmpty())
        {
            Debug.LogError("O slot de destino n�o est� vazio.");
            return;
        }

        // Mover o item do invent�rio de origem para o invent�rio de destino
        toSlot.AddItem(fromSlot.GetItem());
        fromSlot.RemoveItem();
        fromInventory.iventoryUpdate();
        toInventory.iventoryUpdate();
    }

    // Redefinir o tamanho do invent�rio mantendo os itens e posi��es dos itens
    public List<ItemSlot> ResizeInventory(int newNumberOfSlots)
    {
        List<ItemSlot> removedSlots = new List<ItemSlot>(); // Lista para armazenar os slots removidos

        if (newNumberOfSlots < 1)
        {
            Debug.LogError("N�mero inv�lido de slots.");
            return removedSlots; // Retorna uma lista vazia se o n�mero de slots for inv�lido
        }

        // Se o novo n�mero de slots for menor que o n�mero atual, remova os slots excedentes
        if (newNumberOfSlots < numberOfSlots)
        {
            // Itera sobre os slots excedentes e adiciona os slots removidos � lista
            for (int i = numberOfSlots - 1; i >= newNumberOfSlots; i--)
            {
                if (!slots[i].IsEmpty())
                {
                    removedSlots.Add(slots[i]);
                    slots.RemoveAt(i);
                }
            }
        }
        // Atualiza o n�mero de slots ap�s a opera��o de redimensionamento
        numberOfSlots = newNumberOfSlots;
        iventoryUpdate();
        return removedSlots; // Retorna a lista de slots removidos
    }
}

[System.Serializable]
public class ItemSlot
{
    [SerializeField]
    public ItemData item = new(); // O item neste slot
    [SerializeField]
    public float quantity = 0;

    // Verificar se o slot est� vazio
    public bool IsEmpty()
    {
        return item.ID == -1;
    }

    // Obter o item neste slot
    public ItemData GetItem()
    {
        return item;
    }

    // Obter a quantidade de itens neste slot
    public float GetQuantity()
    {
        return quantity;
    }

    // Definir a quantidade de itens neste slot
    public void SetQuantity(float newQuantity)
    {
        quantity = Mathf.Max(0, newQuantity);
        if (quantity == 0)
        {
            item = null;
        }
    }

    // Adicionar um item ao slot com uma quantidade espec�fica
    public bool AddItem(ItemData newItem, float addedQuantity = 1)
    {
        if (addedQuantity <= 0)
        {
            return false;
        }

        if(item.ID == -1)
        {
            item = newItem;
            quantity += addedQuantity;
            return true;
        }

        if (item.ID != newItem.ID)
        {
            Debug.LogError("Este slot j� cont�m um item diferente.");
            return false;
        }

        item = newItem;
        quantity += addedQuantity;
        return true;
    }

    // Remover uma quantidade espec�fica de itens deste slot
    public void RemoveItem(float removedQuantity = 1)
    {
        if (removedQuantity <= 0)
        {
            Debug.LogError("A quantidade removida deve ser maior que zero.");
            return;
        }

        quantity = Mathf.Max(0, quantity - removedQuantity);

        if (quantity == 0)
        {
            item = null;
        }
    }
    // M�todo para clonar o ItemSlot
    public ItemSlot Clone()
    {
        ItemSlot clonedSlot = new()
        {
            item = this.item != null ? (ItemData)item.Clone() : null,
            quantity = this.quantity
        };
        return clonedSlot;
    }
}

