using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.Config;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class HologramItem : ModItem {
		public HologramDefinition Def { get; private set; } = null;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override ModItem Clone( Item item ) {
			var myclone = (HologramItem)base.Clone( item );
			myclone.Def = this.Def;

			return myclone;
		}

		public override ModItem Clone() {
			var myclone = (HologramItem)base.Clone();
			myclone.Def = this.Def;

			return myclone;
		}


		////////////////

		public override void SetStaticDefaults() {
			this.DisplayName.SetDefault( "Hologram" );
			this.Tooltip.SetDefault( "Casts an image of something into the world."
				+ "\n" + "Place on a tile to apply effect"
				+ "\n" + "Holograms may be wire controlled"
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
			//this.item.createTile = ModContent.TileType<HologramTile>();
		}


		////////////////

		public override void Load( TagCompound tag ) {
			try
			{
				this.Def = new HologramDefinition(
					mode: (int)tag.GetInt("HologramMode"),
					type: (int)GetHologramType(tag),
					scale: tag.GetFloat( "HologramScale" ),
					color: new Color(
						tag.GetByte( "HologramColorR" ),
						tag.GetByte( "HologramColorG" ),
						tag.GetByte( "HologramColorB" )
					),
					alpha: tag.GetByte( "HologramAlpha" ),
					direction: tag.GetInt( "HologramDirection" ),
					rotation: tag.GetFloat( "HologramRotation" ),
					offsetX: tag.GetInt( "HologramOffsetX" ),
					offsetY: tag.GetInt( "HologramOffsetY" ),
					frameStart: tag.GetInt( "HologramFrameStart" ),
					frameEnd: tag.GetInt( "HologramFrameEnd" ),
					frameRateTicks: tag.GetInt( "HologramFrameRateTicks" ),
					worldLight: tag.GetBool( "HologramWorldLighting" ),
					crtEffect:tag.GetBool("HologramCRTEFfect"),
					isActivated: tag.GetBool( "HologramIsActivated" )
				);
			} catch { }
		}

		public int GetHologramType(TagCompound tag)
		{
			switch((int)tag.GetInt("HologramMode"))
			{
				case 1:
				return NPCDefinition.FromString(tag.GetString("HologramType")).Type;
				case 2:
				return ItemDefinition.FromString(tag.GetString("HologramType")).Type;
				case 3:
				return ProjectileDefinition.FromString(tag.GetString("HologramType")).Type;
			}
			return 1;
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}
			return new TagCompound {
				{ "HologramMode", (int)this.Def.Mode },
				{ "HologramType", (string)this.Def.SetHologramType().ToString()},
				{ "HologramScale", (float)this.Def.Scale },
				{ "HologramColorR", (byte)this.Def.Color.R },
				{ "HologramColorG", (byte)this.Def.Color.G },
				{ "HologramColorB", (byte)this.Def.Color.B },
				{ "HologramAlpha", (byte)this.Def.Alpha },
				{ "HologramDirection", (int)this.Def.Direction },
				{ "HologramRotation", (float)this.Def.Rotation },
				{ "HologramOffsetX", (int)this.Def.OffsetX },
				{ "HologramOffsetY", (int)this.Def.OffsetY },
				{ "HologramFrameStart", (int)this.Def.FrameStart },
				{ "HologramFrameEnd", (int)this.Def.FrameEnd },
				{ "HologramFrameRateTicks", (int)this.Def.FrameRateTicks },
				{ "HologramWorldLighting", (bool)this.Def.WorldLighting },
				{ "HologramCRTEffect", (bool)this.Def.CrtEffect },
				{ "HologramIsActivated", (bool)this.Def.IsActivated },
			};
		}


		////////////////

		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Insert( 1, new TooltipLine( this.mod, "HologramUI", "[c/00FF00:Right-click in inventory to adjust settings]" ) );
			tooltips.Insert( 2, new TooltipLine( this.mod, "HologramToggle", "[c/00FF00:Left-click in world to toggle activation]" ) );
			tooltips.Insert( 3, new TooltipLine( this.mod, "HologramRemove", "[c/00FF00:Right-click in world to remove]" ) );

			if( this.Def == null ) {
				return;
			}

			var modeTip = new TooltipLine(this.mod, "HologramMode", " Mode: " + this.Def?.RenderMode());
			var typeTip = new TooltipLine( this.mod, "HologramType", " Type: " + this.Def?.RenderType() );
			var scaleTip = new TooltipLine( this.mod, "HologramScale", " Scale: " + this.Def?.RenderScale() );
			var colorTip = new TooltipLine( this.mod, "HologramColor", " Color: " + this.Def?.RenderColor() );
			var alphaTip = new TooltipLine( this.mod, "HologramAlpha", " Alpha: " + this.Def?.RenderAlpha() );
			var directionTip = new TooltipLine( this.mod, "HologramDirection", " Direction: " + this.Def?.RenderDirection() );
			var rotationTip = new TooltipLine( this.mod, "HologramRotation", " Rotation: " + this.Def?.RenderRotation() );
			var offsetTip = new TooltipLine( this.mod, "HologramOffset", " Offset: " + this.Def?.RenderOffset() );
			var frameTip = new TooltipLine( this.mod, "HologramFrame", " Frame: " + this.Def?.RenderFrame() );
			var worldLightingTip = new TooltipLine( this.mod, "HologramWorldLighting", " World Lighting: " + this.Def?.WorldLighting );
			var CRTEffectTip = new TooltipLine(this.mod, "HologramCRTEffect", " CRT Effect: " + this.Def?.CrtEffect);

			var color = Color.White * 0.75f;
			modeTip.overrideColor = color;
			typeTip.overrideColor = color;
			scaleTip.overrideColor = color;
			colorTip.overrideColor = color;
			alphaTip.overrideColor = color;
			directionTip.overrideColor = color;
			rotationTip.overrideColor = color;
			offsetTip.overrideColor = color;
			frameTip.overrideColor = color;
			worldLightingTip.overrideColor = color;
			CRTEffectTip.overrideColor = color;

			tooltips.Add(modeTip);
			tooltips.Add(typeTip);
			tooltips.Add( scaleTip );
			tooltips.Add( colorTip );
			tooltips.Add( alphaTip );
			tooltips.Add( directionTip );
			tooltips.Add( rotationTip );
			tooltips.Add( offsetTip );
			tooltips.Add( frameTip );
			tooltips.Add( worldLightingTip );
			tooltips.Add(CRTEffectTip);
		}


		////////////////

		public void SetHologramDefinition( HologramDefinition def ) {
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
			if( HologramItem.CanViewHolograms( Main.LocalPlayer ) ) {
				this.UpdateInterface();
			}
		}

		////

		private void UpdateInterface() {
			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				//	this.AttemptHologramToggle( Main.MouseWorld );
			} else if( Main.mouseRight && Main.mouseRightRelease ) {
				HologramItem.AttemptHologramPickup( Main.MouseWorld );
			}
		}
	}
}