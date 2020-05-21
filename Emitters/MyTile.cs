using System;
using Terraria.ModLoader;
using Emitters.Definitions;


namespace Emitters {
	class EmittersTile : GlobalTile {
		public override void HitWire( int i, int j, int type ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var x = (ushort)i;
			var y = (ushort)j;

			EmitterDefinition def = myworld.GetEmitter( x, y );
			SoundEmitterDefinition sdef = myworld.GetSoundEmitter( x, y );
			HologramDefinition hdef = myworld.GetHologram( x, y );

			if( def != null ) {
				def.Activate( !def.IsActivated );
			}
			if( sdef != null ) {
				sdef.Activate( !sdef.IsActivated );
			}
			if( hdef != null ) {
				hdef.Activate( !hdef.IsActivated );
			}
		}
	}
}
