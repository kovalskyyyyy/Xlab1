using System.Collections.Generic;
using UnityEngine;

public class teamArena : MonoBehaviour
{
    private List<Unit> m_heroes = new List<Unit>();
    private List<Unit> m_enemies = new List<Unit>();
    
    private List<Weapon> m_droppedWeaponsPool = new List<Weapon>();

    private void Awake()
    {
        PrepareTeams();
    }

    private void Start()
    {
        ExecuteTeamBattle();
    }

    private void PrepareTeams()
    {
        Weapon fireSword = new Weapon("Огненный меч", 30, Element.Fire, 5);
        Weapon iceStaff = new Weapon("Ледяной посох", 25, Element.Ice, 4);
        Weapon rustyDagger = new Weapon("Ржавый кинжал", 15, Element.None, 1);
        Weapon legendaryAxe = new Weapon("Топор Кровавой Луны", 45, Element.Fire, 10);

        m_heroes.Add(new Unit("Рыцарь Артур", 120, 15, Element.None, fireSword));
        m_heroes.Add(new Unit("Маг Джайна", 80, 5, Element.Ice, iceStaff));
        m_heroes.Add(new Unit("Вор Робин", 90, 8, Element.Fire, rustyDagger));

        m_enemies.Add(new Unit("Орк-Вождь", 150, 12, Element.Fire, legendaryAxe));
        m_enemies.Add(new Unit("Гоблин-Плут", 60, 4, Element.None, rustyDagger));
        m_enemies.Add(new Unit("Ледяной Тролль", 130, 10, Element.Ice, iceStaff));
    }

    private void ExecuteTeamBattle()
    {
        Debug.Log("НАЧАЛО КОМАНДНОГО СРАЖЕНИЯ");
        int round = 1;

        while (IsTeamAlive(m_heroes) && IsTeamAlive(m_enemies) && round <= 50)
        {
            Debug.Log($"\n РАУНД {round}");

            ExecuteTeamTurn(m_heroes, m_enemies);

            if (!IsTeamAlive(m_enemies)) break;

            ExecuteTeamTurn(m_enemies, m_heroes);

            round++;
        }

        Debug.Log("\nБОЙ ЗАВЕРШЕН");
        if (IsTeamAlive(m_heroes) && !IsTeamAlive(m_enemies))
            Debug.Log("<color=green>Победа команды Героев!</color>");
        else if (!IsTeamAlive(m_heroes) && IsTeamAlive(m_enemies))
            Debug.Log("<color=red>Поражение! Команда Противников победила.</color>");
        else
            Debug.Log("Ничья! Никого не осталось в живых.");
    }

    private void ExecuteTeamTurn(List<Unit> attackers, List<Unit> defenders)
    {
        foreach (var attacker in attackers)
        {
            if (!attacker.IsAlive) continue;

            TryTeamPickUp(attacker);

            Unit target = GetFirstAliveTarget(defenders);
            if (target == null) return; 

            int finalDamage = attacker.CalculateDamageTo(target, out string details);
            target.TakeDamage(finalDamage);

            Debug.Log($"{attacker.Name} бьет {target.Name} из оружия «{attacker.CurrentWeapon?.Name}» на {finalDamage} урона ({details}). У цели осталось: {target.Health} HP");

            if (!target.IsAlive)
            {
                Debug.Log($"<color=orange>☠️ {target.Name} погибает!</color>");
                if (target.CurrentWeapon != null)
                {
                    Debug.Log($"   [СОБЫТИЕ] Оружие «{target.CurrentWeapon.Name}» падает на землю.");
                    m_droppedWeaponsPool.Add(target.CurrentWeapon);
                }
            }
        }
    }

    private void TryTeamPickUp(Unit unit)
    {
        if (m_droppedWeaponsPool.Count == 0) return;

        for (int i = m_droppedWeaponsPool.Count - 1; i >= 0; i--)
        {
            Weapon groundWeapon = m_droppedWeaponsPool[i];
            if (unit.TryPickUpWeapon(groundWeapon))
            {
                m_droppedWeaponsPool.RemoveAt(i);
                break; 
            }
        }
    }

    private bool IsTeamAlive(List<Unit> team)
    {
        return team.Exists(u => u.IsAlive);
    }

    private Unit GetFirstAliveTarget(List<Unit> team)
    {
        return team.Find(u => u.IsAlive);
    }
}
