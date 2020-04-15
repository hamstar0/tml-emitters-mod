using System;
using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters {
	public partial class EmitterDefinition {
		public static EmitterDefinition Read( BinaryReader reader ) {
			return new EmitterDefinition {
				IsGoreMode = (bool)reader.ReadBoolean(),
				Type = (int)reader.ReadUInt16(),
				Scale = (float)reader.ReadSingle(),
				Delay = (int)reader.ReadInt16(),
				SpeedX = (float)reader.ReadSingle(),
				SpeedY = (float)reader.ReadSingle(),
				Color = new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				Alpha = (byte)reader.ReadByte(),
				Scatter = (float)reader.ReadSingle(),
				HasGravity = (bool)reader.ReadBoolean(),
				HasLight = (bool)reader.ReadBoolean(),
				IsActivated = (bool)reader.ReadBoolean(),
			};
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
