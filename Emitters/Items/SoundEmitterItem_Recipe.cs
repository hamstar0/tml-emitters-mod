using Terraria.ID;
using Terraria.ModLoader;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem {
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.FireworkFountain, 1 );
			recipe.AddIngredient( ItemID.Wire, 1 );
			recipe.AddIngredient( ItemID.DartTrap, 1 );
			recipe.AddIngredient( ItemID.Duck, 1 );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}