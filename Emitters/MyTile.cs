using System;
using Terraria.ModLoader;


namespace Emitters {
	class EmittersTile : GlobalTile {
		public override void HitWire( int i, int j, int type ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			EmitterDefinition def = myworld.GetEmitter( (ushort)i, (ushort)j );
			if( def == null ) {
				return;
			}

			def.Activate( !def.IsActivated );
		}
	}
}
