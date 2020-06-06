using Microsoft.Xna.Framework;
using Terraria;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public bool AnimateHologram( Vector2 worldPos, bool isUI ) {
			if( !this.IsActivated ) {
				return false;
			}
			if( HologramDefinition.IsBadType(this.Mode, this.Type) ) {
				return false;
			}

			// Cycle animations at all distances
			this.AnimateCurrentFrame();

			if( !isUI ) {
				int maxDistSqr = EmittersConfig.Instance.HologramMinimumRangeBeforeProject;
				maxDistSqr *= maxDistSqr;

				// Too far away?
				if( (Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr ) {
					return false;
				}
			}

			return true;
		}
	}
}
