using System.Collections;
using System.Collections.Generic;

//CreateTime：2022/11/5 13:11:45
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
			{"HPBar",new Data_GameObjectID_Struct("HPBar",1001,"ZKW/HPBar")},
			{"EnemyA",new Data_GameObjectID_Struct("EnemyA",1002,"ZKW/Enity/EnemyA")},
			{"Player",new Data_GameObjectID_Struct("Player",1003,"ZKW/Enity/Player")},
			{"Spawn1",new Data_GameObjectID_Struct("Spawn1",1004,"ZKW/Enity/Spawn1")},
			{"Tower",new Data_GameObjectID_Struct("Tower",1005,"ZKW/Enity/Tower")},
			{"Wall",new Data_GameObjectID_Struct("Wall",1006,"ZKW/Enity/Wall")},
		};
		public static string key_FireBullet = "FireBullet";
		public static string key_HPBar = "HPBar";
		public static string key_EnemyA = "EnemyA";
		public static string key_Player = "Player";
		public static string key_Spawn1 = "Spawn1";
		public static string key_Tower = "Tower";
		public static string key_Wall = "Wall";
	}
}

