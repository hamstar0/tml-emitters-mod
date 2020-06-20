using Terraria.ID;
using Terraria.ModLoader;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem, IBaseEmitterItem<EmitterDefinition> {
		public override void AddRecipes() {
			if( !EmittersConfig.Instance.EmitterRecipeEnabled ) {
				return;
			}

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