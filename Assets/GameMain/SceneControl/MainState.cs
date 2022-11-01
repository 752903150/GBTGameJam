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
            //Debug.Log("MainState");
            SoundSystem.Instance.PlayMusic(Data_AudioID.key_March_of_the_Brave);//²¥·ÅÒôÀÖ
            //UISystem.Instance.OpenUIForm(Data_UIFormID.key_MainForm, obj);

            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() =>
            {
                m_Contorller.SetState("StartState", null);
            })
            .SetDelay(3f);
        }

        public override void StateUpdate()
        {
            //Debug.Log("MainState Update");
        }

        public override void StateEnd()
        {
            SoundSystem.Instance.StopMusic(Data_AudioID.key_March_of_the_Brave);
            //Debug.Log("MainState End");
        }
    }
}

