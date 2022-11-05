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
        GameObject Spawn2;
        GameObject Spawn3;
        GameObject Spawn4;
        GameObject Spawn5;
        GameObject Spawn6;
        GameObject Player;

        GameObject Tower1;
        GameObject Tower2;
        GameObject Tower3;
        GameObject Tower4;
        GameObject Tower5;

        List<EnemySpawn> Spawns;

        GameObject HPBarCanvas;

        int curr_wave;
        int last_enemy;

        int cuur_level;
        int all_wave;
        public MainState(SceneStateC c) : base(c)
        {
            this.StateName = "MainState";
        }

        public override void StateBegin(System.Object obj)
        {
            curr_wave = 0;

            cuur_level = (int)obj;
            all_wave = TOOLS.GetMonsterWaves((uint)cuur_level);
            SkillAdditionSystem.CreateInstance(0,0,0);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.KillMonster_str, KillMonster);
            EventManagerSystem.Instance.Add2(Data_EventName.BackMenu_str, OnBackMenu);
            Enity1 = m_Contorller.GetData("Enity1") as GameObject;
            Enity1?.SetActive(true);
            Spawn1 = m_Contorller.GetData("Spawn1") as GameObject;
            Spawn2 = m_Contorller.GetData("Spawn2") as GameObject;
            Spawn3 = m_Contorller.GetData("Spawn3") as GameObject;
            Spawn4 = m_Contorller.GetData("Spawn4") as GameObject;
            Spawn5 = m_Contorller.GetData("Spawn5") as GameObject;
            Spawn6 = m_Contorller.GetData("Spawn6") as GameObject;
            Spawns = new List<EnemySpawn>();

            Spawns.Add(Spawn1.GetComponent<EnemySpawn>());
            Spawns.Add(Spawn2.GetComponent<EnemySpawn>());
            Spawns.Add(Spawn3.GetComponent<EnemySpawn>());
            Spawns.Add(Spawn4.GetComponent<EnemySpawn>());
            Spawns.Add(Spawn5.GetComponent<EnemySpawn>());
            Spawns.Add(Spawn6.GetComponent<EnemySpawn>());
            Player = m_Contorller.GetData("Player") as GameObject;
            HPBarCanvas = m_Contorller.GetData("HPBarCanvas") as GameObject;

            Tower1 = m_Contorller.GetData("Tower1") as GameObject;
            Tower2 = m_Contorller.GetData("Tower2") as GameObject;
            Tower3 = m_Contorller.GetData("Tower3") as GameObject;
            Tower4 = m_Contorller.GetData("Tower4") as GameObject;
            Tower5 = m_Contorller.GetData("Tower5") as GameObject;
            CreateTower();
            CreateEnemy();
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
            Spawns.Clear();
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
            Tower1.GetComponent<Tower>().init(ETurrutType.Normal, cuur_level);
            Tower2.GetComponent<Tower>().init(ETurrutType.Normal, cuur_level);
            Tower3.GetComponent<Tower>().init(ETurrutType.Normal, cuur_level);
            Tower4.GetComponent<Tower>().init(ETurrutType.Normal, cuur_level);
            Tower5.GetComponent<Tower>().init(ETurrutType.Center, cuur_level);
        }

        void CreateEnemySpawn()
        {

        }

        void CreateEnemy()
        {
            List<uint> Monsters  = TOOLS.GetFirstMonsters((uint)cuur_level, (uint)curr_wave);
            last_enemy = Monsters.Count;
            for (int i = 0; i < Monsters.Count; i++)
            {
                Spawns[i % Spawns.Count].SpawnPlan((int)Monsters[i]);
            }
        }

        void GameOver(IEventArgs eventArgs)
        {
            GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;

            UISystem.Instance.OpenUIForm(Data_UIFormID.key_GameOverForm,"您失败了");
            
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.2f);
            seq.AppendCallback(() => {
                Enity1.SetActive(false);
            });
        }

        void GameOverOK()
        {
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.2f);
            seq.AppendCallback(() => {
                Enity1.SetActive(false);
            });

            UISystem.Instance.OpenUIForm(Data_UIFormID.key_GameOverForm, "恭喜通过第"+(cuur_level+1).ToString()+"关！");
        }

        void KillMonster(IEventArgs eventArgs)
        {
            KillMonsterEventArgs killMonsterEventArgs = (KillMonsterEventArgs)eventArgs;
            int killWave = killMonsterEventArgs.Wave;
            last_enemy--;
            if (last_enemy == 0)
            {
                curr_wave++;
                if(curr_wave< all_wave)
                {
                    CreateEnemy();
                }
                else
                {
                    GameOverOK();
                    EventManagerSystem.Instance.Invoke2(DataCs.Data_EventName.GameOK_str, GameOKEventArgs.Create(cuur_level));
                }
                
            }
        }

        private void OnBackMenu(IEventArgs eventArgs)
        {
            m_Contorller.SetState("MenuState", null);
        }
    }
}

