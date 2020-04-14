using System;
using Microsoft.Xna.Framework;
using Terraria;
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
			int scrX = (tileX<<4) - (int)Main.screenPosition.X;
			int scrY = (tileY<<4) - (int)Main.screenPosition.Y;

			Main.spriteBatch.Draw(
				texture: EmittersMod.Instance.Emitter,
				position: new Vector2(scrX, scrY),
				color: Color.White
			);
		}


		////////////////

		public void AnimateEmitter( Vector2 worldPos ) {
			f
		}
	}
}
