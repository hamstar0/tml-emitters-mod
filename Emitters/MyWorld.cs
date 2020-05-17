using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;


namespace Emitters {
	public partial class EmittersWorld : ModWorld {

		private IDictionary<ushort, IDictionary<ushort, EmitterDefinition>> Emitters
			= new ConcurrentDictionary<ushort, IDictionary<ushort, EmitterDefinition>>();

		private IDictionary<ushort, IDictionary<ushort, SoundEmitterDefinition>> SoundEmitters
			= new ConcurrentDictionary<ushort, IDictionary<ushort, SoundEmitterDefinition>>();


		////////////////

		public override void Initialize() {
			this.Emitters.Clear();
			this.SoundEmitters.Clear();
		}

		////////////////

		public override void Load( TagCompound tag ) {
			this.LoadEmitters( tag );
			this.LoadSoundEmitters( tag );
		}

		private void LoadEmitters( TagCompound tag ) {
			this.Emitters.Clear();
			if( !tag.ContainsKey( "emitter_count" ) ) {
				return;
			}

			int count = tag.GetInt( "emitter_count" );

			try {
				for( int i = 0; i < count; i++ ) {
					ushort tileX = (ushort)tag.GetInt( "emitter_" + i + "_x" );
					ushort tileY = (ushort)tag.GetInt( "emitter_" + i + "_y" );
					string rawDef = tag.GetString( "emitter_" + i );

					var def = JsonConvert.DeserializeObject<EmitterDefinition>( rawDef );
					def.Activate( tag.GetBool( "emitter_" + i + "_on" ) );

					this.Emitters.Set2D( tileX, tileY, def );
				}
			} catch( Exception e ) {
				LogHelpers.Warn( e.ToString() );
			}
		}

		private void LoadSoundEmitters( TagCompound tag ) {
			this.SoundEmitters.Clear();
			if( !tag.ContainsKey( "snd_emitter_count" ) ) {
				return;
			}

			int count = tag.GetInt( "snd_emitter_count" );

			try {
				for( int i = 0; i < count; i++ ) {
					ushort tileX = (ushort)tag.GetInt( "snd_emitter_" + i + "_x" );
					ushort tileY = (ushort)tag.GetInt( "snd_emitter_" + i + "_y" );
					string rawDef = tag.GetString( "snd_emitter_" + i );

					var def = JsonConvert.DeserializeObject<SoundEmitterDefinition>( rawDef );
					def.Activate( tag.GetBool( "snd_emitter_" + i + "_on" ) );

					this.SoundEmitters.Set2D( tileX, tileY, def );
				}
			} catch( Exception e ) {
				LogHelpers.Warn( e.ToString() );
			}
		}


		public override TagCompound Save() {
			var tag = new TagCompound {
				{ "emitter_count", this.Emitters.Count2D() },
				{ "snd_emitter_count",this.SoundEmitters.Count2D() },
			};

			int i = 0;
			foreach( (ushort tileX, IDictionary<ushort, EmitterDefinition> tileYs) in this.Emitters ) {
				foreach( (ushort tileY, EmitterDefinition def) in tileYs ) {
					tag["emitter_" + i + "_x"] = (int)tileX;
					tag["emitter_" + i + "_y"] = (int)tileY;
					tag["emitter_" + i] = (string)JsonConvert.SerializeObject( def );
					tag["emitter_" + i + "_on"] = (bool)def.IsActivated;
					i++;
				}
			}

			i = 0;
			foreach( (ushort tileX, IDictionary<ushort, SoundEmitterDefinition> tileYs) in this.SoundEmitters ) {
				foreach( (ushort tileY, SoundEmitterDefinition def) in tileYs ) {
					tag["snd_emitter_" + i + "_x"] = (int)tileX;
					tag["snd_emitter_" + i + "_y"] = (int)tileY;
					tag["snd_emitter_" + i] = (string)JsonConvert.SerializeObject( def );
					tag["snd_emitter_" + i + "_on"] = (bool)def.IsActivated;
					i++;
				}
			}

			return tag;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			this.Emitters.Clear();
			this.SoundEmitters.Clear();

			try {
				int count = reader.ReadInt32();
				int soundCount = reader.ReadInt32();

				for( int i = 0; i < count; i++ ) {
					ushort tileX = reader.ReadUInt16();
					ushort tileY = reader.ReadUInt16();
					var def = EmitterDefinition.Read( reader );
					this.Emitters.Set2D( tileX, tileY, def );
				}

				for( int i = 0; i < soundCount; i++ ) {
					ushort tileX = reader.ReadUInt16();
					ushort tileY = reader.ReadUInt16();
					var sdef = SoundEmitterDefinition.Read( reader );
					this.SoundEmitters.Set2D( tileX, tileY, sdef );
				}
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( this.Emitters.Count2D() );
				writer.Write( this.SoundEmitters.Count2D() );

				foreach( (ushort tileX, IDictionary<ushort, EmitterDefinition> tileYs) in this.Emitters ) {
					foreach( (ushort tileY, EmitterDefinition def) in tileYs ) {
						writer.Write( tileX );
						writer.Write( tileY );
						EmitterDefinition.Write( def, writer );
					}
				}

				foreach( (ushort tileX, IDictionary<ushort, SoundEmitterDefinition> tileYs) in this.SoundEmitters ) {
					foreach( (ushort tileY, SoundEmitterDefinition sdef) in tileYs ) {
						writer.Write( tileX );
						writer.Write( tileY );
						SoundEmitterDefinition.Write( sdef, writer );
					}
				}
			} catch { }
		}


		////////////////

		public void AddEmitter( EmitterDefinition def, ushort tileX, ushort tileY ) {
			if( ( tileX < 0 || tileX >= Main.maxTilesX ) || ( tileY < 0 || tileY >= Main.maxTilesY ) ) {
				throw new ModHelpersException( "Cannot place emitter outside of world." );
			}
			//Main.NewText( def.ToString() );
			this.Emitters.Set2D( tileX, tileY, def );
		}

		public EmitterDefinition GetEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Get2DOrDefault( tileX, tileY );
		}

		public bool RemoveEmitter( ushort tileX, ushort tileY ) {
			return this.Emitters.Remove2D( tileX, tileY );
		}

		public void AddSoundEmitter( SoundEmitterDefinition def, ushort tileX, ushort tileY ) {
			if( ( tileX < 0 || tileX >= Main.maxTilesX ) || ( tileY < 0 || tileY >= Main.maxTilesY ) ) {
				throw new ModHelpersException( "Cannot place emitter outside of world." );
			}
			//Main.NewText( def.ToString() );
			this.SoundEmitters.Set2D( tileX, tileY, def );
		}

		public SoundEmitterDefinition GetSoundEmitter( ushort tileX, ushort tileY ) {
			return this.SoundEmitters.Get2DOrDefault( tileX, tileY );
		}
		public bool RemoveSoundEmitter( ushort tileX, ushort tileY ) {
			return this.SoundEmitters.Remove2D( tileX, tileY );
		}


		////////////////

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
						if( this.Emitters.TryGetValue2D( x, y, out EmitterDefinition def ) ) {
							def.Draw( x, y, scrTiles.Contains( x, y ) );
						}
						if( this.SoundEmitters.TryGetValue2D( x, y, out SoundEmitterDefinition sdef ) ) {
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