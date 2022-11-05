using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;

//CreateTime：2022/11/5 10:43:42
public partial class GameOverForm
{
	private AutoBind autoBind;
	private Button m_btnOK;
	private Text m_txtDeadDic;

	private void InitComponent()
	{
		autoBind = GetComponent<AutoBind>();
		m_btnOK = autoBind.itemList[0].obj.GetComponent<Button>();
		m_txtDeadDic = autoBind.itemList[1].obj.GetComponent<Text>();
	}
}

