using UnityEngine;

public class Unit
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Armor { get; private set; }
    public Element TargetElement { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public float CritChance { get; private set; }

    public int MinDamage { get; private set; }
    public int MaxDamage { get; private set; }

    public bool IsAlive => Health > 0;

    public Unit(string name, int maxHealth, int armor, Element targetElement, Weapon startingWeapon, float critChance = 0.3f)
    {
        Name = name;
        Health = maxHealth;
        Armor = armor;
        TargetElement = targetElement;
        CurrentWeapon = startingWeapon;
        CritChance = critChance;
    }

    public Unit(string name, int maxHealth, int minDamage, int maxDamage)
    {
        Name = name;
        Health = maxHealth;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        Armor = 0; 
        TargetElement = Element.None;
        CritChance = 0f; 
        CurrentWeapon = null; 
    }

    public void TakeDamage(int amount) => Health = Mathf.Max(0, Health - amount);

    public bool TryPickUpWeapon(Weapon groundWeapon)
    {
        if (CurrentWeapon == null || groundWeapon.UtilityValue > CurrentWeapon.UtilityValue)
        {
            CurrentWeapon = groundWeapon;
            return true;
        }
        return false;
    }
}