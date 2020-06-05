using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public void Output(
					out int type,
					out HologramMode mode,
					out float scale,
					out Color color,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frameStart,
					out int frameEnd,
					out int frameRateTicks,
					out bool worldLight,
					out HologramShaderMode shaderMode,
					out float shaderTime,
					out int shaderType,
					out bool isActivated ) {
			type = this.Type;
			mode = this.Mode;
			scale = this.Scale;
			color = this.Color;
			alpha = this.Alpha;
			direction = this.Direction;
			rotation = this.Rotation;
			offsetX = this.OffsetX;
			offsetY = this.OffsetY;
			frameStart = this.FrameStart;
			frameEnd = this.FrameEnd;
			frameRateTicks = this.FrameRateTicks;
			worldLight = this.WorldLighting;
			shaderMode = this.ShaderMode;
			shaderTime = this.ShaderTime;
			shaderType = this.ShaderType;
			isActivated = this.IsActivated;
		}

		public void Output(
					out int type,
					out HologramMode mode,
					out float scale,
					out byte colorR,
					out byte colorG,
					out byte colorB,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frameStart,
					out int frameEnd,
					out int frameRateTicks,
					out bool worldLight,
					out HologramShaderMode shaderMode,
					out float shaderTime,
					out int shaderType,
					out bool isActivated ) {
			Color color;
			this.Output(
				out type,
				out mode,
				out scale,
				out color,
				out alpha,
				out direction,
				out rotation,
				out offsetX,
				out offsetY,
				out frameStart,
				out frameEnd,
				out frameRateTicks,
				out worldLight,
				out shaderMode,
				out shaderTime,
				out shaderType,
				out isActivated
			);
			colorR = color.R;
			colorG = color.G;
			colorB = color.B;
		}


		////////////////

		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderMode()
		{
			return this.Mode.ToString();
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
		public string RenderOffset() {
			return this.OffsetX.ToString()+", "+this.OffsetY.ToString();
		}
		public string RenderFrameStart() {
			return this.FrameStart.ToString();
		}
		public string RenderFrameEnd() {
			return this.FrameEnd.ToString();
		}
		public string RenderFrameRateTicks() {
			return this.FrameRateTicks.ToString();
		}
		public string RenderFrame() {
			return "#"+this.CurrentFrame+" between "+this.FrameStart+" and "+this.FrameEnd+" (rate: "+this.FrameRateTicks+")";
		}
		public string RenderShaderMode()
		{
			return this.ShaderMode.ToString();
		}
		public string RenderShaderTime()
		{
			return this.ShaderTime.ToString();
		}
		public string RenderShaderType()
		{
			return this.ShaderType.ToString();
		}
		////////////////

		public override string ToString() {
			return "Emitter Definition:"
				+/*"\n"+*/" Type: " + this.RenderType() + ", "
				+/*"\n"+*/" Mode: " + this.RenderMode() + ", "
				+/*"\n"+*/" Scale: " + this.RenderScale() + ", "
				+/*"\n"+*/" Color: " + this.RenderColor() + ", "
				+/*"\n"+*/" Alpha: " + this.RenderAlpha() + ", "
				+/*"\n"+*/" Direction: " + this.RenderDirection() + ", "
				+/*"\n"+*/" Rotation: " + this.RenderRotation() + ", "
				+/*"\n"+*/" Offset: " + this.RenderOffset() + ", "
				+/*"\n"+*/" Frame: " + this.RenderFrame() + ", "
				+/*"\n"+*/" WorldLight: " + this.WorldLighting + ", "
				+/*"\n"+*/" ShaderMode: " + this.RenderShaderMode() + ", "
				+/*"\n"+*/" ShaderTime: " + this.ShaderTime + ", "
				+/*"\n"+*/" ShaderTime: " + this.ShaderType + ", "
				+/*"\n"+*/" IsActivated: " + this.IsActivated;
		}
	}
}
