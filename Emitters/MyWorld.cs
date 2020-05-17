using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
				writer.Write( (int)this.Emitters.Count2D() );
				writer.Write( (int)this.SoundEmitters.Count2D() );

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
	}
}