using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Emitters.Definitions;


namespace Emitters {
	public partial class EmittersWorld : ModWorld {
		public override void PostDrawTiles() {
			int leftTile = (int)Main.screenPosition.X >> 4;
			int topTile = (int)Main.screenPosition.Y >> 4;
			int tileWidth = Main.screenWidth >> 4;
			int tileHeight = Main.screenHeight >> 4;
			int maxX = leftTile + tileWidth + 1;
			int maxY = topTile + tileHeight + 1;

			var scrTiles = new Rectangle( leftTile, topTile, maxX, maxY );
			maxX += 8;
			maxY += 8;

			Main.spriteBatch.Begin();

			try {
				for( ushort x = (ushort)( leftTile - 8 ); x < maxX; x++ ) {
					for( ushort y = (ushort)( topTile - 8 ); y < maxY; y++ ) {
						EmitterDefinition def;
						if( this.Emitters.TryGetValue2D( x, y, out def ) ) {
							def.Draw( x, y, scrTiles.Contains( x, y ) );
						}

						SoundEmitterDefinition sdef;
						if( this.SoundEmitters.TryGetValue2D( x, y, out sdef ) ) {
							sdef.Draw( x, y, scrTiles.Contains( x, y ) );
						}
					}
				}
			} finally {
				Main.spriteBatch.End();
			}
		}
	}
}