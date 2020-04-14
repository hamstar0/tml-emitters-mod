using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;


namespace Emitters.NetProtocols {
	class EmitterPlacementProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( EmitterDefinition def, ushort tileX, ushort tileY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client."); }

			var protocol = new EmitterPlacementProtocol();

			protocol.Def = def;
			protocol.TileX = tileX;
			protocol.TileY = tileY;

			protocol.SendToServer( true );
		}



		////////////////

		public EmitterDefinition Def;
		public ushort TileX;
		public ushort TileY;



		////////////////

		private EmitterPlacementProtocol() { }


		////////////////
		
		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			Main.PlaySound( SoundID.Item108, new Vector2(this.TileX<<4, this.TileY<<4) );

			myworld.AddEmitter( this.Def, this.TileX, this.TileY );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			myworld.AddEmitter( this.Def, this.TileX, this.TileY );
		}
	}
}
