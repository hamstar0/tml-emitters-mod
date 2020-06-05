using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using Emitters.Definitions;


namespace Emitters.NetProtocols {
	class HologramActivateProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( bool isActivated, ushort tileX, ushort tileY ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModHelpersException("Not client."); }

			var protocol = new HologramActivateProtocol {
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

		private HologramActivateProtocol() { }


		////////////////
		
		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			HologramDefinition def = myworld.GetHologram( this.TileX, this.TileY );

			def.Activate( this.IsActivated );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			HologramDefinition def = myworld.GetHologram( this.TileX, this.TileY );

			def.Activate( this.IsActivated );
		}
	}
}
