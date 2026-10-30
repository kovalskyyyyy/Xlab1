using UnityEngine;

public class DamageCalc : MonoBehaviour
{
    [Header("Параметры бойцов")]
    [SerializeField] private int m_baseDamage = 50;
    [SerializeField] private int m_armor = 10;
    [SerializeField] private Element m_weaponElement = Element.Fire;
    [SerializeField] private Element m_targetElement = Element.Ice;
    
    [Header("Параметры ударов")]
    [SerializeField, Range(0f, 1f)] private float m_critChance = 0.3f;
    [SerializeField] private int m_NumberOfHits = 3;

    public enum Element
    {
        None,
        Fire,
        Ice
    }

    void Start()
    {
        Debug.Log($"=== НАЧАЛО РАСЧЕТА УРОНА (Всего ударов: {m_NumberOfHits}) ===");
        
        for (int i = 1; i <= m_NumberOfHits; ++i)
        {
            FinalDamage(i);
        }
    }

    private void FinalDamage(int hit)
    {
        int damage = Mathf.Max(1, m_baseDamage - m_armor);
        string elemInfo = "без стихии";

        if (m_weaponElement == Element.None || m_targetElement == Element.None)
        {
        } 
        else if (m_weaponElement == m_targetElement)
        {
            damage = 0;
            elemInfo = "стихии совпали";
        }
        else
        {
            damage = Mathf.FloorToInt(damage * 1.5f);
            elemInfo = "стихия х1,5";
        }

        bool isCrit = Random.value < m_critChance;
        
        if (isCrit && damage > 0)
        {
            damage *= 2;
        }
        else if (damage == 0)
        {
    
            isCrit = false;
        }

        string critInfo = isCrit ? "крит х2" : "без крита";
        string log = $"Удар {hit}: {damage} урона ({elemInfo}, {critInfo})";
        Debug.Log(log);
    }
}
