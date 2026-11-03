using UnityEngine;

public class Unit
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Armor { get; private set; }
    public Element TargetElement { get; private set; }
    public Weapon CurrentWeapon { get; private set; }
    public float CritChance { get; private set; }

    private int minDamage;
    private int maxDamage;

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
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        
        Armor = 0; 
        TargetElement = Element.None;
        CritChance = 0f; 
        CurrentWeapon = new Weapon("Кулаки", minDamage, Element.None, 0); 
    }

    public int GetRandomDamage()
    {
        if (CurrentWeapon != null) return CurrentWeapon.BaseDamage; 
        return Random.Range(minDamage, maxDamage + 1);
    }

    public int CalculateDamageTo(Unit target, out string logDetails)
    {
        int damage = Mathf.Max(1, (CurrentWeapon != null ? CurrentWeapon.BaseDamage : GetRandomDamage()) - target.Armor);
        string elemInfo = "без стихии";

        Element weaponElem = CurrentWeapon != null ? CurrentWeapon.WeaponElement : Element.None;

        if (weaponElem == Element.None || target.TargetElement == Element.None) { } 
        else if (weaponElem == target.TargetElement) { damage = 0; elemInfo = "стихии совпали"; }
        else { damage = Mathf.FloorToInt(damage * 1.5f); elemInfo = "стихия х1,5"; }

        bool isCrit = Random.value < CritChance;
        if (isCrit && damage > 0) damage *= 2;
        logDetails = $"{elemInfo}, {(isCrit ? "крит х2" : "без крита")}";
        return damage;
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
