using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DataCs;



namespace MyGameFrameWork
{
    public class MainState : ISceneState
    {
        public MainState(SceneStateC c) : base(c)
        {
            this.StateName = "MainState";
        }

        public override void StateBegin(System.Object obj)
        {
            SkillAdditionSystem.CreateInstance(0,0,0);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.KillMonster_str, KillMonster);
            GameObject temp = m_Contorller.GetData("Enity1") as GameObject;
            temp?.SetActive(true);
            CreateMainUI();
        }

        public override void StateUpdate()
        {
            //Debug.Log("MainState Update");
        }

        public override void StateEnd()
        {
            EventManagerSystem.Instance.Delete2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Delete2(DataCs.Data_EventName.KillMonster_str, KillMonster);
        }

        void CreateMainUI()
        {
            UISystem.Instance.OpenUIForm(Data_UIFormID.key_MainForm);
        }

        void CreatePlayer()
        {

        }

        void CreateTower()
        {

        }

        void CreateEnemySpawn()
        {

        }

        void GameOver(IEventArgs eventArgs)
        {
            GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
            Debug.Log("Dead");
        }

        void KillMonster(IEventArgs eventArgs)
        {
            KillMonsterEventArgs killMonsterEventArgs = (KillMonsterEventArgs)eventArgs;
            int killWave = killMonsterEventArgs.Wave;
        }
    }
}

