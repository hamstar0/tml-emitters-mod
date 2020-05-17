using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Emitters.Definitions;


namespace Emitters {
	public partial class EmittersWorld : ModWorld {
		public void AddEmitter( EmitterDefinition def, ushort tileX, ushort tileY ) {
			if( ( tileX < 0 || tileX >= Main.maxTilesX ) || ( tileY < 0 || tileY >= Main.maxTilesY ) ) {
				throw new ModHelpersException( "Cannot place emitter outside of world." );
			}
			//Main.NewText( def.ToString() );
			this.Emitters.Set2D( tileX, tileY, def );
		}

		public EmitterDefinition GetEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Get2DOrDefault( tileX, tileY );
		}

		public bool RemoveEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Remove2D( tileX, tileY );
		}


		////////////////

		public void AddSoundEmitter( SoundEmitterDefinition def, ushort tileX, ushort tileY ) {
			if( ( tileX < 0 || tileX >= Main.maxTilesX ) || ( tileY < 0 || tileY >= Main.maxTilesY ) ) {
				throw new ModHelpersException( "Cannot place emitter outside of world." );
			}
			//Main.NewText( def.ToString() );
			this.SoundEmitters.Set2D( tileX, tileY, def );
		}

		public SoundEmitterDefinition GetSoundEmitter( ushort tileX, ushort tileY ) {
			return this.SoundEmitters.Get2DOrDefault( tileX, tileY );
		}
		public bool RemoveSoundEmitter( ushort tileX, ushort tileY ) {
			return this.SoundEmitters.Remove2D( tileX, tileY );
		}
	}
}