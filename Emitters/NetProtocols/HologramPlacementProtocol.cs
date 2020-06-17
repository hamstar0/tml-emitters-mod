using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.Protocols.Packet.Interfaces;
using HamstarHelpers.Helpers.Players;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.NetProtocols {
	class HologramPlacementProtocol : PacketProtocolBroadcast {
		public static void BroadcastFromClient( HologramDefinition def, ushort tileX, ushort tileY ) {
			if( Main.netMode != NetmodeID.MultiplayerClient ) { throw new ModHelpersException("Not client."); }

			var protocol = new HologramPlacementProtocol( Main.myPlayer, def, tileX, tileY );

			protocol.SendToServer( true );
		}



		////////////////

		public int FromWho;

		public ushort TileX;
		public ushort TileY;

		public int Mode;
		public string TypeDefRaw;
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
		public int ShaderMode;
		public float ShaderTime;
		public int ShaderType;
		public bool IsActivated;



		////////////////

		private HologramPlacementProtocol() { }

		private HologramPlacementProtocol( int fromWho, HologramDefinition def, ushort tileX, ushort tileY ) {
			HologramMode mode;
			HologramShaderMode shaderMode;
			EntityDefinition typeDef;

			def.Output(
				typeDef: out typeDef,
				mode: out mode,
				scale: out this.Scale,
				colorR: out this.ColorR,
				colorG: out this.ColorG,
				colorB: out this.ColorB,
				alpha: out this.Alpha,
				direction: out this.Direction,
				rotation: out this.Rotation,
				offsetX: out this.OffsetX,
				offsetY: out this.OffsetY,
				frameStart: out this.FrameStart,
				frameEnd: out this.FrameEnd,
				frameRateTicks: out this.FrameRateTicks,
				worldLight: out this.WorldLight,
				shaderMode: out shaderMode,
				shaderTime: out this.ShaderTime,
				shaderType: out this.ShaderType,
				isActivated: out this.IsActivated
			) ;

			this.TypeDefRaw = typeDef.ToString();
			this.Mode = (int)mode;
			this.ShaderMode = (int)shaderMode;

			this.FromWho = fromWho;
			this.TileX = tileX;
			this.TileY = tileY;
		}


		////////////////

		private HologramDefinition GetNewHologram() => new HologramDefinition(
			mode: (HologramMode)this.Mode,
			typeDef: HologramDefinition.GetTypeDef( (HologramMode)this.Mode, this.TypeDefRaw ),
			scale: this.Scale,
			color: new Color( this.ColorR, this.ColorG, this.ColorB ),
			alpha: this.Alpha,
			direction: this.Direction,
			rotation: this.Rotation,
			offsetX: this.OffsetX,
			offsetY: this.OffsetY,
			frameStart: this.FrameStart,
			frameEnd: this.FrameEnd,
			frameRateTicks: this.FrameRateTicks,
			worldLight: this.WorldLight,
			shaderMode: (HologramShaderMode)this.ShaderMode,
			shaderTime: this.ShaderTime,
			shaderType: this.ShaderType,
			isActivated: this.IsActivated
		);


		////////////////

		protected override void ReceiveOnClient() {
			Main.PlaySound( SoundID.Item108, new Vector2(this.TileX<<4, this.TileY<<4) );

			var myworld = ModContent.GetInstance<EmittersWorld>();
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
