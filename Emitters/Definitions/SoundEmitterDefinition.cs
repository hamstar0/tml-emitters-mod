using System;


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

		private bool AnimateTimer() {
			if( this.Timer++ < this.Delay ) {
				return false;
			}

			this.Timer = 0;
			return true;
		}
	}
}
