using System;
using Microsoft.Xna.Framework;


namespace Emitters {
	public class EmitterDefinition {
		public bool IsGoreMode { get; internal set; }
		public int Type { get; internal set; }
		public float Scale { get; internal set; }
		public float SpeedX { get; internal set; }
		public float SpeedY { get; internal set; }
		public Color Color { get; internal set; }
		public float Alpha { get; internal set; }
		public float Scatter { get; internal set; }
		public bool HasGravity { get; internal set; }
		public bool HasLight { get; internal set; }



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
			return (this.Scale * 100f).ToString( "N0" );
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
			return (this.Alpha * 100f).ToString( "N0" );
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
	}
}
