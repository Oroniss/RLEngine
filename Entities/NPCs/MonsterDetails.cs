﻿using System;
using System.Collections.Generic;
using RLEngine.Entities.Actors;

namespace RLEngine.Entities.Monsters
{
	[Serializable]
	public class MonsterDetails:ActorDetails
	{
		public MonsterDetails(string entityName, char symbol, string fgColorName, List<Trait> traits)
			: base(entityName, symbol, fgColorName, traits) { }

		public MonsterDetails(string entityName, char symbol, string fgColorName, Trait[] traits)
			: base(entityName, symbol, fgColorName, traits) { }
	}
}
