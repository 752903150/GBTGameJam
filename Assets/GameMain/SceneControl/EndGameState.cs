using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;

namespace MyGameFrameWork
{
    public class EndGameState : ISceneState
    {
        public EndGameState(SceneStateC c) : base(c)
        {
            this.StateName = "EndGameState";
        }

        public override void StateBegin(System.Object obj)
        {
            UISystem.Instance.OpenUIForm(Data_UIFormID.key_DeveloperForm);
            SoundSystem.Instance.PlayMusic(Data_AudioID.key_Dark_Journey);//≤•∑≈“Ù¿÷
            EventManagerSystem.Instance.Add2(Data_EventName.BackStartGame_str, OnBackStartGame);
        }

        public override void StateUpdate()
        {
            //Debug.Log("MainState Update");
        }

        public override void StateEnd()
        {

            SoundSystem.Instance.StopMusic(Data_AudioID.key_Dark_Journey);//≤•∑≈“Ù¿÷
        }

        private void OnBackStartGame(IEventArgs eventArgs)
        {
            m_Contorller.SetState("StartState", null);
        }
    }
}

