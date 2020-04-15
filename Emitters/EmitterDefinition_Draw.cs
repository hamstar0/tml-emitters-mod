using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using Emitters.Items;


namespace Emitters {
	public partial class EmitterDefinition {
		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			this.AnimateEmitter( new Vector2((tileX<<4)+8, (tileY<<4)+8) );

			if( isOnScreen && EmitterItem.CanViewEmitters(Main.LocalPlayer) ) {
				this.DrawEmitter( tileX, tileY );
			}
		}


		////////////////

		public void DrawEmitter( int tileX, int tileY ) {
			Vector2 pos = Main.screenPosition;
			int scrX = (tileX<<4) - (int)pos.X;
			int scrY = (tileY<<4) - (int)pos.Y;

			Main.spriteBatch.Draw(
				texture: EmittersMod.Instance.Emitter,
				position: new Vector2(scrX, scrY),
				color: Color.White
			);
		}


		////////////////

		public void AnimateEmitter( Vector2 worldPos ) {
//DebugHelpers.Print( "emit_"+this.GetHashCode(), "timer: "+this.Timer+", "+this.ToString() );
			if( !this.IsActivated ) {
				return;
			}

			if( this.Timer++ < this.Delay ) {
				return;
			}
			this.Timer = 0;

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
