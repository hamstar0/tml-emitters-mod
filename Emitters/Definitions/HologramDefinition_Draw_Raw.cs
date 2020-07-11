using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.XNA;
using HamstarHelpers.Helpers.UI;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public void DrawHologramRaw( SpriteBatch sb, Vector2 worldPos, bool isUI, Texture2D tex, int frameHeight ) {
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
			}

			color *= (float)this.Alpha / 255f;

			scrPos = worldPos - Main.screenPosition;
			scrPos.X += this.OffsetX;
			scrPos.Y += this.OffsetY;
			scrPos = UIZoomHelpers.ApplyZoomFromScreenCenter( scrPos, isUI ? (bool?)true : null, false, null, null );

			if( this.Direction == -1 ) {
				effects = SpriteEffects.FlipHorizontally;
			}

			try {
				sb.Draw(
					texture: tex,
					position: scrPos,
					sourceRectangle: frame,
					color: color,
					rotation: MathHelper.ToRadians( this.Rotation ),
					origin: isUI ? default(Vector2) : origin,
					scale: this.Scale * Main.GameZoomTarget,
					effects: effects,
					layerDepth: 1f
				);
			} catch( Exception e ) {
				LogHelpers.Warn( e.ToString() );
				throw;
			}
		}
	}
}

