﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;


namespace SeparatistCrisis
{
    public class Main : MBSubModuleBase
    {

        protected override void OnSubModuleLoad()
        {
            
        }

        /*
        #####This code changes the ruling clan of the southern empire on campaign start######
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
