using System.IO;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		public void Output(
					out int type,
					out int style,
					out float volume,
					out float pitch,
					out int delay,
					out bool isActivated ) {
			type = this.Type;
			style = this.Style;
			volume = this.Volume;
			pitch = this.Pitch;
			delay = this.Delay;
			isActivated = this.IsActivated;
		}

		////////////////

		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderStyle() {
			return this.Style.ToString();
		}
		public string RenderVolume() {
			return ( this.Volume * 100f ).ToString( "N0" ) + "%";
		}
		public string RenderPitch() {
			return this.Pitch.ToString( "N3" );
		}
		public string RenderDelay() {
			return this.Delay.ToString();
		}

		////////////////

		public override string ToString() {
			return "Sound Emitter Definition:"
				+/*"\n"+*/" Type: " + this.RenderType() + ", "
				+/*"\n"+*/" Type: " + this.RenderStyle() + ", "
				+/*"\n"+*/" Volume: " + this.RenderVolume() + ", "
				+/*"\n"+*/" Pitch: " + this.RenderPitch() + ", "
				+/*"\n"+*/" Delay: " + this.RenderDelay() + ", "
				+/*"\n"+*/" IsActivated: " + this.IsActivated;
		}
	}
}
