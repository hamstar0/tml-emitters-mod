using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;


namespace Emitters.NetProtocols {
	class HologramRemoveProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( ushort tileX, ushort tileY ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModHelpersException( "Not client." ); }

			var protocol = new HologramRemoveProtocol( tileX, tileY );

			protocol.SendToServer(true);
		}

		public static void BroadcastFromServer( ushort tileX, ushort tileY ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModHelpersException( "Not server." ); }

			var protocol = new HologramRemoveProtocol(tileX, tileY);

			protocol.SendToClient(-1, -1);
		}



		////////////////

		public ushort TileX;
		public ushort TileY;

		/*internal static void DespawnHologram( int npc ) {	// <- ?
			Main.npc[npc] = new NPC {
				whoAmI = npc
			};

			if( Main.netMode == NetmodeID.Server ) {
				NetMessage.SendData( MessageID.SyncNPC, -1, -1, null, npc, 0f, 0f, 0f, 0, 0, 0 );
			}
		}*/

		////////////////

		private HologramRemoveProtocol() { }

		private HologramRemoveProtocol( ushort tileX, ushort tileY ) {
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			Main.PlaySound(SoundID.Item108, new Vector2(this.TileX << 4, this.TileY << 4));

			myworld.RemoveHologram(this.TileX, this.TileY);
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			myworld.RemoveHologram(this.TileX, this.TileY);
		}
	}
}

