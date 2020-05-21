using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Emitters.Definitions;


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

			try {
				this.Def = new EmitterDefinition(
					isGoreMode: tag.GetBool( "EmitterMode" ),
					type: tag.GetInt( "EmitterType" ),
					scale: tag.GetFloat( "EmitterScale" ),
					delay: tag.GetInt( "EmitterDelay" ),
					speedX: tag.GetFloat( "EmitterSpeedX" ),
					speedY: tag.GetFloat( "EmitterSpeedY" ),
					color: new Color(
						tag.GetByte( "EmitterColorR" ),
						tag.GetByte( "EmitterColorG" ),
						tag.GetByte( "EmitterColorB" )
					),
					alpha: tag.GetByte( "EmitterAlpha" ),
					scatter: tag.GetFloat( "EmitterScatter" ),
					hasGravity: tag.GetBool( "EmitterHasGrav" ),
					hasLight: tag.GetBool( "EmitterHasLight" ),
					isActivated: tag.GetBool( "EmitterIsActivated" )
				);
			} catch { }
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}

			return new TagCompound {
				{ "EmitterMode", (bool)this.Def.IsGoreMode },
				{ "EmitterType", (int)this.Def.Type },
				{ "EmitterScale", (float)this.Def.Scale },
				{ "EmitterDelay", (int)this.Def.Delay },
				{ "EmitterSpeedX", (float)this.Def.SpeedX },
				{ "EmitterSpeedY", (float)this.Def.SpeedY },
				{ "EmitterColorR", (byte)this.Def.Color.R },
				{ "EmitterColorG", (byte)this.Def.Color.G },
				{ "EmitterColorB", (byte)this.Def.Color.B },
				{ "EmitterAlpha", (byte)this.Def.Alpha },
				{ "EmitterScatter", (float)this.Def.Scatter },
				{ "EmitterHasGrav", (bool)this.Def.HasGravity },
				{ "EmitterHasLight", (bool)this.Def.HasLight },
				{ "EmitterIsActivated", (bool)this.Def.IsActivated },
			};
		}


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Insert( 1, new TooltipLine(this.mod, "EmitterUI", "[c/00FF00:Right-click in inventory to adjust settings]") );
			tooltips.Insert( 2, new TooltipLine(this.mod, "EmitterToggle", "[c/00FF00:Left-click in world to toggle activation]") );
			tooltips.Insert( 3, new TooltipLine(this.mod, "EmitterRemove", "[c/00FF00:Right-click in world to remove]") );

			if( this.Def == null ) {
				return;
			}

			var modeTip = new TooltipLine( this.mod, "EmitterMode", " Mode: "+this.Def?.RenderMode() );
			var typeTip = new TooltipLine( this.mod, "EmitterType", " Type: "+this.Def?.RenderType() );
			var scaleTip = new TooltipLine( this.mod, "EmitterScale", " Scale: "+this.Def?.RenderScale() );
			var delayTip = new TooltipLine( this.mod, "EmitterDelay", " Delay: "+this.Def?.RenderDelay() );
			var speedxTip = new TooltipLine( this.mod, "EmitterSpeedX", " SpeedX: "+this.Def?.RenderSpeedX() );
			var speedyTip = new TooltipLine( this.mod, "EmitterSpeedY", " SpeedY: "+this.Def?.RenderSpeedY() );
			var colorTip = new TooltipLine( this.mod, "EmitterColor", " Color: "+this.Def?.RenderColor() );
			var alphaTip = new TooltipLine( this.mod, "EmitterAlpha", " Alpha: "+this.Def?.RenderAlpha() );
			var scatterTip = new TooltipLine( this.mod, "EmitterScatter", " Scatter: "+this.Def?.RenderScatter() );
			var hasGravTip = new TooltipLine( this.mod, "EmitterHasGrav", " Has Gravity: "+this.Def?.RenderHasGravity() );
			var hasLightTip = new TooltipLine( this.mod, "EmitterHasLight", " Has Light: "+this.Def?.RenderHasLight() );

			var color = Color.White * 0.75f;
			modeTip.overrideColor = color;
			typeTip.overrideColor = color;
			scaleTip.overrideColor = color;
			delayTip.overrideColor = color;
			speedxTip.overrideColor = color;
			speedyTip.overrideColor = color;
			colorTip.overrideColor = color;
			alphaTip.overrideColor = color;
			scatterTip.overrideColor = color;
			hasGravTip.overrideColor = color;
			hasLightTip.overrideColor = color;

			tooltips.Add( modeTip );
			tooltips.Add( typeTip );
			tooltips.Add( scaleTip );
			tooltips.Add( delayTip );
			tooltips.Add( speedxTip );
			tooltips.Add( speedyTip );
			tooltips.Add( colorTip );
			tooltips.Add( alphaTip );
			tooltips.Add( scatterTip );
			tooltips.Add( hasGravTip );
			tooltips.Add( hasLightTip );
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
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
			//	this.AttemptEmitterToggle( Main.MouseWorld );
			} else if( Main.mouseRight && Main.mouseRightRelease ) {
				EmitterItem.AttemptEmitterPickup( Main.MouseWorld );
			}
		}
	}
}