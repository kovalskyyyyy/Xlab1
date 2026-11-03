using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private Dictionary<string, int> m_items = new Dictionary<string, int>();
    private const int MaxUniqueItems = 10;
    
    public void AddItem(string item, int count = 1)
    {
        if (count <= 0) return;
        
        if (m_items.ContainsKey(item))
        {
            m_items[item] += count;
            Debug.Log($"Добавлено: {item} x {count}");
            return;
        }
        
        if (m_items.Count >= MaxUniqueItems)
        {
            Debug.LogWarning($"Нет места для «{item}»: занято {m_items.Count} из {MaxUniqueItems}");
            return;
        }
        
        m_items.Add(item, count);
        Debug.Log($"Добавлено: {item} x {count}");
    }
    
    public void RemoveItem(string item, int count = 1)
    {
        if (count <= 0) return;
        
        if (!m_items.ContainsKey(item))
        {
            Debug.LogWarning($"Нельзя убрать «{item}»: его нет в инвентаре");
            return;
        }

        if (count > m_items[item])
        {
            Debug.LogWarning($"Нельзя убрать {count} шт. «{item}»: в наличии только {m_items[item]} шт.");
            return;
        }

        m_items[item] -= count;
        Debug.Log($"Убрано: {item} x {count}");

        if (m_items[item] == 0)
        {
            m_items.Remove(item);
        }
    }

    public void Print()
    {
        Debug.Log("--- Текущее состояние инвентаря ---");
        
        int totalCount = 0;
        foreach (var kvp in m_items)
        {
            Debug.Log($"{kvp.Key} ×{kvp.Value}");
            totalCount += kvp.Value;
        }

        Debug.Log($"кол.во уникальных предметов {m_items.Count}, общее кол-во {totalCount}");
        Debug.Log($"Занято {m_items.Count} из {MaxUniqueItems}");
        Debug.Log("------------------------------------");
    }
}
