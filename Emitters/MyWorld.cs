using System.IO;
using System.Collections.Generic;
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
	}
}