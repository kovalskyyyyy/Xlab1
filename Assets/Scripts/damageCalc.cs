using UnityEngine;

public class DamageCalc
{
    public static int FinalDamage(Unit attacker, Unit target, out string logDetails)
    {
        int baseDamage = attacker.CurrentWeapon != null 
            ? attacker.CurrentWeapon.BaseDamage 
            : Random.Range(attacker.MinDamage, attacker.MaxDamage + 1);

        int damage = Mathf.Max(1, baseDamage - target.Armor);
        string elemInfo = "без стихии";
        Element weaponElem = attacker.CurrentWeapon.WeaponElement;
        if (attacker.CurrentWeapon != null)
        {
            weaponElem = Element.None;
        }

        if (weaponElem != Element.None && target.TargetElement != Element.None)
        {
            if (weaponElem == target.TargetElement)
            {
                damage = 0;
                elemInfo = "стихии совпали";
            }
            else
            {
                damage = Mathf.FloorToInt(damage * 1.5f);
                elemInfo = "стихия х1,5";
            }
        }

        bool isCrit = Random.value < attacker.CritChance;
        if (isCrit && damage > 0) damage *= 2;

        logDetails = $"{elemInfo}, {(isCrit ? "крит х2" : "без крита")}";
        return damage;
    }
}