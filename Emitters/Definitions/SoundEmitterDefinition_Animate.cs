using Microsoft.Xna.Framework;
using Terraria;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		public void AnimateSoundEmitter( Vector2 worldPos ) {
			//DebugHelpers.Print( "emit_"+this.GetHashCode(), "timer: "+this.Timer+", "+this.ToString() );
			if( !this.IsActivated ) {
				return;
			}

			if( !this.AnimateTimer() ) {
				return;
			}

			int maxDistSqr = EmittersConfig.Instance.SoundEmitterMinimumRangeBeforeEmit;
			maxDistSqr *= maxDistSqr;

			if( (Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr ) {
				return;
			}

			Main.PlaySound( this.Type, (int)worldPos.X, (int)worldPos.Y, this.Style, this.Volume, this.Pitch );
		}
	}
}
