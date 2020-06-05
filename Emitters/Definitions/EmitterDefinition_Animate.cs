using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
		public void AnimateEmitter( Vector2 worldPos ) {
//DebugHelpers.Print( "emit_"+this.GetHashCode(), "timer: "+this.Timer+", "+this.ToString() );
			if( !this.IsActivated ) {
				return;
			}

			if( !this.AnimateTimer() ) {
				return;
			}

			int maxDistSqr = EmittersConfig.Instance.DustEmitterMinimumRangeBeforeEmit;
			maxDistSqr *= maxDistSqr;
			if( (Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr ) {
				return;
			}

			if( this.IsGoreMode ) {
				var scatter = new Vector2(
					Main.rand.NextFloat() * this.Scatter,
					Main.rand.NextFloat() * this.Scatter
				);

				int goreIdx = Gore.NewGore(
					Position: worldPos + scatter - new Vector2(this.Scatter * 0.5f),
					Velocity: new Vector2(this.SpeedX, this.SpeedY),
					Type: (int)this.Type,
					Scale: this.Scale
				);

				Main.gore[goreIdx].alpha = this.Alpha;
			} else {
				int dustIdx = Dust.NewDust(
					Position: worldPos - new Vector2(this.Scatter * 0.5f),
					Width: (int)this.Scatter,
					Height: (int)this.Scatter,
					Type: (int)this.Type,
					SpeedX: this.SpeedX,
					SpeedY: this.SpeedY,
					Alpha: (int)this.Alpha,
					newColor: this.Color,
					Scale: this.Scale
				);

				Main.dust[dustIdx].noGravity = !this.HasGravity;
				Main.dust[dustIdx].noLight = !this.HasLight;
			}
		}
	}
}
