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
	class HologramPlacementProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( HologramDefinition def, ushort tileX, ushort tileY ) {
			if( Main.netMode != 1 ) { throw new ModHelpersException("Not client."); }

			var protocol = new HologramPlacementProtocol( Main.myPlayer, def, tileX, tileY );

			protocol.SendToServer( true );
		}



		////////////////

		public int FromWho;

		public ushort TileX;
		public ushort TileY;

		public int Mode;
		public int Type;
		public float Scale;
		public byte ColorR;
		public byte ColorG;
		public byte ColorB;
		public byte Alpha;
		public int Direction;
		public float Rotation;
		public int OffsetX;
		public int OffsetY;
		public int FrameStart;
		public int FrameEnd;
		public int FrameRateTicks;
		public bool WorldLight;
		public bool CrtEffect;
		public bool IsActivated;



		////////////////

		private HologramPlacementProtocol() { }

		private HologramPlacementProtocol( int fromWho, HologramDefinition def, ushort tileX, ushort tileY ) {
			HologramMode mode;

			def.Output(
				out this.Type,
				out mode,
				out this.Scale,
				out this.ColorR,
				out this.ColorG,
				out this.ColorB,
				out this.Alpha,
				out this.Direction,
				out this.Rotation,
				out this.OffsetX,
				out this.OffsetY,
				out this.FrameStart,
				out this.FrameEnd,
				out this.FrameRateTicks,
				out this.WorldLight,
				out this.CrtEffect,
				out this.IsActivated
			) ;

			this.Mode = (int)mode;

			this.FromWho = fromWho;
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		private HologramDefinition GetNewHologram() => new HologramDefinition(
			mode: (HologramMode)this.Mode,
			type: this.Type,
			scale: this.Scale,
			color: new Color(this.ColorR, this.ColorG, this.ColorB),
			alpha: this.Alpha,
			direction: this.Direction,
			rotation: this.Rotation,
			offsetX: this.OffsetX,
			offsetY: this.OffsetY,
			frameStart: this.FrameStart,
			frameEnd: this.FrameEnd,
			frameRateTicks: this.FrameRateTicks,
			worldLight: this.WorldLight,
			crtEffect: this.CrtEffect,
			isActivated: this.IsActivated
		);


		////////////////

		protected override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			Main.PlaySound( SoundID.Item108, new Vector2(this.TileX<<4, this.TileY<<4) );

			myworld.AddHologram( this.GetNewHologram(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<HologramItem>(), 1 );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			myworld.AddHologram( this.GetNewHologram(), this.TileX, this.TileY );

			PlayerItemHelpers.RemoveInventoryItemQuantity( Main.player[this.FromWho], ModContent.ItemType<HologramItem>(), 1 );
		}
	}
}
