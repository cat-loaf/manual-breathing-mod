using ManualBreathing.Content.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using ManualBreathing.Content.Players;

namespace ManualBreathing.Content.NPCs;

public class IronLungGuideDialogue : GlobalNPC
{
    public override void GetChat(NPC npc, ref string chat)
    {
        if (npc.type != NPCID.Guide)
            return;

        Player player = Main.LocalPlayer;

        bool hasIronLung = player.HasItem(ModContent.ItemType<IronLung>());
        bool hasIronLungEquipped = false;
        for (int i = 0; i < player.armor.Length; i++)
        {
            if (player.armor[i].type == ModContent.ItemType<IronLung>())
            {
                hasIronLungEquipped = true;
                break;
            }
        }

        ManualBreathingPlayer modPlayer = player.GetModPlayer<ManualBreathingPlayer>();
        if (modPlayer.breathValue <= modPlayer.maxBreathValue / 3)
        {
            chat = Language.GetTextValue("Mods.ManualBreathing.Dialogue.GuideTips.LowBreath");
            return;
        }

        if (!(hasIronLung || hasIronLungEquipped))
        {

            if (Main.rand.NextBool(2))
            {
                chat = Language.GetTextValue("Mods.ManualBreathing.Dialogue.GuideTips.IronLung" + Main.rand.Next(1, 5));
            }
        }
    }
}

