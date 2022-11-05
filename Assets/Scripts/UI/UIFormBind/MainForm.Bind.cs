using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;

//CreateTime：2022/11/5 9:14:01
public partial class MainForm
{
	private AutoBind autoBind;
	private Scrollbar m_scrollbarInjure;
	private Text m_txtHP;

	private void InitComponent()
	{
		autoBind = GetComponent<AutoBind>();
		m_scrollbarInjure = autoBind.itemList[0].obj.GetComponent<Scrollbar>();
		m_txtHP = autoBind.itemList[1].obj.GetComponent<Text>();
	}
}

