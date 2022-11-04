using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum EMonster
{
    EMA,
    EMB,
    EMC,
    None,
}

public class Player
{
    int MaxHp;
    int CurrExp;
    int CurrLevel;
    int CurrGoal;
}

public class TOOLS 
{
    static int GetPlayerDps(EMonster monster)//Attack monster damage
    {
        return 1;
    }

    static int GetMonsterDps(EMonster monster)//Take damage from monsters
    {
        return 1;
    }

    static int GetPlayerMaxHp()//Gain player health
    {
        return 100;
    }

    static int GetMonsterExp(EMonster monster)//Get Moster Exp
    {
        return 1;
    }

    static int GetPlayerNextExp()
    {
        return 100;
    }

    static string[] GetDialoguefirstlevel()//Get the dialogue from the first level
    {
        return new string[] { 
            "1",
            "2",
            "3",
            "4",
        };
    }

    static List<List<EMonster>> GetFirstMonsters()
    {
        List<List<EMonster>> monsters = new List<List<EMonster>>();

        List<EMonster> list = new List<EMonster>();
        list.Add(EMonster.EMB);
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
        list.Add(EMonster.EMC);

        return monsters;
    }
}
