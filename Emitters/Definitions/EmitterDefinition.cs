using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
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



		////////////////

		public override BaseEmitterDefinition Read( BinaryReader reader ) {
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

		public override void Write( BinaryWriter writer ) {
			writer.Write( (bool)this.IsGoreMode );
			writer.Write( (ushort)this.Type );
			writer.Write( (float)this.Scale );
			writer.Write( (ushort)this.Delay );
			writer.Write( (float)this.SpeedX );
			writer.Write( (float)this.SpeedY );
			writer.Write( (byte)this.Color.R );
			writer.Write( (byte)this.Color.G );
			writer.Write( (byte)this.Color.B );
			writer.Write( (byte)this.Alpha );
			writer.Write( (float)this.Scatter );
			writer.Write( (bool)this.HasGravity );
			writer.Write( (bool)this.HasLight );
			writer.Write( (bool)this.IsActivated );
		}


		////////////////

		private bool AnimateTimer() {
			if( this.Timer++ < this.Delay ) {
				return false;
			}

			this.Timer = 0;
			return true;
		}
	}
}
