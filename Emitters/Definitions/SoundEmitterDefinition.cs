using System.IO;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		internal int Timer = 0;


		////////////////

		public int Type { get; set; }
		public int Style { get; set; }
		public float Volume { get; set; }
		public float Pitch { get; set; }
		public int Delay { get; set; }



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

		public override BaseEmitterDefinition Read( BinaryReader reader ) {
			return new SoundEmitterDefinition(
				type: (int)reader.ReadUInt16(),
				style: (int)reader.ReadUInt16(),
				volume: (float)reader.ReadSingle(),
				pitch: (float)reader.ReadSingle(),
				delay: (int)reader.ReadInt16(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (ushort)this.Type );
			writer.Write( (ushort)this.Style );
			writer.Write( (float)this.Volume );
			writer.Write( (float)this.Pitch );
			writer.Write( (ushort)this.Delay );
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
