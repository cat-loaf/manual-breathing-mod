using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria;

namespace ManualBreathing
{
    public class ManualBreathing : Mod
    {
        public static ModKeybind BreatheKey { get; private set; }
        public static NetworkText NoBreathDeathText;
        public override void Load()
        {
            BreatheKey = KeybindLoader.RegisterKeybind(
                this,
                "Breathe",
                Keys.B
            );
        }

        public override void Unload()
        {
            BreatheKey = null;
        }
    }
}
