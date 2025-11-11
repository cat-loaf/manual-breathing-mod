using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ManualBreathing.Content.Players;
namespace ManualBreathing.Content.Items.Accessories;

[AutoloadEquip(EquipType.Body)]
public class IronLung : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.value = 10000;
        Item.rare = ItemRarityID.Green;
    }

    public override void UpdateEquip(Player player)
    {
        ManualBreathingPlayer modPlayer = player.GetModPlayer<ManualBreathingPlayer>();
        modPlayer.autoBreathe = true;
        modPlayer.RefillBreath();
        player.AddBuff(BuffID.Slow, 2);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddRecipeGroup(RecipeGroupID.IronBar, 10)
            .AddIngredient(ItemID.Bottle, 1)
            .AddIngredient(ItemID.Gel, 20)
            .AddIngredient(ItemID.Glass, 20)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
