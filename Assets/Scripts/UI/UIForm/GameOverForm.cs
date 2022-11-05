using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;
using DataCs;

//CreateTime：2022/11/5 10:42:13
public partial class GameOverForm : UIForm
{
	public override void Awake()
	{
		base.Awake();
		InitComponent(); 
	}

	public override void OnOpen(System.Object obj)
	{
		base.OnOpen(obj);
		RegisterEvent(); 
	}

	public override void OnClose()
	{
		base.OnClose();
		ReleaseEvent(); 
	}

	private void RegisterEvent()
	{
		m_btnOK.onClick.AddListener(OnBtnOK);
		//m_btnDeadDic.onClick.AddListener(OnBtnDeadDic);
	}

	private void ReleaseEvent()
	{
		m_btnOK.onClick.RemoveListener(OnBtnOK);
		//m_btnDeadDic.onClick.RemoveListener(OnBtnDeadDic);
	}

	private void OnBtnOK()
	{
        EventManagerSystem.Instance.Invoke2(Data_EventName.BackMenu_str, BackMenuEventArgs.Create());
        UISystem.Instance.CloseUIForm(Data_UIFormID.key_GameOverForm, this);
    }
}

