using System;
using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
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
			alpha = this.Transparency;
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

		public string RenderMode() {
			return this.IsGoreMode
				? "Gores"
				: "Dusts";
		}
		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderScale() {
			return (this.Scale * 100f).ToString( "N0" ) + "%";
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
			Color color = this.Color;
			color.A = this.Transparency;
			return color.ToString();
		}
		public string RenderAlpha() {
			return this.Transparency.ToString();
		}
		public string RenderScatter() {
			return this.Scatter.ToString( "N2" );
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
				+/*"\n"+*/" IsActivated: " + this.IsActivated + ", "
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
				+/*"\n"+*/" HasLight: " + this.RenderHasLight();
		}
	}
}
