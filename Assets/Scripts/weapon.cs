public enum Element { None, Fire, Ice }

public class Weapon
{
    public string Name { get; private set; }
    public int BaseDamage { get; private set; }
    public Element WeaponElement { get; private set; }
    public int UtilityValue { get; private set; }

    public Weapon(string name, int baseDamage, Element element, int utilityValue)
    {
        Name = name;
        BaseDamage = baseDamage;
        WeaponElement = element;
        UtilityValue = utilityValue;
    }
}