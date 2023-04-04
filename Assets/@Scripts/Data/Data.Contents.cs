using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
	//#region PlayerData
	//[Serializable]
	//public class PlayerData
	//{
	//	public int level;
	//	public int maxHp;
	//	public int attack;
	//	public int totalExp;
	//}

	//[Serializable]
	//public class PlayerDataLoader : ILoader<int, PlayerData>
	//{
	//	public List<PlayerData> stats = new List<PlayerData>();

	//	public Dictionary<int, PlayerData> MakeDict()
	//	{
	//		Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
	//		foreach (PlayerData stat in stats)
	//			dict.Add(stat.level, stat);
	//		return dict;
	//	}
	//}
	//#endregion

	#region PlayerData
	public class PlayerData
	{
		[XmlAttribute]//xml 파일을 가져올 경우 xml의 Attribute값에 이런 것들이 들어간다는 것을 표시(안쓰면 잘 못찾음).
        public int level;
		[XmlAttribute]
		public int maxHp;
		[XmlAttribute]
		public int attack;
		[XmlAttribute]
		public int totalExp;
	}

	[Serializable, XmlRoot("PlayerDatas")]
	public class PlayerDataLoader : ILoader<int, PlayerData>
	{
		[XmlElement("PlayerData")]
		public List<PlayerData> stats = new List<PlayerData>();

		public Dictionary<int, PlayerData> MakeDict()
		{
			Dictionary<int, PlayerData> dict = new Dictionary<int, PlayerData>();
			foreach (PlayerData stat in stats)
				dict.Add(stat.level, stat);
			return dict;
		}
	}
	#endregion

	#region MonsterData
	public class MonsterData
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string prefab;
		[XmlAttribute]
		public int level;
		[XmlAttribute]
		public int maxHp;
		[XmlAttribute]
		public int attack;
		[XmlAttribute]
		public float speed;
		// DropData
		// - 일정 확률로
		// - 어떤 아이템을 (보석, 스킬 가차, 골드, 고기)
		// - 몇 개 드랍할지?
	}

	[Serializable, XmlRoot("MonsterDatas")]
	public class MonsterDataLoader : ILoader<string, MonsterData>
	{
		[XmlElement("MonsterData")]
		public List<MonsterData> stats = new List<MonsterData>();

		public Dictionary<string, MonsterData> MakeDict()
		{
			Dictionary<string, MonsterData> dict = new Dictionary<string, MonsterData>();
			foreach (MonsterData stat in stats)
				dict.Add(stat.name, stat);
			return dict;
		}
	}
	#endregion
}