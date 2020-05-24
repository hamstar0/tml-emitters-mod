using System;
using Emitters.Definitions;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;


namespace Emitters {
	public partial class EmittersMod : Mod {
		private void HookWireHit( ILContext il ) {
			var c = new ILCursor( il );

			// Push tile x and y onto stack
			c.Emit( OpCodes.Ldarg_0 );
			c.Emit( OpCodes.Ldarg_1 );

			// Intercept wire hits
			c.EmitDelegate<Action<int, int>>( ( x, y ) => {
				this.WireHit( x, y );
			} );
		}


		////////////////

		public void WireHit( int i, int j ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var x = (ushort)i;
			var y = (ushort)j;

			EmitterDefinition edef = myworld.GetEmitter( x, y );
			SoundEmitterDefinition sdef = myworld.GetSoundEmitter( x, y );
			HologramDefinition hdef = myworld.GetHologram( x, y );

			if( edef != null ) {
				if( EmittersConfig.Instance.DebugModeInfo ) {
					Main.NewText( "Toggling emitter at "+x+", "+y+" "+(edef.IsActivated?"off":"on") );
				}
				edef.Activate( !edef.IsActivated );
			}
			if( sdef != null ) {
				if( EmittersConfig.Instance.DebugModeInfo ) {
					Main.NewText( "Toggling sound emitter at "+x+", "+y+" "+(sdef.IsActivated?"off":"on") );
				}
				sdef.Activate( !sdef.IsActivated );
			}
			if( hdef != null ) {
				if( EmittersConfig.Instance.DebugModeInfo ) {
					Main.NewText( "Toggling hologram at "+x+", "+y+" "+(hdef.IsActivated?"off":"on") );
				}
				hdef.Activate( !hdef.IsActivated );
			}
		}
	}
}