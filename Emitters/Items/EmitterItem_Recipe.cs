using Terraria.ID;
using Terraria.ModLoader;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem {
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.FireworkFountain, 1 );
			recipe.AddIngredient( ItemID.Wire, 1 );
			recipe.AddIngredient( ItemID.Cloud, 1 );
			recipe.AddIngredient( ItemID.Flare, 1 );
			//recipe.AddIngredient( ItemID.ExplosivePowder, 1 );
			//recipe.AddIngredient( ItemID.Nanites, 1 );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}
	}
}