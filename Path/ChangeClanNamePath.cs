using HarmonyLib;
using StoryMode.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SueTheSecondGeneration.Path
{
    class ChangeClanNamePath
    {

		[HarmonyPatch(typeof(FirstPhaseCampaignBehavior), "OnChangeClanNameDone")]
		internal class OnChangeClanNameDonePath
		{
			private static void Postfix(string newClanName)
			{
				//GameStartBusiness.PreOnGameStart();
			}
		}
	}
}
