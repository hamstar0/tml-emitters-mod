using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using HamstarHelpers.Helpers.Debug;
using Emitters.Items;
using Emitters.Effects;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public int CurrentFrame { get; internal set; }

		internal int CurrentFrameElapsedTicks = 0;



		////////////////

		public void Draw( SpriteBatch sb, int tileX, int tileY, bool isOnScreen ) {
			var wldPos = new Vector2( ( tileX << 4 ) + 8, ( tileY << 4 ) + 8 );

			if( this.AnimateHologram( wldPos, false ) ) {
				this.DrawHologram( sb, wldPos, false );
			}

			if( isOnScreen && HologramItem.CanViewHolograms( Main.LocalPlayer ) ) {
				this.DrawHologramTile( sb, tileX, tileY );
			}
		}


		///////////

		public void DrawHologram( SpriteBatch sb, Vector2 wldPos, bool isUI ) {
			switch( this.Mode ) {
			case HologramMode.NPC:
				Main.instance.LoadNPC( this.Type );
				break;
			case HologramMode.Projectile:
				Main.instance.LoadProjectile( this.Type );
				break;
			}

			Texture2D tex = HologramDefinition.GetTexture( this.Mode, this.Type );
			var frameCount = HologramDefinition.GetFrameCount( this.Mode, this.Type );
			var frameHeight = tex.Height / frameCount;

			
			try {
				switch( this.ShaderMode ) {
				case HologramShaderMode.Vanilla:
					this.BeginBatch( sb, null );
					this.VanillaShaderBegin( tex, frameHeight );
					break;
				case HologramShaderMode.Custom:
					this.BeginBatch( sb, this.CustomEffectsBegin(tex) );
					this.CustomEffectsBegin( tex );
					break;
				}

				this.DrawHologramRaw( sb, wldPos, isUI, tex, frameHeight );
			} finally {
				if( this.ShaderMode != HologramShaderMode.None ) {
					this.BatchEnd( sb );
				}
			}
		}


		////

		public void BeginBatch( SpriteBatch sb, Effect shader ) {
			sb.End();
			sb.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				SamplerState.PointClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone,
				shader,
				Main.GameViewMatrix.EffectMatrix
			);
		}

		public void BatchEnd( SpriteBatch sb ) {
			sb.End();
			sb.Begin();
		}


		////

		public void VanillaShaderBegin( Texture2D tex, int frameHeight ) {
			Vector4 frame = new Vector4(
				x: 0,
				y: frameHeight * this.CurrentFrame,
				z: tex.Width,
				w: frameHeight
			);
			float cyclePerc = (Main.GlobalTime % this.ShaderTime) / this.ShaderTime;

			var mymod = EmittersMod.Instance;
			ArmorShaderData baseShaderData = mymod.ArmorShaders[ this.ShaderType ];
			EmitterArmorShaderData shaderData = mymod.MyArmorShaders[this.ShaderType];
			Effect fx = baseShaderData.Shader;

			EffectPass effect = fx.CurrentTechnique.Passes[ shaderData.PassName ];

			fx.Parameters["uColor"].SetValue( shaderData.UColor );
			fx.Parameters["uSaturation"].SetValue( shaderData.USaturation );
			fx.Parameters["uSecondaryColor"].SetValue( shaderData.USecondaryColor );
			fx.Parameters["uTime"].SetValue( cyclePerc );
			fx.Parameters["uOpacity"].SetValue( shaderData.UOpacity );
			fx.Parameters["uImageSize0"].SetValue( new Vector2(tex.Width, tex.Height) );
			fx.Parameters["uSourceRect"].SetValue( frame );

			effect.Apply();
		}


		public Effect CustomEffectsBegin( Texture2D tex ) {
			Effect fx = EmittersMod.Instance.HologramFX;
			Color color = this.Color;
			color.A = this.Alpha;
			float cyclePerc = (Main.GlobalTime % this.ShaderTime) / this.ShaderTime;

			fx.Parameters["TexWidth"].SetValue( (float)tex.Width * this.Scale );
			fx.Parameters["TexHeight"].SetValue( (float)tex.Height * this.Scale );
			fx.Parameters["RandValue"].SetValue( Main.rand.NextFloat() );
			fx.Parameters["CyclePercent"].SetValue( cyclePerc );
			fx.Parameters["Frame"].SetValue( (float)this.CurrentFrame );
			fx.Parameters["FrameMax"].SetValue( (float)Main.npcFrameCount[this.Type] );
			fx.Parameters["UserColor"].SetValue( color.ToVector4() );
			fx.Parameters["WaveScale"].SetValue( (float)color.A / 255f );	// TODO ?

			return fx;
		}
	}
}

