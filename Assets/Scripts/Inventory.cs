using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    private const int MaxUniqueItems = 10;
    
    public void AddItem(string item, int count = 1)
    {
        if (count <= 0) return;
        
        if (items.ContainsKey(item))
        {
            items[item] += count;
            Debug.Log($"Добавлено: {item} x {count}");
            return;
        }
        
        if (items.Count >= MaxUniqueItems)
        {
            Debug.LogWarning($"Нет места для «{item}»: занято {items.Count} из {MaxUniqueItems}");
            return;
        }
        
        items.Add(item, count);
        Debug.Log($"Добавлено: {item} x {count}");
    }
    
    public void RemoveItem(string item, int count = 1)
    {
        if (count <= 0) return;
        
        if (!items.ContainsKey(item))
        {
            Debug.LogWarning($"Нельзя убрать «{item}»: его нет в инвентаре");
            return;
        }

        if (count > items[item])
        {
            Debug.LogWarning($"Нельзя убрать {count} шт. «{item}»: в наличии только {items[item]} шт.");
            return;
        }

        items[item] -= count;
        Debug.Log($"Убрано: {item} x {count}");

        if (items[item] == 0)
        {
            items.Remove(item);
        }
    }

    public void Print()
    {
        Debug.Log("--- Текущее состояние инвентаря ---");
        
        int totalCount = 0;
        foreach (var kvp in items)
        {
            Debug.Log($"{kvp.Key} ×{kvp.Value}");
            totalCount += kvp.Value;
        }

        Debug.Log($"кол.во уникальных предметов {items.Count}, общее кол-во {totalCount}");
        Debug.Log($"Занято {items.Count} из {MaxUniqueItems}");
        Debug.Log("------------------------------------");
    }
}
