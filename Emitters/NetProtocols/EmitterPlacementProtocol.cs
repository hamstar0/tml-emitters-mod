using System;
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
	class EmitterPlacementProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( EmitterDefinition def, ushort tileX, ushort tileY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client."); }

			var protocol = new EmitterPlacementProtocol( Main.myPlayer, def, tileX, tileY );

			protocol.SendToServer( true );
		}



		////////////////

		public int FromWho;

		public ushort TileX;
		public ushort TileY;

		public bool IsGoreMode;
		public int Type;
		public float Scale;
		public int Delay;
		public float SpeedX;
		public float SpeedY;
		public byte ColorR;
		public byte ColorG;
		public byte ColorB;
		public byte Alpha;
		public float Scatter;
		public bool HasGravity;
		public bool HasLight;
		public bool IsActivated;



		////////////////

		private EmitterPlacementProtocol() { }

		private EmitterPlacementProtocol( int fromWho, EmitterDefinition def, ushort tileX, ushort tileY ) {
			def.Output(
				out this.IsGoreMode,
				out this.Type,
				out this.Scale,
				out this.Delay,
				out this.SpeedX,
				out this.SpeedY,
				out this.ColorR,
				out this.ColorG,
				out this.ColorB,
				out this.Alpha,
				out this.Scatter,
				out this.HasGravity,
				out this.HasLight,
				out this.IsActivated
			);

			this.FromWho = fromWho;
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		private EmitterDefinition GetNewEmitter() => new EmitterDefinition(
			isGoreMode: this.IsGoreMode,
			type: this.Type,
			scale: this.Scale,
			delay: this.Delay,
			speedX: this.SpeedX,
			speedY: this.SpeedY,
			color: new Color( this.ColorR, this.ColorG, this.ColorB ),
			alpha: this.Alpha,
			scatter: this.Scatter,
			hasGravity: this.HasGravity,
			hasLight: this.HasLight,
			isActivated: this.IsActivated
		);


		////////////////

		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			Main.PlaySound( SoundID.Item108, new Vector2(this.TileX<<4, this.TileY<<4) );

			myworld.AddEmitter( this.GetNewEmitter(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<EmitterItem>(), 1 );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			myworld.AddEmitter( this.GetNewEmitter(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<EmitterItem>(), 1 );
		}
	}
}
