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

			protocol.TileX = tileX;
			protocol.TileY = tileY;

			protocol.IsGoreMode = def.IsGoreMode;
			protocol.Type = def.Type;
			protocol.Scale = def.Scale;
			protocol.Delay = def.Delay;
			protocol.SpeedX = def.SpeedX;
			protocol.SpeedY = def.SpeedY;
			protocol.ColorR = def.Color.R;
			protocol.ColorG = def.Color.G;
			protocol.ColorB = def.Color.B;
			protocol.Alpha = def.Alpha;
			protocol.Scatter = def.Scatter;
			protocol.HasGravity = def.HasGravity;
			protocol.HasLight = def.HasLight;
			protocol.IsActivated = def.IsActivated;

			protocol.SendToServer( true );
		}



		////////////////

		private EmitterDefinition Def => new EmitterDefinition {
			IsGoreMode = this.IsGoreMode,
			Type = this.Type,
			Scale = this.Scale,
			Delay = this.Delay,
			SpeedX = this.SpeedX,
			SpeedY = this.SpeedY,
			Color = new Color( this.ColorR, this.ColorG, this.ColorB ),
			Alpha = this.Alpha,
			Scatter = this.Scatter,
			HasGravity = this.HasGravity,
			HasLight = this.HasLight,
			IsActivated = this.IsActivated,
		};


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
