using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ManualBreathing.Config;

namespace ManualBreathing.Content.Players;

public class ManualBreathingPlayer : ModPlayer
{
    public int breathValue { get; private set; } = 200;
    public int maxBreathValue = 200;
    public int breathTimer = 0;
    public int dmgTimer = 0;
    public int breathTickRate = 10;
    public int dmgTickRate = 10;
    public bool autoBreathe = false;
    public override void ResetEffects()
    {
        var config = ModContent.GetInstance<ManualBreathingConfig>();
        breathTickRate = config.breathTickRate;

        breathTimer++;
        if (breathTimer <= breathTickRate) return;

        breathTimer = 0;
        if (breathValue > 0)
        {
            breathValue--;
        }
        else
        {
            dmgTimer++;
            if (dmgTimer <= dmgTickRate) return;
            dmgTimer = 0;

            NetworkText deathText = NetworkText.FromKey(
                "Mods.ManualBreathing.DeathMessage.ForgotToBreathe",
                Player.name
            );

            if (config.enableInstaDeath)
            {
                Player.KillMe(
                    PlayerDeathReason.ByCustomReason(deathText),
                    9999,
                    0,
                    false
                );
                return;
            }

            Player.Hurt(
                PlayerDeathReason.ByCustomReason(deathText),
                Damage: 10,
                hitDirection: 0,
                pvp: false,
                quiet: false,
                cooldownCounter: 300
            );
        }
    }

    public override void OnRespawn()
    {
        breathValue = maxBreathValue;
    }

    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        autoBreathe = false;
        if (ManualBreathing.BreatheKey.JustPressed)
        {
            breathValue += 20;
            if (breathValue > maxBreathValue)
                breathValue = maxBreathValue;

            if (Main.myPlayer == Player.whoAmI)
            {
                Vector2 spawnPos = Player.MouthPosition ?? Player.Center;
                Vector2 velocity = Player.direction * new Vector2(1.5f, 0f);

                int dustIndex = Dust.NewDust(
                    spawnPos,
                    4, 4,
                    DustID.Smoke,
                    velocity.X,
                    velocity.Y,
                    100,
                    default,
                    1.2f
                );
                Main.dust[dustIndex].noGravity = true;

            }
        }
    }

    public void RefillBreath()
    {
        breathValue = maxBreathValue;
    }
}
