using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem, IBaseEmitterItem {
		public EmitterDefinition Def { get; private set; } = null;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override ModItem Clone( Item item ) {
			var myclone = (EmitterItem)base.Clone( item );
			myclone.Def = this.Def;

			return myclone;
		}

		public override ModItem Clone() {
			var myclone = (EmitterItem)base.Clone();
			myclone.Def = this.Def;

			return myclone;
		}


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Emitter" );
			this.Tooltip.SetDefault( "Spews particles."
				+"\n"+"Place on a tile to apply effect"
				+"\n"+"Emitters may be wire controlled"
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

		public void SetEmitterDefinition( EmitterDefinition def ) {
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
			if( EmitterItem.CanViewEmitters( Main.LocalPlayer, false ) ) {
				this.UpdateInterface();
			}
		}

		////

		private void UpdateInterface() {
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
			//	this.AttemptEmitterToggle( Main.MouseWorld );
			} else if( Main.mouseRight && Main.mouseRightRelease ) {
				EmitterItem.AttemptEmitterPickup( Main.MouseWorld );
			}
		}


		////////////////

		public override bool PreDrawInInventory(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			var mymod = EmittersMod.Instance;
			var newPos = new Vector2( pos.X - 4f, pos.Y - 16f );

			if( mymod.CanPressEditorButton(newPos) ) {
				mymod.ReadyEditorButtonPress( () => EmitterItem.OpenUI(this.item) );
			}

			return base.PreDrawInInventory( sb, pos, frame, drawColor, itemColor, origin, scale );
		}


		////////////////

		public override void PostDrawInInventory(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			var mymod = EmittersMod.Instance;
			var newPos = new Vector2( pos.X - 4f, pos.Y - 16f );
			var mouseRect = new Rectangle(
				x: (int)newPos.X,
				y: (int)newPos.Y,
				width: mymod.EditorButtonWidth,
				height: mymod.EditorButtonHeight
			);

			if( mouseRect.Contains(Main.mouseX, Main.mouseY) ) {
				mymod.DrawEditorButton( sb, newPos );
			}
		}
	}
}