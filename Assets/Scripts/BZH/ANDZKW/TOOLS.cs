using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using DataCs;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;


public static class TOOLS
{
	private static Dictionary<uint, MonsterData> monsterIdMap;

	private static Data_Empyrean empyreanData;

	static TOOLS()
	{
		var data = Data_Empyrean.GetDefaultObject();
		monsterIdMap = new Dictionary<uint, MonsterData>()
		{
			{1u, data.MonsterDatas[0]},
			{2u, data.MonsterDatas[1]},
			{3u, data.MonsterDatas[2]},
			{4u, data.MonsterDatas[3]},
		};
		empyreanData = Data_Empyrean.GetDefaultObject();
	}

	/// <summary>
	/// 通过怪物ID获取怪物信息类对象
	/// </summary>
	/// <param name="id">怪物ID</param>
	/// <returns></returns>
	public static MonsterData GetMonsterDataById(uint id)
	{
		return monsterIdMap[id];
	}

	public static float GetPlayerInitialHp() => PlayerData.GetDefaultObject().InitialHp;
	
    public static float GetPlayerDps(PlayerHpState stateWhenFire, uint monsterId, float monsterCurrHp, float monsterCurrDefense)//Attack monster damage
    {
	    PlayerData playerData = PlayerData.GetDefaultObject();
	    float originalDamage = playerData.NormalDamage;
	    if (stateWhenFire == PlayerHpState.Fever)
	    {
		    originalDamage = playerData.FeverDamage;
	    }
	    else if (stateWhenFire == PlayerHpState.Overheating)
	    {
		    originalDamage = playerData.OverheatingDamage;
	    }
	    
	    return GetMonsterDataById(monsterId)
		    .ApplyDamage(originalDamage * (1.0f + SkillAdditionSystem.Instance.DamageIncrease), monsterCurrHp, monsterCurrDefense);
    }

    public static float GetMonsterDps(uint monsterId, float playerCurrHp)//Take damage from monsters
    {
	    MonsterData monsterData = GetMonsterDataById(monsterId);
	    PlayerData playerData = PlayerData.GetDefaultObject();

	    return playerData.ApplyDamage(monsterData.Damage, playerCurrHp,
		    playerData.InitialDefense + SkillAdditionSystem.Instance.DefenseIncrease, monsterData);
    }

    public static float GetPlayerMaxHp()//Gain player health
    {
	    return PlayerData.GetDefaultObject().MaxHp;
    }

    public static int GetMonsterExp(uint monsterId)//Get Moster Exp
    {
	    return GetMonsterDataById(monsterId).OfferedExp;
    }

    public static string[] GetDialoguefirstlevel()//Get the dialogue from the first level
    {
        return new string[] 
        { 
            "1",
            "2",
            "3",
            "4",
        };
    }

    public static List<uint> GetFirstMonsters(uint level, uint waveIndex)
    {
	    if (level < 2u)
	    {
		    return empyreanData.MonstersInLevels[level][waveIndex];
	    }

	    var waves = empyreanData.MonstersInLevels[2];
	    if (waveIndex < waves.WavesCount)
	    {
		    return waves[waveIndex];
	    }

	    return GenerateRandomMonsters(waveIndex);
    }

    private static uint GenerateMonsterCount(uint waveIndex)
    {
	    System.Random rand = new System.Random();
	    if (waveIndex < 15u)
	    {
		    return (uint)rand.Next(5, 8);
	    }

	    if (waveIndex < 25u)
	    {
		    return (uint)rand.Next(8, 12);
	    }

	    return (uint)rand.Next(9, 15);
    }

    private static List<uint> GenerateRandomMonsters(uint waveIndex)
    {
	    uint total = GenerateMonsterCount(waveIndex);
	    uint m1Count = 0u, m2Count = 0u, m3Count = 0u;
	    m3Count = (uint)Random.Range(0.0f, 0.2f * total);
	    m2Count = (uint)Random.Range(0.1f * total, 4.5f);
	    m1Count = total - m2Count - m3Count;
	    MonsterWave wave = new MonsterWave(new MonsterConfig(1u, m1Count), new MonsterConfig(2u, m2Count),
		    new MonsterConfig(3u, m3Count));
	    return wave.AllMonsters;
    }
}
