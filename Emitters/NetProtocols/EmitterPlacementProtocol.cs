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

			def.Output(
				out protocol.IsGoreMode,
				out protocol.Type,
				out protocol.Scale,
				out protocol.Delay,
				out protocol.SpeedX,
				out protocol.SpeedY,
				out protocol.ColorR,
				out protocol.ColorG,
				out protocol.ColorB,
				out protocol.Alpha,
				out protocol.Scatter,
				out protocol.HasGravity,
				out protocol.HasLight,
				out protocol.IsActivated
			);

			protocol.TileX = tileX;
			protocol.TileY = tileY;

			protocol.SendToServer( true );
		}



		////////////////

		private EmitterDefinition Def => new EmitterDefinition(
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
