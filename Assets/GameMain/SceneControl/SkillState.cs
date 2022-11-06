using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataCs;

namespace MyGameFrameWork
{
    public class SkillState : ISceneState
    {
        public SkillState(SceneStateC c) : base(c)
        {
            this.StateName = "SkillState";
        }

        public override void StateBegin(System.Object obj)
        {
            //开始时
            Debug.Log(1.1);
            int lastKill = (int)m_Contorller.GetData("lastKill");
            Debug.Log(1.2);
            int defenseKill = (int)m_Contorller.GetData("defenseKill");
            Debug.Log(1.3);
            int attackSkill = (int)m_Contorller.GetData("attackSkill");
            Debug.Log(1.4);
            int attackSkillSpeed = (int)m_Contorller.GetData("attackSkillSpeed");
            Debug.Log(1.5);
            UISystem.Instance.OpenUIForm(Data_UIFormID.key_SkillForm,new SkillFormStruct(lastKill, defenseKill, attackSkill, attackSkillSpeed));//打开UI
            Debug.Log(1.6);

            EventManagerSystem.Instance.Add2(Data_EventName.SaveSkill_str, OnSaveSkill);
            EventManagerSystem.Instance.Add2(Data_EventName.BackMenu_str, OnBackMenu);
        }

        public override void StateUpdate()
        {

        }

        public override void StateEnd()
        {
            //结束时

            EventManagerSystem.Instance.Delete2(Data_EventName.SaveSkill_str, OnSaveSkill);
            EventManagerSystem.Instance.Delete2(Data_EventName.BackMenu_str, OnBackMenu);
        }



        private void OnSaveSkill(IEventArgs eventArgs)
        {
            SaveSkillEventArgs saveSkillEventArgs = (SaveSkillEventArgs)eventArgs;

            m_Contorller.SetData("lastKill", saveSkillEventArgs.lastKill);
            m_Contorller.SetData("defenseKill", saveSkillEventArgs.defence);
            m_Contorller.SetData("attackSkill", saveSkillEventArgs.attack);
            m_Contorller.SetData("attackSkillSpeed", saveSkillEventArgs.attackSpeed);
        }

        private void OnBackMenu(IEventArgs eventArgs)
        {
            m_Contorller.SetState("MenuState");
        }
    }
}

