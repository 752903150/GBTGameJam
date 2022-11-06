using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;

//CreateTime：2022/11/6 11:19:40
public partial class HelperForm
{
	private AutoBind autoBind;
	private Text m_txtDes;
	private Button m_btnClose;

	private void InitComponent()
	{
		autoBind = GetComponent<AutoBind>();
		m_txtDes = autoBind.itemList[0].obj.GetComponent<Text>();
		m_btnClose = autoBind.itemList[1].obj.GetComponent<Button>();
	}
}

