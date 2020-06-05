using Emitters.Items;
using HamstarHelpers.Helpers.UI;
using HamstarHelpers.Helpers.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Emitters.Definitions
{
	public partial class HologramDefinition : BaseEmitterDefinition
	{
		public int CurrentFrame { get; internal set; }

		internal int CurrentFrameElapsedTicks = 0;



		////////////////

		public void Draw(int tileX, int tileY, bool isOnScreen)
		{
			var wldPos = new Vector2((tileX << 4) + 8, (tileY << 4) + 8);

			if (this.AnimateHologram(wldPos, false))
			{
				this.DrawHologram(wldPos, false);
			}

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


		///////////

		public void DrawHologram(Vector2 wldPos, bool isUI)
		{
			switch (this.Mode)
			{
				case HologramMode.NPC:
					Main.instance.LoadNPC(this.Type);
					break;
				case HologramMode.Projectile:
					Main.instance.LoadProjectile(this.Type);
					break;
			}

			Texture2D tex = HologramDefinition.GetTexture(this.Mode, this.Type);
			var frameCount = HologramDefinition.GetFrameCount(this.Mode, this.Type);
			var frameHeight = tex.Height / frameCount;
			BeginBatch(tex);
			if (this.ShaderMode == HologramShaderMode.VanillaShader)
			{
				this.VanillaShaderBegin(tex, frameHeight);
			}

			try
			{
				this.DrawHologramRaw(wldPos, isUI, tex, frameHeight);
			}
			finally
			{
				BatcEnd();
			}
		}


		////
		public void BeginBatch(Texture2D tex)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				SamplerState.PointClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone,
				this.ShaderMode == HologramShaderMode.CustomShader ? this.CustomEffectsBegin(tex) : null,
				Main.GameViewMatrix.EffectMatrix
			);
		}

		/// 
		public void VanillaShaderBegin(Texture2D tex, int frameHeight)
		{
			Effect fx = EmittersMod.Instance.ShaderData[this.ShaderType].Shader;
			var effect = fx.CurrentTechnique.Passes[EmittersMod.Instance.passNames[this.ShaderType]];
			var armor = EmittersMod.Instance.ArmorShaderReflections[this.ShaderType];
			Vector4 frame = new Vector4(
				x: 0,
				y: frameHeight * this.CurrentFrame,
				z: tex.Width,
				w: frameHeight
			);
			fx.Parameters["uColor"].SetValue(armor.UColor);
			fx.Parameters["uSaturation"].SetValue(armor.USaturation);
			fx.Parameters["uSecondaryColor"].SetValue(armor.USecondaryColor);
			fx.Parameters["uTime"].SetValue( ( Main.GlobalTime % 3600f) * this.ShaderTime);
			fx.Parameters["uOpacity"].SetValue(armor.UOpacity);
			fx.Parameters["uImageSize0"].SetValue(new Vector2((float)tex.Width, (float)tex.Height));
			fx.Parameters["uSourceRect"].SetValue(frame);
			effect.Apply();
		}


		public Effect CustomEffectsBegin(Texture2D tex)
			{
				Effect fx = EmittersMod.Instance.HologramFX;
				Color color = this.Color;
				color.A = this.Alpha;
				fx.Parameters["TexWidth"].SetValue((float) tex.Width * this.Scale);
				fx.Parameters["TexHeight"].SetValue((float) tex.Height * this.Scale);
				fx.Parameters["RandValue"].SetValue(Main.rand.NextFloat());
				fx.Parameters["Time"].SetValue((Main.GlobalTime % 3600f) / this.ShaderTime);
				fx.Parameters["Frame"].SetValue((float) this.CurrentFrame);
				fx.Parameters["FrameMax"].SetValue((float) Main.npcFrameCount[this.Type]);
				fx.Parameters["UserColor"].SetValue(color.ToVector4());
				return fx;
			}

			public void BatcEnd()
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin();
			}


			////

			public void DrawHologramRaw( Vector2 worldPos, bool isUI, Texture2D tex ,int frameHeight) {
				Color color = this.Color;
				SpriteEffects effects = SpriteEffects.None;
				Vector2 origin = new Vector2( tex.Width, frameHeight ) * 0.5f;
				Vector2 scrPos;
				Rectangle frame = new Rectangle(
					x: 0,
					y: frameHeight * this.CurrentFrame,
					width: tex.Width,
					height: frameHeight
				);
				if( this.WorldLighting ) {
					color = Lighting.GetColor( (int)( worldPos.X / 16f ), (int)( worldPos.Y / 16f ) );
					color = XNAColorHelpers.Mul( color, this.Color );
					color = Lighting.GetColor((int) (worldPos.X / 16f), (int) (worldPos.Y / 16f));
					color = XNAColorHelpers.Mul(color, this.Color);
				}

				color *= (float) this.Alpha / 255f;

				if (isUI)
				{
					scrPos = worldPos - Main.screenPosition;
					//scrPos.X -= npcTexture.Width;
					scrPos.X += this.OffsetX;
					scrPos.Y += this.OffsetY;
					scrPos *= Main.GameZoomTarget;
				}
				else
				{
					scrPos = UIHelpers.ConvertToScreenPosition(worldPos);
					scrPos.X += this.OffsetX;
					scrPos.Y += this.OffsetY;
				}

				if (this.Direction == -1)
				{
					effects = SpriteEffects.FlipHorizontally;
				}

				Main.spriteBatch.Draw(
					texture: tex,
					position: scrPos,
					sourceRectangle: frame,
					color: color,
					rotation: MathHelper.ToRadians(this.Rotation),
					origin: isUI ? default(Vector2) : origin,
					scale: this.Scale * Main.GameZoomTarget,
					effects: effects,
					layerDepth: 1f
				);
			}
	}
}

