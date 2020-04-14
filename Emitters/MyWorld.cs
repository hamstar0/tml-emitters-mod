using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace Emitters {
	public class EmittersWorld : ModWorld {
		private IDictionary<ushort, IDictionary<ushort, EmitterDefinition>> Emitters
			= new Dictionary<ushort, IDictionary<ushort, EmitterDefinition>>();



		////////////////

		public override void Load( TagCompound tag ) {
			this.Emitters.Clear();

			if( !tag.ContainsKey("emitter_count") ) {
				return;
			}

			int count = tag.GetInt( "emitter_count" );

			for( int i=0; i<count; i++ ) {
				ushort tileX = (ushort)tag.GetInt( "emitter_" + i + "_x" );
				ushort tileY = (ushort)tag.GetInt( "emitter_" + i + "_y" );
				string rawDef = tag.GetString( "emitter_" + i );

				var def = JsonConvert.DeserializeObject<EmitterDefinition>( rawDef );

				this.Emitters.Set2D( tileX, tileY, def );
			}
		}

		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "emitter_count", this.Emitters.Count2D() }
			};

			int i = 0;
			foreach( (ushort tileX, IDictionary<ushort, EmitterDefinition> tileYs) in this.Emitters ) {
				foreach( (ushort tileY, EmitterDefinition def) in tileYs ) {
					tag[ "emitter_"+i+"_x" ] = (int)tileX;
					tag[ "emitter_"+i+"_y" ] = (int)tileY;
					tag[ "emitter_"+i ] = JsonConvert.SerializeObject(def);
					i++;
				}
			}

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			try {
				int count = reader.ReadInt32();

				for( int i = 0; i < count; i++ ) {
					ushort tileX = reader.ReadUInt16();
					ushort tileY = reader.ReadUInt16();
					EmitterDefinition def = EmitterDefinition.Read( reader );

					this.Emitters.Set2D( tileX, tileY, def );
				}
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.Emitters.Count2D() );

				foreach( (ushort tileX, IDictionary<ushort, EmitterDefinition> tileYs) in this.Emitters ) {
					foreach( (ushort tileY, EmitterDefinition def) in tileYs ) {
						writer.Write( tileX );
						writer.Write( tileY );
						EmitterDefinition.Write( def, writer );
					}
				}
			} catch { }
		}


		////////////////
		
		public void AddEmitter( EmitterDefinition def, ushort tileX, ushort tileY ) {
			this.Emitters.Set2D( tileX, tileY, def );
		}

		public EmitterDefinition GetEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Get2DOrDefault( tileX, tileY );
		}

		////

		public bool RemoveEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Remove2D( tileX, tileY );
		}


		////////////////

		public override void PostDrawTiles() {
			int leftTile = (int)Main.screenPosition.X >> 4;
			int topTile = (int)Main.screenPosition.Y >> 4;
			int tileWidth = Main.screenWidth >> 4;
			int tileHeight = Main.screenHeight >> 4;
			int maxX = leftTile + tileWidth + 1;
			int maxY = topTile + tileHeight + 1;
			EmitterDefinition def;

			var scrTiles = new Rectangle( leftTile, topTile, maxX, maxY );
			maxX += 8;
			maxY += 8;

			for( ushort x=(ushort)(leftTile - 8); x < maxX; x++ ) {
				for( ushort y=(ushort)(topTile - 8); y < maxY; y++ ) {
					if( this.Emitters.TryGetValue2D(x, y, out def) ) {
						def.Draw( x, y, scrTiles.Contains(x, y) );
					}
				}
			}
		}
	}
}