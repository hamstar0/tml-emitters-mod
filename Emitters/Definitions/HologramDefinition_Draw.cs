using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.UI;
using Emitters.Items;
using HamstarHelpers.Helpers.XNA;

namespace Emitters.Definitions {
	public partial class HologramDefinition {
		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			this.AnimateHologram( new Vector2( (tileX * 16) + 8, (tileY * 16) + 8 ), false );

			if ( isOnScreen && HologramItem.CanViewHolograms( Main.LocalPlayer ) ) {
				this.DrawHologram(tileX, tileY);
			}
		}


		////////////////
		//
		public void DrawHologram( int tileX, int tileY ) {
			Vector2 scr = UIHelpers.ConvertToScreenPosition( new Vector2( tileX << 4, tileY << 4 ) );
			Main.spriteBatch.Draw(
				texture: EmittersMod.Instance.HologramTex,
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
		internal int frameCounter = 0;
		internal int frameTimer = 0;
		const int frameDelay = 7;
		public void AnimateHologram(Vector2 worldPos, bool isUI)
		{
			if (!this.IsActivated)
			{
				return;
			}
			int maxDistSqr = EmittersConfig.Instance.DustEmitterMinimumRangeBeforeEmit;
			maxDistSqr *= maxDistSqr;
			if ((Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr)
			{
				return;
			}

			Main.instance.LoadNPC(this.Type);
			Texture2D npcTexture = Main.npcTexture[this.Type];
			if (++frameTimer > frameDelay)
			{
				frameCounter = frameCounter + 1;
				frameTimer = 0;
				if (frameCounter >= Main.npcFrameCount[this.Type] - 1)
				{
					frameCounter = 0;
				}
			}
			Color spriteColor = this.Color;

			Rectangle drawRectangle = new Rectangle( 
				0,
				npcTexture.Height / Main.npcFrameCount[this.Type] * frameCounter, 
				npcTexture.Width,
				npcTexture.Height / Main.npcFrameCount[this.Type]
			);

			Vector2 origin = new Vector2 ( npcTexture.Width / 2 , npcTexture.Height / Main.npcFrameCount[this.Type] );
			Vector2 scr;

			if(this.WorldLighting){spriteColor = XNAColorHelpers.Mul( Lighting.GetColor( (int)(worldPos.X / 16),(int)(worldPos.Y / 16), Color.White ), spriteColor );}

			if (isUI)
			{
				worldPos -= Main.screenPosition;
				scr = worldPos - origin;
			}
			else
			{
				scr = UIHelpers.ConvertToScreenPosition(new Vector2(
					(worldPos.X - origin.X * this.Scale) + this.OffsetX * this.Scale,
					(worldPos.Y - origin.Y * this.Scale) + this.OffsetY * this.Scale
					));
			}

			
			Main.spriteBatch.Draw(
				texture: npcTexture,
				position: scr,
				sourceRectangle: drawRectangle,
				color: spriteColor,
				rotation: 0f,
				origin: default(Vector2),
				scale: this.Scale * Main.GameZoomTarget,
				effects: SpriteEffects.None,
				layerDepth: 1f
			);
		}


	}
	
}



