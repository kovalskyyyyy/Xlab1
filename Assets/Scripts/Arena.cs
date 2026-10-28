using UnityEngine;

public class Arena : MonoBehaviour
{
    public enum HeroClass { Warrior, Mage }

    [Header("Настройки Героя")]
    [SerializeField] private HeroClass chosenClass = HeroClass.Warrior;

    [Header("Настройки Битвы")]
    [SerializeField, Range(1, 100)] private int maxRounds = 20;

    private Unit hero;
    private Unit enemy;

    private void Awake()
    {
        if (chosenClass == HeroClass.Warrior)
        {
            hero = new Unit("Воин", 100, 10, 16);
        }
        else
        {
            hero = new Unit("Маг", 130, 6, 12);
        }

        int randomEnemyType = Random.Range(0, 3);
        switch (randomEnemyType)
        {
            case 0:
                enemy = new Unit("Гоблин", 50, 4, 8);
                break;
            case 1:
                enemy = new Unit("Орк", 80, 5, 10);
                break;
            case 2:
                enemy = new Unit("Тролль", 140, 8, 12);
                break;
        }
    }

    private void Start()
    {
        ExecuteBattle();
    }

    private void ExecuteBattle()
    {
        Debug.Log($"{hero.Name} ({hero.Health} HP) против {enemy.Name} ({enemy.Health} HP)");

        int currentRound = 1;

        while (hero.IsAlive && enemy.IsAlive && currentRound <= maxRounds)
        {
            string roundLog = $"Раунд {currentRound}: ";

            // --- ХОД ГЕРОЯ ---
            int heroDamage = hero.GetRandomDamage();
            enemy.TakeDamage(heroDamage);
            roundLog += $"{hero.Name} наносит {heroDamage}, у {enemy.Name.ToLower()}а {enemy.Health} HP; ";

            // --- ХОД ВРАГА (только если он выжил) ---
            if (enemy.IsAlive)
            {
                int enemyDamage = enemy.GetRandomDamage();
                hero.TakeDamage(enemyDamage);
                roundLog += $"{enemy.Name} наносит {enemyDamage}, у {hero.Name.ToLower()}а {hero.Health} HP";
            }
            else
            {
                roundLog += $"{enemy.Name} погиб и не успел атаковать";
            }

            Debug.Log(roundLog);
            currentRound++;
        }

        if (!hero.IsAlive && !enemy.IsAlive)
        {
            Debug.Log("Ничья! Оба бойца пали в одном раунде.");
        }
        else if (hero.IsAlive && !enemy.IsAlive)
        {
            Debug.Log($"Победа! У {hero.Name.ToLower()}а осталось {hero.Health} HP");
        }
        else if (!hero.IsAlive && enemy.IsAlive)
        {
            Debug.Log($"Поражение! Побеждает {enemy.Name} ({enemy.Health} HP)");
        }
        else
        {
            Debug.Log($"Ничья! Достигнут лимит раундов ({maxRounds}). {hero.Name}: {hero.Health} HP, {enemy.Name}: {enemy.Health} HP");
        }
    }
}
