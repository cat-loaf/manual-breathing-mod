using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ManualBreathing.Config;

public class ManualBreathingConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(10)]
    [Range(0, int.MaxValue)]
    public int breathTickRate;

    [DefaultValue(false)]
    public bool enableInstaDeath;
}
