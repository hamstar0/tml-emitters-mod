using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.UI;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Emitters.UI {
	public partial class EditorButton {
		private Texture2D EditorButtonTex;
		private Action EditorButtonAction = null;



		////////////////

		public EditorButton() {
			ReflectionHelpers.Get( typeof(UICommon), null, "ButtonConfigTexture", out this.EditorButtonTex );
		}


		////////////////

		public bool CanPressEditorButton( Vector2 scrPos ) {
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

		/// <summary></summary>
		/// <returns>`true` if mouse interaction has occurred.</returns>
		public bool PressEditorButtonIfInteracting() {
			if( this.EditorButtonAction == null ) {
				return false;
			}

			bool isMouse = Main.mouseLeft && Main.mouseLeftRelease;
			if( isMouse ) {
				this.EditorButtonAction.Invoke();
			}

			this.EditorButtonAction = null;

			return isMouse;
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
