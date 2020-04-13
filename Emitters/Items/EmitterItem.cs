using HamstarHelpers.Services.Timers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emitters.Items {
	public class EmitterItem : ModItem {
		public static void OpenUI( Item emitterItem ) {
			EmittersMod.Instance.EmitterEditorDialog.Open();
		}



		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Emitter" );
			this.Tooltip.SetDefault( "Spews particles.\nRight-click item for adjustments" );
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 999;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = 1;
			this.item.consumable = true;
			//this.item.createTile = ModContent.TileType<EmitterTile>();
		}

		////////////////

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe( this.mod );
			recipe.AddIngredient( ItemID.FireworkFountain, 1 );
			recipe.AddTile( TileID.WorkBenches );
			recipe.SetResult( this );
			recipe.AddRecipe();
		}


		////////////////

		public override bool CanRightClick() {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			EmitterItem.OpenUI( this.item );

			return false;
		}


		////////////////

		public override bool UseItem( Player player ) {
			string timerName = "EmitterPlace_" + player.whoAmI;
			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return base.UseItem( player );
			}
			Timers.SetTimer( timerName, 4, false, () => false );

			this.AttemptPlacement( player );

			return base.UseItem( player );
		}


		////////////////

		private void AttemptPlacement( Player plr ) {

		}
	}
}