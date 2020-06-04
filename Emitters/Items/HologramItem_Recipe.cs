using Terraria.ID;
using Terraria.ModLoader;

namespace Emitters.Items
{
	public partial class HologramItem : ModItem 
	{
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(this.mod);
			recipe.AddIngredient(ItemID.LogicSensor_Above, 1);
			recipe.AddIngredient(ItemID.WireBulb, 1);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 10);
			recipe.AddRecipe();
		}
	}
}
