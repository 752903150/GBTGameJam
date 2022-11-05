using System.Collections;
using System.Collections.Generic;

//CreateTime：2022/11/5 13:11:45
namespace DataCs
{
	public struct Data_UIFormID_Struct
	{
		public string key;
		public int ID;
		public string path;
		public int root;

		public Data_UIFormID_Struct(string _key, int _ID, string _path, int _root)
		{
			key = _key;
			ID = _ID;
			path = _path;
			root = _root;
		}
	}

	public static class Data_UIFormID
	{
		public static Dictionary<string, Data_UIFormID_Struct> Dic = new Dictionary<string, Data_UIFormID_Struct>()
		{
			{"StartGameForm",new Data_UIFormID_Struct("StartGameForm",1000,"ZKW/StartGameForm",1)},
			{"MainForm",new Data_UIFormID_Struct("MainForm",1001,"ZKW/MainForm",1)},
			{"DeveloperForm",new Data_UIFormID_Struct("DeveloperForm",1002,"ZKW/DeveloperForm",1)},
			{"MenuForm",new Data_UIFormID_Struct("MenuForm",1003,"ZKW/MenuForm",1)},
			{"GameOverForm",new Data_UIFormID_Struct("GameOverForm",1004,"ZKW/GameOverForm",1)},
		};
		public static string key_StartGameForm = "StartGameForm";
		public static string key_MainForm = "MainForm";
		public static string key_DeveloperForm = "DeveloperForm";
		public static string key_MenuForm = "MenuForm";
		public static string key_GameOverForm = "GameOverForm";
	}
}

