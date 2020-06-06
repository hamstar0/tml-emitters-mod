using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using HamstarHelpers.Helpers.Players;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.NetProtocols {
	class SoundEmitterPlacementProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( SoundEmitterDefinition def, ushort tileX, ushort tileY ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModHelpersException("Not client."); }

			var protocol = new SoundEmitterPlacementProtocol( Main.myPlayer, def, tileX, tileY );

			protocol.SendToServer( true );
		}



		////////////////

		public int FromWho;

		public ushort TileX;
		public ushort TileY;

		public int Type;
		public int Style;
		public float Volume;
		public float Pitch;
		public int Delay;
		public bool IsActivated;



		////////////////

		private SoundEmitterPlacementProtocol() { }

		private SoundEmitterPlacementProtocol( int fromWho, SoundEmitterDefinition def, ushort tileX, ushort tileY ) {
			def.Output(
				out this.Type,
				out this.Style,
				out this.Volume,
				out this.Pitch,
				out this.Delay,
				out this.IsActivated
			);

			this.FromWho = fromWho;
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		private SoundEmitterDefinition GetNewEmitter() => new SoundEmitterDefinition(
			type: this.Type,
			style: this.Style,
			volume: this.Volume,
			pitch: this.Pitch,
			delay: this.Delay,
			isActivated: this.IsActivated
		);


		////////////////

		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			Main.PlaySound( SoundID.Item108, new Vector2(this.TileX<<4, this.TileY<<4) );

			myworld.AddSoundEmitter( this.GetNewEmitter(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<SoundEmitterItem>(), 1 );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			myworld.AddSoundEmitter( this.GetNewEmitter(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<SoundEmitterItem>(), 1 );
		}
	}
}
