using HarmonyLib;
using SueTheSecondGeneration.Behavior;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SueTheSecondGeneration
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            Harmony harmony = new Harmony("mod.sue.SueTheSecondGeneration");
            harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            InformationManager.DisplayMessage(new InformationMessage("SueTheRichSecondGeneration OnGameStart"));
            if (gameStarterObject.GetType() == typeof(CampaignGameStarter))
            {
                CampaignGameStarter gameInitializer = (CampaignGameStarter)gameStarterObject;
                gameInitializer.AddBehavior(new TheSecondGenerateBehavior());
            }
        }
    }
}