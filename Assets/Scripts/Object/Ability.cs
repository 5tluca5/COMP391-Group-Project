using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType : int
{
    MaxHP = 0,
    FireRate,
    Damage,
}

public abstract class Ability
{
    public AbilityType abilityType;
    protected int maxLevel = 10;
    protected int level;
    protected List<int> costs = new List<int>();

    public abstract float GetCurrentEffect();
    public abstract float GetNextEffect();

    public int GetLevel()
    {
        return level;
    }

    public bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    public bool LevelUp()
    {
        if (level + 1 > maxLevel) return false;

        level++;

        return true;
    }

    public int GetUpgradeCost()
    {
        return costs.Count > level ? costs[level] : 99999;
    }

}

public class AbilityHeath : Ability
{
    readonly float effectPerLevel = 0.5f;

    public AbilityHeath(int level)
    {
        maxLevel = 10;
        costs = new List<int>() { 2, 4, 6, 8, 10, 20, 20, 20, 30, 30 };
        abilityType = AbilityType.MaxHP;
        this.level = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level + 1);
    }
}

public class AbilityFireRate : Ability
{
    readonly float effectPerLevel = 0.02f;

    public AbilityFireRate(int level)
    {
        maxLevel = 10;
        costs = new List<int>() { 5, 10, 15, 20, 25, 30, 30, 30, 30, 30 };
        abilityType = AbilityType.FireRate;
        this.level = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level + 1);
    }
}

public class AbilityDamage : Ability
{
    readonly float effectPerLevel = 0.8f;

    public AbilityDamage(int level)
    {
        maxLevel = 10;
        costs = new List<int>() { 5, 10, 20, 20, 20, 30, 30, 40, 50, 60 };
        abilityType = AbilityType.Damage;
        this.level = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level + 1);
    }
}