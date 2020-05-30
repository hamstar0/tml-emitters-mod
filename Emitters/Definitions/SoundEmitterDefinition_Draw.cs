using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.UI;
using Emitters.Items;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			this.AnimateSoundEmitter( new Vector2( (tileX<<4) + 8, (tileY<<4) + 8 ) );

			if( isOnScreen && SoundEmitterItem.CanViewSoundEmitters(Main.LocalPlayer) ) {
				this.DrawSoundEmitterTile( tileX, tileY );
			}
		}


		////////////////

		public void DrawSoundEmitterTile( int tileX, int tileY ) {
			Vector2 scr = UIHelpers.ConvertToScreenPosition( new Vector2(tileX<<4, tileY<<4) );

			Main.spriteBatch.Draw(
				texture: EmittersMod.Instance.SoundEmitterTex,
				position: scr,
				sourceRectangle: null,
				color: Color.White,
				rotation: 0f,
				origin: default( Vector2 ),
				scale: Main.GameZoomTarget,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}


		////////////////

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
