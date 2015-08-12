/*
 * Crée par SharpDevelop.
 * Utilisateur: Mickael-Blet
 * Date: 02/11/2014
 * Heure: 03:34
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

using AddProcess;
using MemoryLib;

namespace BOBY_Shulack
{
	/// <summary>
	/// Description of Ability_List.
	/// </summary>
	public class Ability_List
	{
		public int skillfound = 0;
		
		public Dictionary<long, Ability> Abilities { get; set; }

		public HashSet<long> AbilityAddresses { get; set; }

		public int AbilitiesCount { get; set; }
		
		public Dictionary<long, ChainSkill> ChainsSkills { get; set; }
		public Dictionary<long, ChainSkill> ChainsSkills2 { get; set; }
		
		public void UpdateAbilities()
		{
			if (Abilities == null)
			{
				Abilities = new Dictionary<long, Ability>();
				AbilityAddresses = new HashSet<long>();
			}
			else
			{
				Abilities.Clear();
				AbilityAddresses.Clear();
			}
			
			//var abilitiesPointer = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.Game.TESTAbyPt);
			//var abilitiesFirstItem = SplMemory.ReadLong(abilitiesPointer + (long)Offset.Game.TESTAbyFirst);
			var abilitiesPointer = SplMemory.ReadLong(AionProcess.Modules.Game + (long)Offset.AbilityList.Pointer);
			var abilitiesFirstItem = SplMemory.ReadLong(abilitiesPointer + (long)Offset.AbilityList.FirstItem);

			Func<long, long> getAbilityPointer = (baseAddress) =>
			{
				try
				{
					var abilityPointer = SplMemory.ReadLong(baseAddress + (long)Offset.AbilityArrayItem.AbilityPointer); // + 0x14
					abilityPointer = SplMemory.ReadLong(abilityPointer + (long)Offset.AbilityPointers.First);
					abilityPointer = SplMemory.ReadLong(abilityPointer + (long)Offset.AbilityPointers.Second);
					abilityPointer = SplMemory.ReadLong(abilityPointer + (long)Offset.AbilityPointers.Third);
					abilityPointer = SplMemory.ReadLong(abilityPointer + (long)Offset.AbilityPointers.Final);
					return abilityPointer;
				}
				catch { }
				return 0;
			};

			Action<long> addAbility = null;
			addAbility = (baseAddress) =>
			{
				var abilityID = SplMemory.ReadInt(baseAddress + (long)Offset.AbilityArrayItem.AbilityID);
				var abilityAcquired = SplMemory.ReadLong(baseAddress + (long)Offset.AbilityArrayItem.AbilityAcquired);
				if (abilityAcquired == 1)
				{
					var abilityPointer = getAbilityPointer(baseAddress);
					if (abilityPointer != 0)
					{
						var id = SplMemory.ReadInt(abilityPointer + (long)Offset.Ability.ID);
						var nameLength = SplMemory.ReadInt(abilityPointer + (long)Offset.Ability.NameLength);
						var name = SplMemory.ReadWchar(abilityPointer + (long)Offset.Ability.Name, nameLength);
						if (!Regex.IsMatch(name, @"^\p{IsBasicLatin}"))
						{
							var nameAddress = SplMemory.ReadLong(abilityPointer + (long)Offset.Ability.Name);
							name = SplMemory.ReadWchar(nameAddress, nameLength);
							this.skillfound = 1;
						}
						//MessageBox.Show(name);
                        Console.WriteLine(name);

						if (!string.IsNullOrEmpty(name.Trim()))
						{
							var lastUseTimestamp = SplMemory.ReadLong(abilityPointer + (long)Offset.Ability.LastUseTimestamp);
							var cooldown = SplMemory.ReadInt(abilityPointer + (long)Offset.Ability.Cooldown);
							var cooldownEnd = SplMemory.ReadLong(abilityPointer + (long)Offset.Ability.CooldownEnd);
							var castTime = SplMemory.ReadInt(abilityPointer + (long)Offset.Ability.CastTime);

							var ability = new Ability(abilityPointer, id, name, lastUseTimestamp, cooldown, cooldownEnd, castTime);
							if (!Abilities.ContainsKey(abilityPointer))
								Abilities.Add(abilityPointer, ability);
						}
					}
				}

				if (AbilityAddresses.Add(baseAddress))
				{
					addAbility(SplMemory.ReadLong(baseAddress + (long)Offset.AbilityArrayItem.Pointer1));
					addAbility(SplMemory.ReadLong(baseAddress + (long)Offset.AbilityArrayItem.Pointer2));
					addAbility(SplMemory.ReadLong(baseAddress + (long)Offset.AbilityArrayItem.Pointer3));
				}
			};

			addAbility(abilitiesFirstItem);
		}
		
		public void UpdateChainsManager2()
		{
			const int chainsCount = 18;

			if (ChainsSkills2 == null)
				ChainsSkills2 = new Dictionary<long, ChainSkill>();
			else
				ChainsSkills2.Clear();

			var chainsManagerAddress = SplMemory.ReadLong((long)Offset.Base_windows.newbase["support_shortcut_dialog"]);
			var chainsArrayStart = chainsManagerAddress + (long)Offset.ChainsManager.ArrayStart2;
			for (int chainIndex = 0; chainIndex < chainsCount; chainIndex++)
			{
				var chainSkillAddress = SplMemory.ReadLong(chainsArrayStart + (long)chainIndex * 4);
				if (chainSkillAddress != 0x0)
				{
					var abilityID = SplMemory.ReadInt(chainSkillAddress + (long)Offset.Chain.AbilityId);

					if (abilityID != 0x0)
					{
						var chainSkill = new ChainSkill(chainSkillAddress, abilityID);
						ChainsSkills2.Add(chainSkill.Address, chainSkill);
					}
				}
			}
		}
		
		public void UpdateChainsManager()
		{
			const int chainsCount = 18;

			if (ChainsSkills == null)
				ChainsSkills = new Dictionary<long, ChainSkill>();
			else
				ChainsSkills.Clear();

			var chainsManagerAddress = SplMemory.ReadLong((long)Offset.Base_windows.newbase["support_shortcut_dialog"]);
			var chainsArrayStart = chainsManagerAddress + (long)Offset.ChainsManager.ArrayStart;
			for (int chainIndex = 0; chainIndex < chainsCount; chainIndex++)
			{
				var chainSkillAddress = SplMemory.ReadLong(chainsArrayStart + (long)chainIndex * 4);
				if (chainSkillAddress != 0x0)
				{
					var abilityID = SplMemory.ReadInt(chainSkillAddress + (long)Offset.Chain.AbilityId);

					if (abilityID != 0x0)
					{
						var chainSkill = new ChainSkill(chainSkillAddress, abilityID);
						ChainsSkills.Add(chainSkill.Address, chainSkill);
					}
				}
			}
		}
	}
	
	public class Ability
	{
		public long Address { get; set; }

		public int ID { get; set; }

		public string Name { get; set; }

		public long LastUseTimestamp { get; set; }

		public int Cooldown { get; set; }

		public long CooldownEnd { get; set; }

		public int CastTime { get; set; }

		public Ability(long address, int id, string name, long lastUseTimestamp, int cooldown, long cooldownEnd, int castTime)
		{
			Address = address;
			ID = id;
			Name = name;
			LastUseTimestamp = lastUseTimestamp;
			Cooldown = cooldown;
			CooldownEnd = cooldownEnd;
			CastTime = castTime;
		}
	}
	
	public class ChainSkill
	{
		public long Address { get; set; }

		public int AbilityID { get; set; }

		public ChainSkill(long address, int abilityID)
		{
			Address = address;
			AbilityID = abilityID;
		}
	}
}
