using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using DataCs;
using UnityEngine;
using static UnityEngine.ParticleSystem;


public static class TOOLS
{
	private static Dictionary<uint, MonsterData> monsterIdMap;

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

    public static List<List<uint>> GetFirstMonsters(uint level, uint waveCount)
    {
        List<List<uint>> monsters = new List<List<uint>>();

        List<uint> list = new List<uint>();
        /*list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);

        List<EMonster> list2 = new List<EMonster>();
        list.Add(EMonster.EMA);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMB);
        list.Add(EMonster.EMC);*/
        
        

        return monsters;
    }
}
