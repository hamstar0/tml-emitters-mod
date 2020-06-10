using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Helpers.Debug;


namespace Emitters {
	public partial class EmittersMod : Mod {
		public bool CanPressEditorButton( Vector2 scrPos ) {
			//if( !Main.mouseLeft || !Main.mouseLeftRelease ) {
			//	return false;
			//}

			int minX = (int)scrPos.X;
			int minY = (int)scrPos.Y;
			int maxX = minX + this.EditorButtonTex.Width;
			int maxY = minY + this.EditorButtonTex.Height;

			return Main.mouseX >= minX && Main.mouseX < maxX && Main.mouseY >= minY && Main.mouseY < maxY;
		}

		////
		
		public void ReadyEditorButtonPress( Action func ) {
			this.EditorButtonAction = func;
		}

		public bool RunEditorButtonIfInteracting() {
			if( this.EditorButtonAction == null ) {
				return false;
			}

			if( Main.mouseLeft && Main.mouseLeftRelease ) {
				this.EditorButtonAction.Invoke();
			}
			this.EditorButtonAction = null;

			return true;
		}


		////////////////

		public void DrawEditorButton( SpriteBatch sb, Vector2 scrPos ) {
			sb.Draw(
				texture: this.EditorButtonTex,
				position: scrPos,
				color: Color.White
			);
		}
	}
}