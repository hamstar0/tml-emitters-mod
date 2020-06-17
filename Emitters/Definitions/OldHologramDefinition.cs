using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;


namespace Emitters.Definitions {
	public partial class OldHologramDefinition : BaseEmitterDefinition {
		public NPCDefinition Type { get; set; }
		public float Scale { get; set; }
		public Color Color { get; set; }
		public byte Alpha { get; set; }
		public int Direction { get; set; }
		public float Rotation { get; set; }
		public int OffsetX { get; set; }
		public int OffsetY { get; set; }
		public int FrameStart { get; set; }
		public int FrameEnd { get; set; }
		public int FrameRateTicks { get; set; }
		public bool WorldLighting { get; set; }
		public bool CrtEffect { get; set; }



		////////////////

		public OldHologramDefinition() { }


		////////////////

		public override void Read( BinaryReader reader ) {
			throw new NotImplementedException();
		}

		public override void Write( BinaryWriter writer ) {
			throw new NotImplementedException();
		}


		////////////////

		public HologramDefinition ConvertToNew() {
			return new HologramDefinition(
				mode: HologramMode.NPC,
				typeDef: this.Type,
				scale: this.Scale,
				color: this.Color,
				alpha: this.Alpha,
				direction: this.Direction,
				rotation: this.Rotation,
				offsetX: this.OffsetX,
				offsetY: this.OffsetY,
				frameStart: this.FrameStart,
				frameEnd: this.FrameEnd,
				frameRateTicks: this.FrameRateTicks,
				worldLight: this.WorldLighting,
				shaderMode: HologramShaderMode.None,
				shaderTime: 1f,
				shaderType: 0,
				isActivated: this.IsActivated
			);
		}
	}
}
