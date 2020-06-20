using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem, IBaseEmitterItem<SoundEmitterDefinition> {
		public override bool PreDrawInInventory(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			EmittersMod.Instance.EditorButton.PreDrawInInventory( pos, this.item );
			return base.PreDrawInInventory( sb, pos, frame, drawColor, itemColor, origin, scale );
		}

		////

		public override void PostDrawInInventory(
					SpriteBatch sb,
					Vector2 pos,
					Rectangle frame,
					Color drawColor,
					Color itemColor,
					Vector2 origin,
					float scale ) {
			EmittersMod.Instance.EditorButton.PostDrawInInventory( sb, pos );
		}
	}
}