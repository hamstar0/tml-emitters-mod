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
				frame: (int)reader.ReadUInt16(),
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
			writer.Write( (ushort)def.Frame );
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
		public int Frame { get; set; }
		////
		public bool WorldLighting { get; set; }
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
			this.Frame = copy.Frame;
			this.WorldLighting = copy.WorldLighting;
			this.IsActivated = copy.IsActivated;
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
					int frame,
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
			this.Frame = frame;
			this.WorldLighting = worldLight;
			this.IsActivated = isActivated;
		}


		////

		public void Output(
					out int type,
					out float scale,
					out Color color,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frame,
					out bool worldLight,
					out bool isActivated ) {
			type = this.Type.Type;
			scale = this.Scale;
			color = this.Color;
			alpha = this.Alpha;
			direction = this.Direction;
			rotation = this.Rotation;
			offsetX = this.OffsetX;
			offsetY = this.OffsetY;
			frame = this.Frame;
			worldLight = this.WorldLighting;
			isActivated = this.IsActivated;
		}

		public void Output(
					out int type,
					out float scale,
					out byte colorR,
					out byte colorG,
					out byte colorB,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frame,
					out bool worldLight,
					out bool isActivated ) {
			Color color;
			this.Output(
				out type,
				out scale,
				out color,
				out alpha,
				out direction,
				out rotation,
				out offsetX,
				out offsetY,
				out frame,
				out worldLight,
				out isActivated
			);
			colorR = color.R;
			colorG = color.G;
			colorB = color.B;
		}


		////////////////

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}


		////////////////

		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderScale() {
			return (this.Scale * 100f).ToString( "N0" );
		}
		public string RenderColor() {
			return this.Color.ToString();
		}
		public string RenderAlpha() {
			return this.Alpha.ToString();
		}
		public string RenderDirection() {
			return this.Direction.ToString( "N0" );
		}
		public string RenderRotation() {
			return this.Rotation.ToString( "N2" );
		}
		public string RenderOffsetX() {
			return this.OffsetX.ToString();
		}
		public string RenderOffsetY() {
			return this.OffsetY.ToString();
		}
		public string RenderFrame() {
			return this.Frame.ToString();
		}

		////////////////

		public override string ToString() {
			return "Emitter Definition:"
				+/*"\n"+*/" Type: " + this.RenderType() + ", "
				+/*"\n"+*/" Scale: " + this.RenderScale() + ", "
				+/*"\n"+*/" Color: " + this.RenderColor() + ", "
				+/*"\n"+*/" Alpha: " + this.RenderAlpha() + ", "
				+/*"\n"+*/" Direction: " + this.RenderDirection() + ", "
				+/*"\n"+*/" Rotation: " + this.RenderRotation() + ", "
				+/*"\n"+*/" OffsetX: " + this.RenderOffsetX() + ", "
				+/*"\n"+*/" OffsetY: " + this.RenderOffsetY() + ", "
				+/*"\n"+*/" Frame: " + this.RenderFrame() + ", "
				+/*"\n"+*/" WorldLight: " + this.WorldLighting + ", "
				+/*"\n"+*/" IsActivated: " + this.IsActivated;
		}
	}
}
