using System.Collections;
using System.Collections.Generic;

//CreateTime：2022/11/4 21:42:36
namespace DataCs
{
	public struct Data_GameObjectID_Struct
	{
		public string key;
		public int ID;
		public string path;

		public Data_GameObjectID_Struct(string _key, int _ID, string _path)
		{
			key = _key;
			ID = _ID;
			path = _path;
		}
	}

	public static class Data_GameObjectID
	{
		public static Dictionary<string, Data_GameObjectID_Struct> Dic = new Dictionary<string, Data_GameObjectID_Struct>()
		{
			{"FireBullet",new Data_GameObjectID_Struct("FireBullet",1000,"ZKW/Enity/FireBullet")},
		};
		public static string key_FireBullet = "FireBullet";
	}
}

