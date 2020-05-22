using System;
using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
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

		internal int Timer = 0;


		////////////////

		public bool IsGoreMode { get; set; }
		public int Type { get; set; }
		public float Scale { get; set; }
		public int Delay { get; set; }
		public float SpeedX { get; set; }
		public float SpeedY { get; set; }
		public Color Color { get; set; }
		public byte Alpha { get; set; }
		public float Scatter { get; set; }
		public bool HasGravity { get; set; }
		public bool HasLight { get; set; }

		////

		public bool IsActivated { get; set; } = true;



		////////////////

		public EmitterDefinition() { }

		public EmitterDefinition( EmitterDefinition copy ) {
			this.IsGoreMode = copy.IsGoreMode;
			this.Type = copy.Type;
			this.Scale = copy.Scale;
			this.Delay = copy.Delay;
			this.SpeedX = copy.SpeedX;
			this.SpeedY = copy.SpeedY;
			this.Color = copy.Color;
			this.Alpha = copy.Alpha;
			this.Scatter = copy.Scatter;
			this.HasGravity = copy.HasGravity;
			this.HasLight = copy.HasLight;
			this.IsActivated = copy.IsActivated;
		}

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

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}
	}
}
