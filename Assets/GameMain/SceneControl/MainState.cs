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
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.KillMonster_str, KillMonster);
            
        }

        public override void StateUpdate()
        {
            //Debug.Log("MainState Update");
        }

        public override void StateEnd()
        {
            //SoundSystem.Instance.StopMusic(Data_AudioID.key_March_of_the_Brave);
            //Debug.Log("MainState End");
        }

        void CreateMainUI()
        {

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
            
        }

        void KillMonster(IEventArgs eventArgs)
        {
            KillMonsterEventArgs killMonsterEventArgs = (KillMonsterEventArgs)eventArgs;
            int killWave = killMonsterEventArgs.Wave;
        }
    }
}

