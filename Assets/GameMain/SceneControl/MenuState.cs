using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DataCs;



namespace MyGameFrameWork
{
    public class MenuState : ISceneState
    {

        GameObject Enity1;
        GameObject HpBarCanvas;
        public MenuState(SceneStateC c) : base(c)
        {
            this.StateName = "MenuState";
        }

        public override void StateBegin(System.Object obj)
        {
            /*SkillAdditionSystem.CreateInstance(0, 0, 0);
            
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.KillMonster_str, KillMonster);
            Enity1 = m_Contorller.GetData("Enity1") as GameObject;
            Enity1?.SetActive(true);*/

            //HpBarCanvas = m_Contorller.GetData("HpBarCanvas") as GameObject;
            EventManagerSystem.Instance.Add2(DataCs.Data_EventName.OpenLevel1_str, OpenLevel1);
            EventManagerSystem.Instance.Add2(Data_EventName.BackStartGame_str, OnBackStartGame);
            CreateMainUI();
        }

        public override void StateUpdate()
        {
            //Debug.Log("MainState Update");
        }

        public override void StateEnd()
        {
            /*EventManagerSystem.Instance.Delete2(DataCs.Data_EventName.GameOver_str, GameOver);
            EventManagerSystem.Instance.Delete2(DataCs.Data_EventName.KillMonster_str, KillMonster);*/
        }

        void CreateMainUI()
        {
            UISystem.Instance.OpenUIForm(Data_UIFormID.key_MenuForm);
        }

        void OpenLevel1(IEventArgs eventArgs)
        {
            m_Contorller.SetState("MainState");
        }

        private void OnBackStartGame(IEventArgs eventArgs)
        {
            m_Contorller.SetState("StartState", null);
        }


        /*void KillMonster(IEventArgs eventArgs)
        {
            KillMonsterEventArgs killMonsterEventArgs = (KillMonsterEventArgs)eventArgs;
            int killWave = killMonsterEventArgs.Wave;
        }*/
    }
}
