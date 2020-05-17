using System;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using Emitters.Definitions;


namespace Emitters.NetProtocols {
	class SoundEmitterActivateProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( bool isActivated, ushort tileX, ushort tileY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client."); }

			var protocol = new SoundEmitterActivateProtocol {
				TileX = tileX,
				TileY = tileY,
				IsActivated = isActivated,
			};

			protocol.SendToServer( true );
		}



		////////////////

		public ushort TileX;
		public ushort TileY;
		public bool IsActivated;



		////////////////

		private SoundEmitterActivateProtocol() { }


		////////////////
		
		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			SoundEmitterDefinition def = myworld.GetSoundEmitter( this.TileX, this.TileY );

			def.Activate( this.IsActivated );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			SoundEmitterDefinition def = myworld.GetSoundEmitter( this.TileX, this.TileY );

			def.Activate( this.IsActivated );
		}
	}
}
