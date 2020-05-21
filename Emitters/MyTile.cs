using System;
using Terraria.ModLoader;
using Emitters.Definitions;
using Terraria;

namespace Emitters {
	class EmittersTile : GlobalTile {
		public override void HitWire( int i, int j, int type ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			SoundEmitterDefinition sdef = myworld.GetSoundEmitter( (ushort)i, (ushort)j );
			EmitterDefinition def = myworld.GetEmitter( (ushort)i, (ushort)j );
			HologramDefinition hdef = myworld.GetHologram((ushort)i, (ushort)j);
			if ( def == null || sdef == null ) {
				return;
			}

			def.Activate( !def.IsActivated );
			sdef.Activate( !sdef.IsActivated );
			hdef.Activate( !hdef.IsActivated );
		}
		
	}

}
