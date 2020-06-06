using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem {
		public override void ModifyTooltips( List<TooltipLine> tooltips ) {
			tooltips.Insert( 1, new TooltipLine( this.mod, "SndEmitterUI", "[c/00FF00:Right-click in inventory to adjust settings]" ) );
			tooltips.Insert( 2, new TooltipLine( this.mod, "SndEmitterToggle", "[c/00FF00:Left-click in world to toggle activation]" ) );
			tooltips.Insert( 3, new TooltipLine( this.mod, "SndEmitterRemove", "[c/00FF00:Right-click in world to remove]" ) );

			if( this.Def == null ) {
				return;
			}

			var typeTip = new TooltipLine( this.mod, "SndEmitterType", " Type: " + this.Def.RenderType() );
			var volumeTip = new TooltipLine( this.mod, "SndEmitterVolume", " Volume: " + this.Def.RenderVolume() );
			var delayTip = new TooltipLine( this.mod, "SndEmitterDelay", " Delay: " + this.Def.RenderDelay() );

			var color = Color.White * 0.75f;
			typeTip.overrideColor = color;
			volumeTip.overrideColor = color;
			delayTip.overrideColor = color;

			tooltips.Add( typeTip );
			tooltips.Add( volumeTip );
			tooltips.Add( delayTip );
		}
	}
}