using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.UI;
using HamstarHelpers.Helpers.XNA;
using Emitters.Items;


namespace Emitters.Definitions {
	public partial class HologramDefinition {
		internal const int FrameDelay = 7;



		////////////////

		internal int FrameCounter = 0;
		internal int FrameTimer = 0;



		////////////////

		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			var wldPos = new Vector2( (tileX<<4)+8, (tileY<<4)+8 );
			this.AnimateHologram( wldPos, false );

			if( isOnScreen && HologramItem.CanViewHolograms( Main.LocalPlayer ) ) {
				this.DrawHologram( tileX, tileY );
			}
		}


		////////////////

		public void DrawHologram( int tileX, int tileY ) {
			Vector2 scr = UIHelpers.ConvertToScreenPosition( new Vector2(tileX<<4, tileY<<4) );
			Texture2D tex = EmittersMod.Instance.HologramTex;

			Main.spriteBatch.Draw(
				texture: tex,
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

		public void AnimateHologram( Vector2 worldPos, bool isUI ) {
			if( !this.IsActivated ) {
				return;
			}

			int maxDistSqr = EmittersConfig.Instance.DustEmitterMinimumRangeBeforeEmit;
			maxDistSqr *= maxDistSqr;
			if( (Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr ) {
				return;
			}

			Main.instance.LoadNPC( this.Type );

			Texture2D npcTexture = Main.npcTexture[ this.Type ];
			int frameHeight = npcTexture.Height / Main.npcFrameCount[this.Type];

			if( ++this.FrameTimer > HologramDefinition.FrameDelay ) {
				this.FrameCounter = this.FrameCounter + 1;
				this.FrameTimer = 0;
				if( this.FrameCounter >= Main.npcFrameCount[this.Type] - 1 ) {
					this.FrameCounter = 0;
				}
			}

			Color spriteColor = this.Color;
			SpriteEffects effects = SpriteEffects.None;
			Rectangle drawRectangle = new Rectangle(
				0,
				frameHeight * this.FrameCounter,
				npcTexture.Width,
				frameHeight
			);

			Vector2 origin = new Vector2( npcTexture.Width, frameHeight ) * 0.5f;
			Vector2 scrPos;

			if( this.WorldLighting ) {
				spriteColor = Lighting.GetColor( (int)(worldPos.X/16f), (int)(worldPos.Y/16f) );
				spriteColor = XNAColorHelpers.Mul( spriteColor, this.Color );
			}

			if( isUI ) {
				Vector2 wldPosOnScr = worldPos - Main.screenPosition;
				scrPos = wldPosOnScr - (origin * this.Scale);
			} else {
				scrPos = worldPos - (origin * this.Scale);
				scrPos.X += (float)this.OffsetX * this.Scale;
				scrPos.Y += (float)this.OffsetY * this.Scale;

				scrPos = UIHelpers.ConvertToScreenPosition( scrPos );
			}

			if( this.Direction == -1 ) {
				effects = SpriteEffects.FlipHorizontally;
			}

			Main.spriteBatch.Draw(
				texture: npcTexture,
				position: scrPos,
				sourceRectangle: drawRectangle,
				color: spriteColor,
				rotation: this.Rotation,
				origin: origin,
				scale: this.Scale * Main.GameZoomTarget,
				effects: effects,
				layerDepth: 1f
			);
		}
	}
}
