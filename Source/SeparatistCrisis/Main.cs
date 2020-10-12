﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;
using TaleWorlds.MountAndBlade.CustomBattle;
using Debug = System.Diagnostics.Debug;

namespace SeparatistCrisis
{
    public class Main : MBSubModuleBase
    {

        protected override void OnSubModuleLoad()
        {

        }

        //This allows us to add custom maps to the custom battle mode
        //Loading custom_battle_scenes.xml in our module
        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            if (!(game.GameType is CustomGame))
            {
                return;
            }
            CustomGame customGame = (CustomGame)game.GameType;
            string filePath = Path.Combine("..", "..", "Modules", "SeparatistCrisis", "ModuleData", "custom_battle_scenes.xml");
            
            if (File.Exists(filePath))
            {
                customGame.LoadCustomBattleScenes(filePath);
            }
        }

        /*
        ##### This code changes the ruling clan of the southern empire on campaign start ######
        public override void OnGameInitializationFinished(Game game)
        {
            if (!(game.GameType is Campaign))
            {
                return;
            }
            Kingdom kingdom = MBObjectManager.Instance.GetObject<Kingdom>("empire_s");
            IReadOnlyList<Clan> clans = kingdom.Clans;
            kingdom.RulingClan = clans[1];
            Debug.Print(kingdom.RulingClan.Name.ToString());
        }
        */
    }
}
