using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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
    protected ReactiveProperty<int> level = new ReactiveProperty<int>();
    protected List<int> costs = new List<int>();

    public abstract float GetCurrentEffect();
    public abstract float GetNextEffect();

    public int GetLevel()
    {
        return level.Value;
    }

    public bool IsMaxLevel()
    {
        return level.Value >= maxLevel;
    }

    public bool LevelUp()
    {
        if (level.Value + 1 > maxLevel) return false;

        level.Value++;

        return true;
    }

    public int GetUpgradeCost()
    {
        return costs.Count > level.Value ? costs[level.Value] : 99999;
    }

    public ReactiveProperty<int> SubscribeLevel()
    {
        return level;
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
        this.level.Value = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level.Value;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level.Value + 1);
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
        this.level.Value = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level.Value;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level.Value + 1);
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
        this.level.Value = level;
    }

    public override float GetCurrentEffect()
    {
        return effectPerLevel * level.Value;
    }

    public override float GetNextEffect()
    {
        return effectPerLevel * (level.Value + 1);
    }
}