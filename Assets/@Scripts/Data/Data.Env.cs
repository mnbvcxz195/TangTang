using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data
{
	public class DropData
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string type;
		[XmlAttribute]
		public int exp;
	}

	public class SkillDropData
	{
		[XmlAttribute]
		public string name;
	}

	public class GemDropData
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string prefab;
		[XmlAttribute]
		public string sprite;
		//[XmlAttribute]
		//public int exp;
	}

	public class GoldDropData
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string prefab;
		[XmlAttribute]
		public string sprite;
		//[XmlAttribute]
		//public int gold;
	}


}