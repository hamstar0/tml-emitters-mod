using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.UI;
using Emitters.Items;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			var wldPos = new Vector2( (tileX<<4)+8, (tileY<<4)+8 );
			this.AnimateEmitter( wldPos );

			if( isOnScreen && EmitterItem.CanViewEmitters(Main.LocalPlayer) ) {
				this.DrawEmitterTile( tileX, tileY );
			}
		}


		////////////////

		public void DrawEmitterTile( int tileX, int tileY ) {
			Vector2 scr = UIHelpers.ConvertToScreenPosition( new Vector2(tileX<<4, tileY<<4) );

			Main.spriteBatch.Draw(
				texture: EmittersMod.Instance.EmitterTex,
				position: scr,
				sourceRectangle: null,
				color: Color.White,
				rotation: 0f,
				origin: default(Vector2),
				scale: Main.GameZoomTarget,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}


		////////////////

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
