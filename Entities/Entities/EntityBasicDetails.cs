﻿// Tidied up for version 0.3.

using System;
using System.Collections.Generic;

namespace RLEngine.Entities
{
	[Serializable]
	public class EntityBasicDetails
	{
		public readonly string EntityName;
		public readonly char Symbol;
		public readonly string FGColorName;
		public readonly List<Trait> Traits;

		public EntityBasicDetails(string entityName, char symbol, string fgColorName, List<Trait> traits)
		{
			EntityName = entityName;
			Symbol = symbol;
			FGColorName = fgColorName;
			Traits = traits;
		}

		public EntityBasicDetails(string entityName, char symbol, string fgColorName, Trait[] traits)
		{
			EntityName = entityName;
			Symbol = symbol;
			FGColorName = fgColorName;
			Traits = new List<Trait>();
			foreach (Trait trait in traits)
				Traits.Add(trait);
		}
	}
}
