using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;
using DataCs;

//CreateTime：2022/11/6 16:19:56
public partial class AllGameStartForm : UIForm
{

	float curr_time;
	float time;

	string des;
	int index;
	public override void Awake()
	{
		base.Awake();
		InitComponent(); 
	}

	public override void OnOpen(System.Object obj)
	{
		base.OnOpen(obj);
		RegisterEvent();
		curr_time = 0f;
		time = 0.2f;
		index = 0;
        des = "asdasdasas\ndasdasasdas\ndasasdasdas\nasdasdasasd\nasdas";
		m_txtDes.text = "";
    }

	public override void OnClose()
	{
		base.OnClose();
		ReleaseEvent(); 
	}

	public override void Update()
	{
		base.Update();
		curr_time += Time.deltaTime;
		if (curr_time >= time)
		{
			curr_time = 0f;
			if(index < des.Length)
			{
                m_txtDes.text = des.Substring(0, index);
            }
			else
			{
				if (index - des.Length > 15)
				{
					OnOver();

                }
			}
			
			index++;
        }
	}

	private void RegisterEvent()
	{
		
	}

	private void ReleaseEvent()
	{
		
	}

	private void OnOver()
	{
        EventManagerSystem.Instance.Invoke2(Data_EventName.BackStartGame_str, BackStartGameEventArgs.Create());
        UISystem.Instance.CloseUIForm(Data_UIFormID.key_AllGameStartForm, this);
    }


}

