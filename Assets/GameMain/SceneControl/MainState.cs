using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DataCs;



namespace MyGameFrameWork
{
    public class MainState : ISceneState
    {

        GameObject Enity1;
        GameObject Spawn1;
        GameObject Player;
        GameObject HPBarCanvas;
        public MainState(SceneStateC c) : base(c)
        {
            this.StateName = "MainState";
        }

        public override void StateBegin(System.Object obj)
        {
            SkillAdditionSystem.CreateInstance(0,0,0);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.KillMonster_str, KillMonster);
            EventManagerSystem.Instance.Add2(Data_EventName.BackMenu_str, OnBackMenu);
            Enity1 = m_Contorller.GetData("Enity1") as GameObject;
            Enity1?.SetActive(true);

            Spawn1 = m_Contorller.GetData("Spawn1") as GameObject;
            Player = m_Contorller.GetData("Player") as GameObject;
            HPBarCanvas = m_Contorller.GetData("HPBarCanvas") as GameObject;

            Spawn1.GetComponent<EnemySpawn>().SpawnPlanA();
            Player.GetComponent<PlayerMove>().PlayerInit();

            //HpBarCanvas = m_Contorller.GetData("HpBarCanvas") as GameObject;
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
            EventManagerSystem.Instance.Delete2(Data_EventName.BackMenu_str, OnBackMenu);
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

            Enity1.SetActive(false);

            UISystem.Instance.OpenUIForm(Data_UIFormID.key_GameOverForm);
            //HPBarCanvas.
        }

        void KillMonster(IEventArgs eventArgs)
        {
            KillMonsterEventArgs killMonsterEventArgs = (KillMonsterEventArgs)eventArgs;
            int killWave = killMonsterEventArgs.Wave;
        }

        private void OnBackMenu(IEventArgs eventArgs)
        {
            m_Contorller.SetState("MenuState", null);
        }
    }
}

