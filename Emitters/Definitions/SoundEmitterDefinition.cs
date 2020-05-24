using System.IO;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition {
		public static SoundEmitterDefinition Read( BinaryReader reader ) {
			return new SoundEmitterDefinition(
				type: (int)reader.ReadUInt16(),
				style: (int)reader.ReadUInt16(),
				volume: (float)reader.ReadSingle(),
				pitch: (float)reader.ReadSingle(),
				delay: (int)reader.ReadInt16(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public static void Write( SoundEmitterDefinition def, BinaryWriter writer ) {
			writer.Write( (ushort)def.Type );
			writer.Write( (ushort)def.Style );
			writer.Write( (float)def.Volume );
			writer.Write( (float)def.Pitch );
			writer.Write( (ushort)def.Delay );
			writer.Write( (bool)def.IsActivated );
		}



		////////////////

		internal int Timer = 0;


		////////////////
		public int Type { get; set; }
		public int Style { get; set; }
		public float Volume { get; set; }
		public float Pitch { get; set; }
		public int Delay { get; set; }

		////

		public bool IsActivated { get; set; } = true;



		////////////////

		public SoundEmitterDefinition() { }

		public SoundEmitterDefinition( SoundEmitterDefinition copy ) {
			this.Type = copy.Type;
			this.Style = copy.Style;
			this.Volume = copy.Volume;
			this.Pitch = copy.Pitch;
			this.Delay = copy.Delay;
			this.IsActivated = copy.IsActivated;
		}

		public SoundEmitterDefinition(
					int type,
					int style,
					float volume,
					float pitch,
					int delay,
					bool isActivated ) {
			this.Type = type;
			this.Style = style;
			this.Volume = volume;
			this.Pitch = pitch;
			this.Delay = delay;
			this.IsActivated = isActivated;
		}


		////////////////
		
		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
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
