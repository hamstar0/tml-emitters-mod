using Emitters.Items;
using HamstarHelpers.Helpers.UI;
using HamstarHelpers.Helpers.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace Emitters.Definitions
{
	public partial class HologramDefinition
	{
		public int CurrentFrame { get; internal set; }

		internal int CurrentFrameElapsedTicks = 0;



		////////////////

		public void Draw(int tileX, int tileY, bool isOnScreen)
		{
			var wldPos = new Vector2((tileX << 4) + 8, (tileY << 4) + 8);
			this.AnimateHologram(wldPos, false);

			if (isOnScreen && HologramItem.CanViewHolograms(Main.LocalPlayer))
			{
				this.DrawHologramTile(tileX, tileY);
			}
		}


		////////////////

		public void DrawHologramTile(int tileX, int tileY)
		{
			Vector2 scr = UIHelpers.ConvertToScreenPosition(new Vector2(tileX << 4, tileY << 4));
			Texture2D tex = EmittersMod.Instance.HologramTex;

			Main.spriteBatch.Draw(
				texture: tex,
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

		public void AnimateHologram(Vector2 worldPos, bool isUI)
		{
			if (!this.IsActivated)
			{
				return;
			}

			// Cycle animations at all distances
			this.AnimateCurrentFrame();

			int maxDistSqr = EmittersConfig.Instance.HologramMinimumRangeBeforeProject;
			maxDistSqr *= maxDistSqr;

			// Too far away?
			if ((Main.LocalPlayer.Center - worldPos).LengthSquared() >= maxDistSqr)
			{
				return;
			}

			int npcType = this.Type.Type;
			int frameCount = Main.npcFrameCount[npcType];

			Main.instance.LoadNPC(npcType);
			Texture2D npcTexture = Main.npcTexture[npcType];

			if (this.CrtEffect)
			{
				this.CRTEffect(npcTexture);
			}

			int frameHeight = npcTexture.Height / frameCount;

			Color color = this.Color;
			SpriteEffects effects = SpriteEffects.None;
			Rectangle drawRectangle = new Rectangle(
				x: 0,
				y: frameHeight * this.CurrentFrame,
				width: npcTexture.Width,
				height: frameHeight
			);

			Vector2 origin = new Vector2(npcTexture.Width, frameHeight) * 0.5f;
			Vector2 scrPos;

			if (this.WorldLighting)
			{
				color = Lighting.GetColor((int)(worldPos.X / 16f), (int)(worldPos.Y / 16f));
				color = XNAColorHelpers.Mul(color, this.Color);
			}
			color *= (float)this.Alpha / 255f;

			if (isUI)
			{
				scrPos = worldPos - Main.screenPosition;
				//scrPos.X -= npcTexture.Width;
				scrPos *= Main.GameZoomTarget;
			}
			else
			{
				scrPos = UIHelpers.ConvertToScreenPosition(worldPos);
			}
			scrPos.X += this.OffsetX;
			scrPos.Y += this.OffsetY;

			if (this.Direction == -1)
			{
				effects = SpriteEffects.FlipHorizontally;
			}

			Main.spriteBatch.Draw(
				texture: npcTexture,
				position: scrPos,
				sourceRectangle: drawRectangle,
				color: color,
				rotation: MathHelper.ToRadians(this.Rotation),
				origin: isUI ? default(Vector2) : origin,
				scale: this.Scale * Main.GameZoomTarget,
				effects: effects,
				layerDepth: 1f
			);

			if (this.CrtEffect)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin();
			}
		}

		///////////

		public void CRTEffect(Texture2D texture)
		{
			Effect Scanlines = EmittersMod.Instance.GetEffect("Effects/ScanlinesPS");
			Scanlines.Parameters["TexWidth"].SetValue(texture.Width*this.Scale);
			Scanlines.Parameters["TexHeight"].SetValue(texture.Height*this.Scale);
			Random random = new Random();
			double randVal = random.NextDouble();
			Scanlines.Parameters["RandValue"].SetValue((float)randVal);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, Scanlines);
		}

	}

}
