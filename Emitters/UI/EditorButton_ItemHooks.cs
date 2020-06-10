using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Emitters.Items;


namespace Emitters.UI {
	public partial class EditorButton {
		public void PreDrawInInventory( Vector2 pos, Item item ) {
			var newPos = new Vector2( pos.X - 4f, pos.Y - 16f );
			var baseModItem = item.modItem as IBaseEmitterItem;

			if( baseModItem != null && this.CanPressEditorButton(newPos) ) {
				this.ReadyEditorButtonPress( () => baseModItem.OpenUI(item) );
			}
		}

		public void PostDrawInInventory( SpriteBatch sb, Vector2 pos ) {
			var newPos = new Vector2( pos.X - 4f, pos.Y - 16f );
			var mouseRect = new Rectangle(
				x: (int)newPos.X,
				y: (int)newPos.Y,
				width: this.EditorButtonTex.Width,
				height: this.EditorButtonTex.Height
			);

			if( mouseRect.Contains( Main.mouseX, Main.mouseY ) ) {
				this.DrawEditorButton( sb, newPos );
			}
		}
	}
}
