using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;

//CreateTime：2022/11/6 7:49:14
public partial class MainForm
{
	private AutoBind autoBind;
	private Image m_imgHead;
	private Scrollbar m_scrollbarInjure;
	private Text m_txtHP;
	private Text m_txtTowerHP;
	private Image m_imgHpImage;
	private Image m_imgSubImg;
	private Image m_imgTowerHp;
	private Image m_imgTowerSubImg;

	private void InitComponent()
	{
		autoBind = GetComponent<AutoBind>();
		m_imgHead = autoBind.itemList[0].obj.GetComponent<Image>();
		m_scrollbarInjure = autoBind.itemList[1].obj.GetComponent<Scrollbar>();
		m_txtHP = autoBind.itemList[2].obj.GetComponent<Text>();
		m_txtTowerHP = autoBind.itemList[3].obj.GetComponent<Text>();
		m_imgHpImage = autoBind.itemList[4].obj.GetComponent<Image>();
		m_imgSubImg = autoBind.itemList[5].obj.GetComponent<Image>();
		m_imgTowerHp = autoBind.itemList[6].obj.GetComponent<Image>();
		m_imgTowerSubImg = autoBind.itemList[7].obj.GetComponent<Image>();
	}
}

