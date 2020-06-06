using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem {
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
	}
}