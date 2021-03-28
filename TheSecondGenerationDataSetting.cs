using SueTheSecondGeneration.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace SueTheSecondGeneration
{
	class TheSecondGenerationDataSetting
	{

		private static TheSecondGenerationDataSetting _instance = new TheSecondGenerationDataSetting();

		public static TheSecondGenerationDataSetting Instance
		{
			get
			{
				return _instance;
			}

		}
		public bool EnableGroupInfluence { get; set; }
		public int ResourceGold { get; set; }
		public int ResourceMeat { get; set; }
		public int ClanTier { get; set; }
		public int ClanRenown { get; set; }
		public int ClanInfluence { get; set; }
		public bool EnableKimdom { get; set; }
		//public bool IsSupportImperial { get; set; }
		//public int FiefNumbers { get; set; }
		public TSGValueNamePair KindomCulture { get; set; }
		public List<TSGValueNamePair> FiefSettlements { get; set; }


		public bool EnableGroupCompanion { get; set; }
		//public bool EnableHero { get; set; }
		public int HeroNumbers { get; set; }
		public int HeroCoupleMaxNumbers { get; set; }
		public float HeroCoupleChildrenProbability { get; set; }
		public int HeroFromTier { get; set; }
		public int HeroSkillLevel { get; set; }
		public bool HeroTakeAllPerks { get; set; }


		public bool EnableGroupTroop { get; set; }
		public ArrayList Soldiers { get; set; }

		public bool EnableGroupPlayerFamily { get; set; }
		public TSGValueNamePair PlayerSpouse { get; set; }
		public ArrayList PlayerChildren { get; set; }
		public int PlayerSkill { get; set; }



		public List<TSGOptionGroup> GenerateSettingData()
		{

			TSGOptionBuilder optionBuilder = new TSGOptionBuilder();
			optionBuilder.BuildGroup(Instance, "EnableGroupInfluence", "{=tsg_setting_clan}Clan", "{=tsg_setting_clan_describe}Enable clan settings")
				//.AddOption(new TSGOptionItem("FiefNumbers", "{=trs_setting_fief_number}Fief Number", FiefNumbers, TSGOptionType.IntegerProperty, 0, 10))
				.AddOption(new TSGOptionItem(Instance, "EnableKimdom", "{=tsg_setting_kindom_creation}Enable kindom creation", EnableKimdom, TSGOptionType.BoolProperty))
				.AddOption(new TSGOptionItem(Instance, "KindomCulture", "{=tsg_setting_kindom_cultrue}Kindom cultrue", KindomCulture, TSGOptionType.SingleSelectProperty).FillSelectItems(CulturePairs()))
				.AddOption(new TSGOptionItem(Instance, "FiefSettlements", "{=tsg_setting_fielf}Fielf", FiefSettlements, TSGOptionType.MultipleSelectProperty).FillSelectItems(FiefPairs()))
				.AddOption(new TSGOptionItem(Instance, "ClanTier", "{=tsg_setting_clan_tier}Clan tier", ClanTier, TSGOptionType.IntegerProperty, 1, 6))
				.AddOption(new TSGOptionItem(Instance, "ClanRenown", "{=tsg_setting_clan_renown}Clan renown", ClanRenown, TSGOptionType.IntegerProperty, 0, 10000))
				.AddOption(new TSGOptionItem(Instance, "ClanInfluence", "{=tsg_setting_clan_influence}Clan influence", ClanInfluence, TSGOptionType.IntegerProperty, 0, 50000))
				.AddOption(new TSGOptionItem(Instance, "ResourceGold", "{=tsg_setting_gold}Gold", ResourceGold, TSGOptionType.IntegerProperty, 0, 300))
				.AddOption(new TSGOptionItem(Instance, "ResourceMeat", "{=tsg_setting_meat} Meat", ResourceGold, TSGOptionType.IntegerProperty, 0, 100));

			optionBuilder.BuildGroup(Instance, "EnableGroupCompanion", "{=tsg_setting_companion}Companion", "{=tsg_setting_companion_describe}Enable conpanion settings")
				.AddOption(new TSGOptionItem(Instance, "HeroNumbers", "{=tsg_setting_number}Number", HeroNumbers, TSGOptionType.IntegerProperty, 0, 100))
				.AddOption(new TSGOptionItem(Instance, "HeroCoupleMaxNumbers", "{=tsg_setting_companion_couple_max_num}Maximum number of couples", HeroCoupleMaxNumbers, TSGOptionType.IntegerProperty, 0, 50))
				.AddOption(new TSGOptionItem(Instance, "HeroCoupleChildrenProbability", "{=tsg_setting_companion_couple_has_children_probility}Probability of couples having children", HeroCoupleChildrenProbability, TSGOptionType.IntegerProperty, 0, 10))
				.AddOption(new TSGOptionItem(Instance, "HeroFromTier", "{=tsg_setting_companion_tier}Companion tier", HeroFromTier, TSGOptionType.IntegerProperty, 1, 6))
				.AddOption(new TSGOptionItem(Instance, "HeroSkillLevel", "{=tsg_setting_companion_skill_level}Companion sill level", HeroSkillLevel, TSGOptionType.IntegerProperty, 0, 300))
				.AddOption(new TSGOptionItem(Instance, "HeroTakeAllPerks", "{=tsg_setting_companion_perk_auto}Companion perk auto", HeroTakeAllPerks, TSGOptionType.BoolProperty));


			optionBuilder.BuildGroup(Instance, "EnableGroupTroop", "{=tsg_setting_troop}Troop", "{=tsg_setting_troop_describe}Enable troop settings")
				.AddOption(new TSGOptionItem(Instance, "Soldiers", "{=tsg_setting_troop_list}Troop List", Soldiers, TSGOptionType.ListProerty, SoldierFieldTemplate()).SetTargetInstanceType(typeof(SoldierTemplateData)));

			optionBuilder.BuildGroup(Instance, "EnableGroupPlayerFamily", "{=tsg_setting_player_family}Player family", "{=tsg_setting_player_family_describe}Enable player family settings")
			.AddOption(new TSGOptionItem(Instance, "PlayerSkill", "{=tsg_setting_player_skill_level}Player skill level", PlayerSkill, TSGOptionType.IntegerProperty, 0, 300))
			.AddOption(new TSGOptionItem(Instance, "PlayerSpouse", "{=tsg_setting_player_spouse}Player spouse", PlayerSpouse, TSGOptionType.SingleSelectProperty).FillSelectItems(HeroPairs()))
			.AddOption(new TSGOptionItem(Instance, "PlayerChildren", "{=tsg_setting_player_children_list}Children List", PlayerChildren, TSGOptionType.ListProerty, ChildrenTemplate()).SetTargetInstanceType(typeof(ChildrenTemplateData)));

			//optionBuilder.BuildGroup("{=trs_setting_resource}Resource", "启用该选项，将会给玩家一些初始资源和自身属性");
			return optionBuilder.Groups;
		}

		private List<TSGOptionItem> SoldierFieldTemplate() {
			List<TSGOptionItem> list = new List<TSGOptionItem>();
			list.Add(new TSGOptionItem(null, "Soldier", "{=tsg_setting_character}Character", SoldierPairs()[0], TSGOptionType.SingleSelectProperty).FillSelectItems(SoldierPairs()));
			list.Add(new TSGOptionItem(null, "Number", "{=tsg_setting_number}Number", 1, TSGOptionType.IntegerProperty, 0, 100));
			return list;
		}

		private List<TSGOptionItem> ChildrenTemplate()
		{
			List<TSGOptionItem> list = new List<TSGOptionItem>();
			list.Add(new TSGOptionItem(null, "Mother", "{=tsg_setting_player_children_mother}Children Mother", HeroPairs()[0], TSGOptionType.SingleSelectProperty).FillSelectItems(HeroPairs()));
			list.Add(new TSGOptionItem(null, "Number", "{=tsg_setting_number}Number", 1, TSGOptionType.IntegerProperty, 0, 20));
			list.Add(new TSGOptionItem(null, "Age", "{=tsg_setting_age}Age", 1, TSGOptionType.IntegerProperty, 1, 30));
			list.Add(new TSGOptionItem(null, "IsFemale", "{=tsg_setting_is_female}Is female", true, TSGOptionType.BoolProperty));
			list.Add(new TSGOptionItem(null, "MinSkill", "{=tsg_setting_skill_level_min}Min skill level", 1, TSGOptionType.IntegerProperty, 1, 300));
			list.Add(new TSGOptionItem(null, "MaxSkill", "{=tsg_setting_skill_level_max}Max skill level", 100, TSGOptionType.IntegerProperty, 1, 300));

			return list;
		}

		public TheSecondGenerationDataSetting()
		{

			this.EnableGroupCompanion = true;
			this.EnableGroupInfluence = true;
			this.EnableGroupTroop = true;
			this.EnableGroupPlayerFamily = true;

			this.EnableKimdom = true;
			//this.FiefNumbers = 1;
			//this.IsSupportImperial = true;
			this.ClanTier = 1;
			this.ClanRenown = 0;
			this.ResourceGold = 100;
			this.ResourceMeat = 50;
			this.KindomCulture = CulturePairs()[0];
			this.FiefSettlements = new List<TSGValueNamePair>();
			this.FiefSettlements.Add(FiefPairs()[0]);

			this.PlayerSkill = 100;

			
			this.HeroNumbers = 10;
			this.HeroSkillLevel = 100;
			this.HeroTakeAllPerks = true;
			this.HeroFromTier = 6;
			this.HeroCoupleMaxNumbers = 0;
			this.HeroCoupleChildrenProbability = 0f;

			
			this.Soldiers = new ArrayList();

		
			this.PlayerSpouse = HeroPairs()[0];
			this.PlayerChildren = new ArrayList();

		}

		List<TSGValueNamePair> _cultureList;
		public List<TSGValueNamePair> CulturePairs()
		{
			if (null == _cultureList)
			{
				List<TSGValueNamePair> list = new List<TSGValueNamePair>();
				foreach (CultureObject current in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
				{
					/*if (current.IsMainCulture)
					{

					}*/
					list.Add(new TSGValueNamePair(current, current.Name.ToString()));
				}
				_cultureList = list;
			}

			return _cultureList;
		}

		List<TSGValueNamePair> _heroList;
		public List<TSGValueNamePair> HeroPairs()
		{
			if (null == _heroList)
			{
				List<TSGValueNamePair> list = new List<TSGValueNamePair>();
				IEnumerable<CharacterObject> heros = CharacterObject.All.Where(
					obj => obj.IsHero && obj.IsFemale && obj.HeroObject.IsAlive && obj.Age < 43 && CharacterObject.PlayerCharacter != obj && (obj.Occupation == Occupation.Lord || obj.Occupation == Occupation.Wanderer));
				foreach (CharacterObject current in heros)
				{
					list.Add(new TSGValueNamePair(current, current.Name.ToString()));
				}
				_heroList = list;
			}

			return _heroList;
		}

		List<TSGValueNamePair> _fiefList;
		public List<TSGValueNamePair> FiefPairs()
		{
			if (null == _fiefList)
			{
				List<TSGValueNamePair> list = new List<TSGValueNamePair>();
				IEnumerable<Settlement> settlements = Settlement.All.Where(obj => obj.IsTown || obj.IsCastle);
				foreach (Settlement current in settlements)
				{
					list.Add(new TSGValueNamePair(current, current.Name.ToString()));
				}
				_fiefList = list;
			}

			return _fiefList;
		}

		List<TSGValueNamePair> _soldierList;
		public List<TSGValueNamePair> SoldierPairs()
		{
			if (null == _soldierList)
			{
				List<TSGValueNamePair> list = new List<TSGValueNamePair>();
				IEnumerable<CharacterObject> soldiers = CharacterObject.All.Where(obj => obj.IsSoldier);
				foreach (CharacterObject current in soldiers)
				{
					list.Add(new TSGValueNamePair(current, current.Name.ToString()));
				}
				_soldierList = list;
			}

			return _soldierList;
		}

		public class SoldierTemplateData
		{
			public int Number { get; set; }
			public TSGValueNamePair Soldier { get; set; }

			public SoldierTemplateData()
			{
				Number = 10;
				Soldier = TheSecondGenerationDataSetting.Instance.SoldierPairs()[0];
			}
		}

		public class ChildrenTemplateData{

			public int Number { get; set; }
			public TSGValueNamePair Mother { get; set; }
			public int Age { get; set; }
			public int MinSkill { get; set; }
			public int MaxSkill { get; set; }
			public bool IsFemale { get; set; }

			public ChildrenTemplateData()
			{
				Number = 1;
				Mother = TheSecondGenerationDataSetting.Instance.HeroPairs()[0];
				Age = 1;
				IsFemale = true;
				MinSkill = 1;
				MaxSkill = 100;
			}
		}

	}
}
