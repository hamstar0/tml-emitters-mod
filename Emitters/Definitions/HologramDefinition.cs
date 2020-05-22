using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;


namespace Emitters.Definitions {
	public partial class HologramDefinition {
		public static HologramDefinition Read( BinaryReader reader ) {
			return new HologramDefinition(
				type: (int)reader.ReadUInt16(),
				scale: (float)reader.ReadSingle(),
				color: new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				alpha: (byte)reader.ReadByte(),
				direction: (int)reader.ReadUInt16(),
				rotation: (float)reader.ReadSingle(),
				offsetX: (int)reader.ReadUInt16(),
				offsetY: (int)reader.ReadUInt16(),
				frameStart: (int)reader.ReadUInt16(),
				frameEnd: (int)reader.ReadUInt16(),
				frameRateTicks: (int)reader.ReadUInt16(),
				worldLight: (bool)reader.ReadBoolean(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public static void Write( HologramDefinition def, BinaryWriter writer ) {
			writer.Write( (ushort)def.Type.Type );
			writer.Write( (float)def.Scale );
			writer.Write( (byte)def.Color.R );
			writer.Write( (byte)def.Color.G );
			writer.Write( (byte)def.Color.B );
			writer.Write( (byte)def.Alpha );
			writer.Write( (ushort)def.Direction );
			writer.Write( (float)def.Rotation );
			writer.Write( (ushort)def.OffsetX );
			writer.Write( (ushort)def.OffsetY );
			writer.Write( (ushort)def.FrameStart );
			writer.Write( (ushort)def.FrameEnd );
			writer.Write( (ushort)def.FrameRateTicks );
			writer.Write( (bool)def.WorldLighting );
			writer.Write( (bool)def.IsActivated );
		}



		////////////////

		internal int Timer = 0;


		////////////////

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

		////

		public bool IsActivated { get; set; } = true;



		////////////////

		public HologramDefinition() { }

		public HologramDefinition( HologramDefinition copy ) {
			this.Type = copy.Type;
			this.Scale = copy.Scale;
			this.Color = copy.Color;
			this.Alpha = copy.Alpha;
			this.Direction = copy.Direction;
			this.Rotation = copy.Rotation;
			this.OffsetX = copy.OffsetX;
			this.OffsetY = copy.OffsetY;
			this.FrameStart = copy.FrameStart;
			this.FrameEnd = copy.FrameEnd;
			this.FrameRateTicks = copy.FrameRateTicks;
			this.WorldLighting = copy.WorldLighting;
			this.IsActivated = copy.IsActivated;

			this.CurrentFrame = this.FrameStart;
		}

		public HologramDefinition(
					int type,
					float scale,
					Color color,
					byte alpha,
					int direction,
					float rotation,
					int offsetX,
					int offsetY,
					int frameStart,
					int frameEnd,
					int frameRateTicks,
					bool worldLight,
					bool isActivated ) {
			this.Type = new NPCDefinition( type );
			this.Scale = scale;
			this.Color = color;
			this.Alpha = alpha;
			this.Direction = direction;
			this.Rotation = rotation;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.FrameStart = frameStart;
			this.FrameEnd = frameEnd;
			this.FrameRateTicks = frameRateTicks;
			this.WorldLighting = worldLight;
			this.IsActivated = isActivated;

			this.CurrentFrame = frameStart;
		}


		////////////////

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}
	}
}
