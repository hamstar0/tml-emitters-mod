using System;
using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters {
	public partial class EmitterDefinition {
		public static EmitterDefinition Read( BinaryReader reader ) {
			return new EmitterDefinition(
				isGoreMode: (bool)reader.ReadBoolean(),
				type: (int)reader.ReadUInt16(),
				scale: (float)reader.ReadSingle(),
				delay: (int)reader.ReadInt16(),
				speedX: (float)reader.ReadSingle(),
				speedY: (float)reader.ReadSingle(),
				color: new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				alpha: (byte)reader.ReadByte(),
				scatter: (float)reader.ReadSingle(),
				hasGravity: (bool)reader.ReadBoolean(),
				hasLight: (bool)reader.ReadBoolean(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public static void Write( EmitterDefinition def, BinaryWriter writer ) {
			writer.Write( (bool)def.IsGoreMode );
			writer.Write( (ushort)def.Type );
			writer.Write( (float)def.Scale );
			writer.Write( (ushort)def.Delay );
			writer.Write( (float)def.SpeedX );
			writer.Write( (float)def.SpeedY );
			writer.Write( (byte)def.Color.R );
			writer.Write( (byte)def.Color.G );
			writer.Write( (byte)def.Color.B );
			writer.Write( (byte)def.Alpha );
			writer.Write( (float)def.Scatter );
			writer.Write( (bool)def.HasGravity );
			writer.Write( (bool)def.HasLight );
			writer.Write( (bool)def.IsActivated );
		}



		////////////////

		private int Timer = 0;


		////////////////

		public bool IsGoreMode { get; internal set; }
		public int Type { get; internal set; }
		public float Scale { get; internal set; }
		public int Delay { get; internal set; }
		public float SpeedX { get; internal set; }
		public float SpeedY { get; internal set; }
		public Color Color { get; internal set; }
		public byte Alpha { get; internal set; }
		public float Scatter { get; internal set; }
		public bool HasGravity { get; internal set; }
		public bool HasLight { get; internal set; }

		////

		public bool IsActivated { get; internal set; } = true;



		////////////////

		public EmitterDefinition() { }

		public EmitterDefinition(
					bool isGoreMode,
					int type,
					float scale,
					int delay,
					float speedX,
					float speedY,
					Color color,
					byte alpha,
					float scatter,
					bool hasGravity,
					bool hasLight,
					bool isActivated ) {
			this.IsGoreMode = isGoreMode;
			this.Type = type;
			this.Scale = scale;
			this.Delay = delay;
			this.SpeedX = speedX;
			this.SpeedY = speedY;
			this.Color = color;
			this.Alpha = alpha;
			this.Scatter = scatter;
			this.HasGravity = hasGravity;
			this.HasLight = hasLight;
			this.IsActivated = isActivated;
		}


		////

		public void Output(
					out bool isGoreMode,
					out int type,
					out float scale,
					out int delay,
					out float speedX,
					out float speedY,
					out Color color,
					out byte alpha,
					out float scatter,
					out bool hasGravity,
					out bool hasLight,
					out bool isActivated ) {
			isGoreMode = this.IsGoreMode;
			type = this.Type;
			scale = this.Scale;
			delay = this.Delay;
			speedX = this.SpeedX;
			speedY = this.SpeedY;
			color = this.Color;
			alpha = this.Alpha;
			scatter = this.Scatter;
			hasGravity = this.HasGravity;
			hasLight = this.HasLight;
			isActivated = this.IsActivated;
		}

		public void Output(
					out bool isGoreMode,
					out int type,
					out float scale,
					out int delay,
					out float speedX,
					out float speedY,
					out byte colorR,
					out byte colorG,
					out byte colorB,
					out byte alpha,
					out float scatter,
					out bool hasGravity,
					out bool hasLight,
					out bool isActivated ) {
			Color color;
			this.Output(
				out isGoreMode,
				out type,
				out scale,
				out delay,
				out speedX,
				out speedY,
				out color,
				out alpha,
				out scatter,
				out hasGravity,
				out hasLight,
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

		public string RenderMode() {
			return this.IsGoreMode
				? "Gores"
				: "Dusts";
		}
		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderScale() {
			return ( this.Scale * 100f ).ToString( "N0" );
		}
		public string RenderDelay() {
			return this.Delay.ToString();
		}
		public string RenderSpeedX() {
			return this.SpeedX.ToString();
		}
		public string RenderSpeedY() {
			return this.SpeedY.ToString();
		}
		public string RenderColor() {
			return this.Color.ToString();
		}
		public string RenderAlpha() {
			return this.Alpha.ToString();
		}
		public string RenderScatter() {
			return (this.Scatter * 100f).ToString( "N0" );
		}
		public string RenderHasGravity() {
			return this.HasGravity.ToString();
		}
		public string RenderHasLight() {
			return this.HasLight.ToString();
		}

		////////////////

		public override string ToString() {
			return "Emitter Definition:"
				+/*"\n"+*/" Mode: " + this.RenderMode() + ", "
				+/*"\n"+*/" Type: " + this.RenderType() + ", "
				+/*"\n"+*/" Scale: " + this.RenderScale() + ", "
				+/*"\n"+*/" Delay: " + this.RenderDelay() + ", "
				+/*"\n"+*/" SpeedX: " + this.RenderSpeedX() + ", "
				+/*"\n"+*/" SpeedY: " + this.RenderSpeedY() + ", "
				+/*"\n"+*/" Color: " + this.RenderColor() + ", "
				+/*"\n"+*/" Alpha: " + this.RenderAlpha() + ", "
				+/*"\n"+*/" Scatter: " + this.RenderScatter() + ", "
				+/*"\n"+*/" HasGravity: " + this.RenderHasGravity() + ", "
				+/*"\n"+*/" HasLight: " + this.RenderHasLight() + ", "
				+/*"\n"+*/" IsActivated: " + this.IsActivated;
		}
	}
}
