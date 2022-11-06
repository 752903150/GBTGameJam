using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;
using DataCs;

//CreateTime：2022/11/5 9:14:01
public partial class MainForm : UIForm
{
    float PlayerHp;
    float CurrPlayerHp;

	float MainTowerHp;
	float CurrMainTowerHp;

	float MaxX;
	float MinX;

	float MaxTowerY;
	float MinTowerY;


    public override void Awake()
	{
		base.Awake();
		InitComponent(); 
	}

	public override void OnOpen(System.Object obj)
	{
		base.OnOpen(obj);
		MaxX = 985f;
		MinX = 720f;

		MaxTowerY = 377f;
		MinTowerY = 129f;

        RegisterEvent();
        PlayerHp = TOOLS.GetPlayerMaxHp();
        CurrPlayerHp = PlayerData.GetDefaultObject().InitialHp;
		MainTowerHp = (float)obj;
		CurrMainTowerHp = MainTowerHp;

		//m_scrollbarInjure.value = 1f;
		//m_scrollbarInjure.size = CurrPlayerHp / PlayerHp;
		SetTowerHp(1F);
        SetHp(CurrPlayerHp / PlayerHp);
        m_txtHP.text = ((int)CurrPlayerHp).ToString();
        m_txtTowerHP.text = ((int)CurrMainTowerHp).ToString();
    }

	public override void OnClose()
	{
		base.OnClose();
		ReleaseEvent(); 
	}

	private void RegisterEvent()
	{
		EventManagerSystem.Instance.Add2(Data_EventName.PlayerInjure_str, PlayerInjure);
        EventManagerSystem.Instance.Add2(Data_EventName.GameOver_str, GameOver);
        EventManagerSystem.Instance.Add2(Data_EventName.GameOK_str, GameOK);
		EventManagerSystem.Instance.Add2(Data_EventName.MainTowerInjure_str, MainTowerInjure);
    }

	private void ReleaseEvent()
	{
        EventManagerSystem.Instance.Delete2(Data_EventName.PlayerInjure_str, PlayerInjure);
        EventManagerSystem.Instance.Delete2(Data_EventName.GameOver_str, GameOver);
        EventManagerSystem.Instance.Delete2(Data_EventName.GameOK_str, GameOK);
        EventManagerSystem.Instance.Delete2(Data_EventName.MainTowerInjure_str, MainTowerInjure);
    }

	void PlayerInjure(IEventArgs eventArgs)
	{
		PlayerInjureEventArgs playerInjureEventArgs = eventArgs as PlayerInjureEventArgs;

		CurrPlayerHp -= playerInjureEventArgs.DPS;

		if (CurrPlayerHp <= 0)
		{
			CurrPlayerHp = 0;
        }
		if (CurrPlayerHp >= PlayerHp)
		{
			CurrPlayerHp = PlayerHp;

        }

		//m_scrollbarInjure.size = CurrPlayerHp / PlayerHp;
		SetHp(CurrPlayerHp / PlayerHp);
        m_txtHP.text = ((int)CurrPlayerHp).ToString();
		//m_scrollbarInjure.value = 1f;
    }

	void MainTowerInjure(IEventArgs eventArgs)
	{
		

		MainTowerInjureEventArgs mainTowerInjureEventArgs = (MainTowerInjureEventArgs)eventArgs;
		float DPS = mainTowerInjureEventArgs.DPS;
        //Debug.Log("TowerInjure:"+DPS.ToString());

        CurrMainTowerHp -= DPS;

        if (CurrMainTowerHp <= 0)
        {
            CurrMainTowerHp = 0;
        }
        if (CurrMainTowerHp >= MainTowerHp)
        {
            CurrMainTowerHp = MainTowerHp;

        }
        //m_scrollbarInjure.size = CurrPlayerHp / PlayerHp;
        SetTowerHp(CurrMainTowerHp / MainTowerHp);
        m_txtTowerHP.text = ((int)CurrMainTowerHp).ToString();
        //m_scrollbarInjure.value = 1f;
    }

    void GameOver(IEventArgs eventArgs)
    {
        GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
		UISystem.Instance.CloseUIForm(Data_UIFormID.key_MainForm, this);
    }

    void GameOK(IEventArgs eventArgs)
    {
        
        UISystem.Instance.CloseUIForm(Data_UIFormID.key_MainForm, this);
    }

	void SetHp(float pre)
	{
		Vector3 temp = m_imgSubImg.rectTransform.anchoredPosition;
		temp.x = pre * (MaxX - MinX) + MinX;
		m_imgSubImg.rectTransform.anchoredPosition = temp;

    }

	void SetTowerHp(float pre)
	{
        Vector3 temp = m_imgTowerSubImg.rectTransform.anchoredPosition;
        temp.y = pre * (MaxTowerY - MinTowerY) + MinTowerY;
        m_imgTowerSubImg.rectTransform.anchoredPosition = temp;
    }
}

