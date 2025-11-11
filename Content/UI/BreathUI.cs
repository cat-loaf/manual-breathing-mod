using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ManualBreathing.Content.Players;
namespace ManualBreathing.Content.UI;

public class BreathUI : ModSystem
{
    public override void PostDrawInterface(SpriteBatch spriteBatch)
    {
        Player player = Main.LocalPlayer;
        ManualBreathingPlayer modPlayer = player.GetModPlayer<ManualBreathingPlayer>();

        if (modPlayer.autoBreathe) return;
        // Settings
        int bubbleValue = 20; // each bubble represents 20 breath
        int totalBubbles = modPlayer.maxBreathValue / bubbleValue;
        float scale = 0.05f; // scale of each bubble
        float bubbleSpacing = 25f; // spacing between bubbles, scaled
        float verticalOffset = 50f; // pixels above center of screen

        // Center the row horizontally
        float totalWidth = (totalBubbles - 1) * bubbleSpacing;
        Vector2 startPos = new Vector2(Main.screenWidth / 2f - totalWidth / 2f, Main.screenHeight / 2f - verticalOffset);

        // Load textures
        Texture2D fullTex = ModContent.Request<Texture2D>("ManualBreathing/Content/UI/breath_full").Value;
        Texture2D emptyTex = ModContent.Request<Texture2D>("ManualBreathing/Content/UI/breath_empty").Value;

        // Draw each bubble
        for (int i = 0; i < totalBubbles; i++)
        {
            Vector2 pos = startPos + new Vector2(i * bubbleSpacing, 0);

            // Draw empty bubble as base
            spriteBatch.Draw(
                emptyTex,
                pos,
                null,
                Color.White,
                0f,
                emptyTex.Size() / 2f,
                scale,
                SpriteEffects.None,
                0f
            );

            // Draw full bubble on top with transparency
            float bubbleFraction = (modPlayer.breathValue - i * bubbleValue) / (float)bubbleValue;
            bubbleFraction = MathHelper.Clamp(bubbleFraction, 0f, 1f);

            if (bubbleFraction > 0f)
            {
                spriteBatch.Draw(
                    fullTex,
                    pos,
                    null,
                    Color.White * bubbleFraction,
                    0f,
                    fullTex.Size() / 2f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
