using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.UI;
using HamstarHelpers.Helpers.XNA;
using Emitters.Items;


namespace Emitters.Definitions {
	public partial class HologramDefinition {
		public int CurrentFrame { get; internal set; }

		internal int CurrentFrameElapsedTicks = 0;



		////////////////

		public void Draw( int tileX, int tileY, bool isOnScreen ) {
			var wldPos = new Vector2( (tileX << 4) + 8, (tileY << 4) + 8 );
			this.AnimateHologram( wldPos, false );

			if( isOnScreen && HologramItem.CanViewHolograms( Main.LocalPlayer ) ) {
				this.DrawHologramTile( tileX, tileY );
			}
		}


		////////////////

		public void DrawHologramTile( int tileX, int tileY ) {
			Vector2 scr = UIHelpers.ConvertToScreenPosition( new Vector2( tileX << 4, tileY << 4 ) );
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
			
			if (CheckIfNull()) {
				return;
			}

			// Cycle animations at all distances
			this.AnimateCurrentFrame();

			int maxDistSqr = EmittersConfig.Instance.HologramMinimumRangeBeforeProject;
			maxDistSqr *= maxDistSqr;

			// Too far away?
			if( (Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr ) {
				return;
			}

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				this.Mode == 2 || this.Mode == 3 ? SamplerState.PointClamp : SamplerState.LinearClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone
			);
			
			switch (this.Mode)
			{
				case 1:
					Main.instance.LoadNPC(this.Type);
					break;
				case 3:
					Main.instance.LoadProjectile(this.Type);
					break;
				default:
					break;
			}

			if ( this.CrtEffect ) {
				this.CRTEffectBegin();
			}

			try {
				this.DrawHologramRaw( worldPos, isUI );
			} finally {
				if( this.CrtEffect ) {
					this.CRTEffectEnd();
				}
			}
		}


		///////////

		public void CRTEffectBegin()
		{
			Texture2D tex = EmitterUtils.GetTexture(this.Mode, this.Type);
			Effect fx = EmittersMod.Instance.HologramFX;
			Color color = this.Color;
			color.A = this.Alpha;

			fx.Parameters["TexWidth"].SetValue( (float)tex.Width * this.Scale );
			fx.Parameters["TexHeight"].SetValue( (float)tex.Height * this.Scale );
			fx.Parameters["RandValue"].SetValue( Main.rand.NextFloat() );
			fx.Parameters["Time"].SetValue( Main.GlobalTime % 3600f );

			fx.Parameters["Frame"].SetValue( (float)this.CurrentFrame );
			fx.Parameters["FrameMax"].SetValue( (float)Main.npcFrameCount[this.Type] );

			fx.Parameters["UserColor"].SetValue( color.ToVector4() );

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				SamplerState.PointClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone,
				fx
				//Main.GameViewMatrix.ZoomMatrix
			);
		}

		public void CRTEffectEnd() {
			Main.spriteBatch.End();
			Main.spriteBatch.Begin();
		}


		////

		public void DrawHologramRaw( Vector2 worldPos, bool isUI )
		{
			Texture2D hologramTexture = EmitterUtils.GetTexture(this.Mode,this.Type);
			int hologramType = this.Type;
			int frameCount = EmitterUtils.GetFrameCount(this.Mode, this.Type);
			if (frameCount == 1)
			{

			}
			int frameHeight = hologramTexture.Height / frameCount;
			Color color = this.Color;
			SpriteEffects effects = SpriteEffects.None;
			Rectangle frame = new Rectangle(
				x: 0,
				y: frameHeight * this.CurrentFrame,
				width: hologramTexture.Width,
				height: frameHeight
			);

			Vector2 origin = new Vector2( hologramTexture.Width, frameHeight ) * 0.5f;
			Vector2 scrPos;

			if( this.WorldLighting ) {
				color = Lighting.GetColor( (int)( worldPos.X / 16f ), (int)( worldPos.Y / 16f ) );
				color = XNAColorHelpers.Mul( color, this.Color );
			}
			color *= (float)this.Alpha / 255f;

			if( isUI ) {
				scrPos = worldPos - Main.screenPosition;
				//scrPos.X -= npcTexture.Width;
				scrPos.X += this.OffsetX;
				scrPos.Y += this.OffsetY;
				scrPos *= Main.GameZoomTarget;
			} else {
				scrPos = UIHelpers.ConvertToScreenPosition( worldPos );
				scrPos.X += this.OffsetX;
				scrPos.Y += this.OffsetY;
			}

			if( this.Direction == -1 ) {
				effects = SpriteEffects.FlipHorizontally;
			}

			Main.spriteBatch.Draw(
				texture: hologramTexture,
				position: scrPos,
				sourceRectangle: frame,
				color: color,
				rotation: MathHelper.ToRadians( this.Rotation ),
				origin: isUI ? default(Vector2) : origin,
				scale: this.Scale * Main.GameZoomTarget,
				effects: effects,
				layerDepth: 1f
			);
		}

		
	}
}
