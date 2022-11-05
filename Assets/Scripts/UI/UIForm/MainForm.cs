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

    public override void Awake()
	{
		base.Awake();
		InitComponent(); 
	}

	public override void OnOpen(System.Object obj)
	{
		base.OnOpen(obj);
		RegisterEvent();
        PlayerHp = TOOLS.GetPlayerMaxHp();
        CurrPlayerHp = PlayerData.GetDefaultObject().InitialHp;

		m_scrollbarInjure.value = 1f;
		m_scrollbarInjure.size = CurrPlayerHp / PlayerHp;
		m_txtHP.text = ((int)CurrPlayerHp).ToString();
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
    }

	private void ReleaseEvent()
	{
        EventManagerSystem.Instance.Delete2(Data_EventName.PlayerInjure_str, PlayerInjure);
        EventManagerSystem.Instance.Delete2(Data_EventName.GameOver_str, GameOver);
    }

	void PlayerInjure(IEventArgs eventArgs)
	{
		PlayerInjureEventArgs playerInjureEventArgs = eventArgs as PlayerInjureEventArgs;

        m_scrollbarInjure.size = playerInjureEventArgs.DPS / PlayerHp;
        m_txtHP.text = ((int)playerInjureEventArgs.DPS).ToString();

    }

    void GameOver(IEventArgs eventArgs)
    {
        GameOverEventArgs gameOverEventArgs = (GameOverEventArgs)eventArgs;
		UISystem.Instance.CloseUIForm(Data_UIFormID.key_MainForm, this);
    }
}

