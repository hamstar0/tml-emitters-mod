using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem, IBaseEmitterItem<SoundEmitterDefinition> {
		public SoundEmitterDefinition Def { get; private set; } = null;
		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override ModItem Clone( Item item ) {
			var myclone = (SoundEmitterItem)base.Clone( item );
			myclone.Def = this.Def;

			return myclone;
		}

		public override ModItem Clone() {
			var myclone = (SoundEmitterItem)base.Clone();
			myclone.Def = this.Def;

			return myclone;
		}


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Sound Emitter" );
			this.Tooltip.SetDefault( "Emits noises."
				+ "\n" + "Place on a tile to apply effect"
				+ "\n" + "Emitters may be wire controlled"
			);
		}

		public override void SetDefaults() {
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 999;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = ItemUseStyleID.SwingThrow;
			this.item.consumable = true;
			//this.item.createTile = ModContent.TileType<EmitterTile>();
		}


		////////////////

		public void SetDefinition( SoundEmitterDefinition def ) {
			//Main.NewText( def.ToString() );
			this.Def = def;
		}


		////////////////

		public override void UpdateInventory( Player player ) {
			if( player.whoAmI == Main.myPlayer ) {
				this.UpdateForCurrentPlayer();
			}
		}

		private void UpdateForCurrentPlayer() {
			if( SoundEmitterItem.CanViewSoundEmitters(Main.LocalPlayer, false) ) {
				this.UpdateInterface();
			}
		}

		////

		private void UpdateInterface() {
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				//	this.AttemptEmitterToggle( Main.MouseWorld );
			} else if( Main.mouseRight && Main.mouseRightRelease ) {
				SoundEmitterItem.AttemptSoundEmitterPickup( Main.MouseWorld );
			}
		}
	}
}