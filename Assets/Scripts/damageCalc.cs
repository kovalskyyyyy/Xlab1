using UnityEngine;

public class DamageCalc : MonoBehaviour
{
    [Header("Параметры бойцов")]
    [SerializeField] private int base_damage = 50;
    [SerializeField] private int armor = 10;
    [SerializeField] private Element weaponElement = Element.Fire;
    [SerializeField] private Element targetElement = Element.Ice;
    
    [Header("Параметры ударов")]
    [SerializeField, Range(0f, 1f)] private float critChance = 0.3f;
    [SerializeField] private int NumberOfHits = 3;

    public enum Element
    {
        None,
        Fire,
        Ice
    }

    void Start()
    {
        Debug.Log($"=== НАЧАЛО РАСЧЕТА УРОНА (Всего ударов: {NumberOfHits}) ===");
        
        for (int i = 1; i <= NumberOfHits; ++i)
        {
            FinalDamage(i);
        }
    }

    private void FinalDamage(int hit)
    {
        int damage = Mathf.Max(1, base_damage - armor);
        string elemInfo = "без стихии";

        if (weaponElement == Element.None || targetElement == Element.None)
        {
        } 
        else if (weaponElement == targetElement)
        {
            damage = 0;
            elemInfo = "стихии совпали";
        }
        else
        {
            damage = Mathf.FloorToInt(damage * 1.5f);
            elemInfo = "стихия х1,5";
        }

        bool isCrit = Random.value < critChance;
        
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
