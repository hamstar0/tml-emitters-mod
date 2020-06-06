using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace Emitters.Items {
	public partial class HologramItem : ModItem {
		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Insert( 1, new TooltipLine( this.mod, "HologramUI", "[c/00FF00:Right-click in inventory to adjust settings]" ) );
			tooltips.Insert( 2, new TooltipLine( this.mod, "HologramToggle", "[c/00FF00:Left-click in world to toggle activation]" ) );
			tooltips.Insert( 3, new TooltipLine( this.mod, "HologramRemove", "[c/00FF00:Right-click in world to remove]" ) );

			if( this.Def == null ) {
				return;
			}

			var modeTip = new TooltipLine( this.mod, "HologramMode", " Mode: " + this.Def?.RenderMode() );
			var typeTip = new TooltipLine( this.mod, "HologramType", " Type: " + this.Def?.RenderType() );
			var scaleTip = new TooltipLine( this.mod, "HologramScale", " Scale: " + this.Def?.RenderScale() );
			var colorTip = new TooltipLine( this.mod, "HologramColor", " Color: " + this.Def?.RenderColor() );
			var alphaTip = new TooltipLine( this.mod, "HologramAlpha", " Alpha: " + this.Def?.RenderAlpha() );
			var directionTip = new TooltipLine( this.mod, "HologramDirection", " Direction: " + this.Def?.RenderDirection() );
			var rotationTip = new TooltipLine( this.mod, "HologramRotation", " Rotation: " + this.Def?.RenderRotation() );
			var offsetTip = new TooltipLine( this.mod, "HologramOffset", " Offset: " + this.Def?.RenderOffset() );
			var frameTip = new TooltipLine( this.mod, "HologramFrame", " Frame: " + this.Def?.RenderFrame() );
			var worldLightingTip = new TooltipLine( this.mod, "HologramWorldLighting", " World Lighting: " + this.Def?.WorldLighting );
			var shaderModeTip = new TooltipLine( this.mod, "HologramCRTEffect", " Shader Mode: " + this.Def?.RenderShaderMode() );
			var shaderTimeTip = new TooltipLine( this.mod, "HologramShaderTime", " Shader Time: " + this.Def?.RenderShaderTime() );
			var shaderTypeTip = new TooltipLine( this.mod, "HologramShaderType", " Shader Type: " + this.Def?.RenderShaderType() );

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
			shaderModeTip.overrideColor = color;
			shaderTimeTip.overrideColor = color;
			shaderTypeTip.overrideColor = color;

			tooltips.Add( modeTip );
			tooltips.Add( typeTip );
			tooltips.Add( scaleTip );
			tooltips.Add( colorTip );
			tooltips.Add( alphaTip );
			tooltips.Add( directionTip );
			tooltips.Add( rotationTip );
			tooltips.Add( offsetTip );
			tooltips.Add( frameTip );
			tooltips.Add( worldLightingTip );
			tooltips.Add( shaderModeTip );
			tooltips.Add( shaderTimeTip );
			tooltips.Add( shaderTypeTip );
		}
	}
}