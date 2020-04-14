using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem {
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
			this.item.useStyle = 1;
			this.item.consumable = true;
			//this.item.createTile = ModContent.TileType<EmitterTile>();
		}


		////////////////

		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("EmitterMode") ) {
				return;
			}

			this.Def = new EmitterDefinition {
				IsGoreMode = tag.GetBool( "EmitterMode"),
				Type = tag.GetInt( "EmitterType" ),
				Scale = tag.GetFloat( "EmitterScale" ),
				SpeedX = tag.GetFloat( "EmitterSpeedX" ),
				SpeedY = tag.GetFloat( "EmitterSpeedY" ),
				Color = new Color(
					tag.GetByte( "EmitterColorR" ),
					tag.GetByte( "EmitterColorG" ),
					tag.GetByte( "EmitterColorB" )
				),
				Alpha = tag.GetFloat( "EmitterAlpha" ),
				Scatter = tag.GetFloat( "EmitterScatter" ),
				HasGravity = tag.GetBool( "EmitterHasGrav" ),
				HasLight = tag.GetBool( "EmitterHasLight" ),
			};
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}

			return new TagCompound {
				{ "EmitterMode", (bool)this.Def.IsGoreMode },
				{ "EmitterType", (int)this.Def.Type },
				{ "EmitterScale", (float)this.Def.Scale },
				{ "EmitterSpeedX", (float)this.Def.SpeedX },
				{ "EmitterSpeedY", (float)this.Def.SpeedY },
				{ "EmitterColorR", (byte)this.Def.Color.R },
				{ "EmitterColorG", (byte)this.Def.Color.G },
				{ "EmitterColorB", (byte)this.Def.Color.B },
				{ "EmitterAlpha", (float)this.Def.Alpha },
				{ "EmitterScatter", (float)this.Def.Scatter },
				{ "EmitterHasGrav", (bool)this.Def.HasGravity },
				{ "EmitterHasLight", (bool)this.Def.HasLight },
			};
		}


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Insert( 0, new TooltipLine(this.mod, "EmitterUI", "[c/00FF00:Right-click item to make adjustments]") );

			if( this.Def == null ) {
				return;
			}

			var modeTip = new TooltipLine( this.mod, "EmitterMode", " Mode: "+this.Def?.RenderMode() );
			var typeTip = new TooltipLine( this.mod, "EmitterType", " Type: "+this.Def?.RenderType() );
			var scaleTip = new TooltipLine( this.mod, "EmitterScale", " Scale: "+this.Def?.RenderScale() );
			var speedxTip = new TooltipLine( this.mod, "EmitterSpeedX", " SpeedX: "+this.Def?.RenderSpeedX() );
			var speedyTip = new TooltipLine( this.mod, "EmitterSpeedY", " SpeedY: "+this.Def?.RenderSpeedY() );
			var colorTip = new TooltipLine( this.mod, "EmitterColor", " Color: "+this.Def?.RenderColor() );
			var alphaTip = new TooltipLine( this.mod, "EmitterAlpha", " Alpha%: "+this.Def?.RenderAlpha() );
			var scatterTip = new TooltipLine( this.mod, "EmitterScatter", " Scatter%: "+this.Def?.RenderScatter() );
			var hasGravTip = new TooltipLine( this.mod, "EmitterHasGrav", " Has Gravity: "+this.Def?.RenderHasGravity() );
			var hasLightTip = new TooltipLine( this.mod, "EmitterHasLight", " Has Light: "+this.Def?.RenderHasLight() );

			modeTip.overrideColor = Color.Gray;
			typeTip.overrideColor = Color.Gray;
			scaleTip.overrideColor = Color.Gray;
			speedxTip.overrideColor = Color.Gray;
			speedyTip.overrideColor = Color.Gray;
			colorTip.overrideColor = Color.Gray;
			alphaTip.overrideColor = Color.Gray;
			scatterTip.overrideColor = Color.Gray;
			hasGravTip.overrideColor = Color.Gray;
			hasLightTip.overrideColor = Color.Gray;

			tooltips.Add( modeTip );
			tooltips.Add( typeTip );
			tooltips.Add( scaleTip );
			tooltips.Add( speedxTip );
			tooltips.Add( speedyTip );
			tooltips.Add( colorTip );
			tooltips.Add( alphaTip );
			tooltips.Add( scatterTip );
			tooltips.Add( hasGravTip );
			tooltips.Add( hasLightTip );
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
			if( EmitterItem.CanViewEmitters( Main.LocalPlayer ) ) {
				this.UpdateInterface();
			}
		}

		////

		private void UpdateInterface() {
			if( Main.mouseRight && Main.mouseRightRelease ) {
				this.AttemptEmitterPickup( Main.MouseWorld );
				return;
			}
		}
	}
}