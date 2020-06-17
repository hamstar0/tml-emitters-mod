using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Emitters.Definitions;


namespace Emitters {
	public partial class EmittersWorld : ModWorld {
		private IDictionary<ushort, IDictionary<ushort, EmitterDefinition>> Emitters
			= new ConcurrentDictionary<ushort, IDictionary<ushort, EmitterDefinition>>();

		private IDictionary<ushort, IDictionary<ushort, SoundEmitterDefinition>> SoundEmitters
			= new ConcurrentDictionary<ushort, IDictionary<ushort, SoundEmitterDefinition>>();
		
		private IDictionary<ushort, IDictionary<ushort, HologramDefinition>> Holograms
			= new ConcurrentDictionary<ushort, IDictionary<ushort, HologramDefinition>>();



		////////////////

		public override void Initialize() {
			this.Emitters.Clear();
			this.SoundEmitters.Clear();
			this.Holograms.Clear();
		}


		////////////////

		public override void Load( TagCompound tag ) {
			this.LoadEmitterType( this.Emitters, tag, "emitter" );
			this.LoadEmitterType( this.SoundEmitters, tag, "snd_emitter" );

			if( !this.LoadEmitterType( this.Holograms, tag, "hologram" ) ) {
				var oldHolos = new Dictionary<ushort, IDictionary<ushort, OldHologramDefinition>>();

				if( this.LoadEmitterType( oldHolos, tag, "hologram" ) ) {
					this.LoadOldHolograms( oldHolos );
				}
			}
		}
		
		public void LoadOldHolograms( IDictionary<ushort, IDictionary<ushort, OldHologramDefinition>> oldHolos ) {
			this.Holograms.Clear();

			foreach( (ushort tileX, IDictionary<ushort, OldHologramDefinition> tileYs) in oldHolos ) {
				foreach( (ushort tileY, OldHologramDefinition oldHoloDef) in tileYs ) {
					this.Holograms.Set2D( tileX, tileY, oldHoloDef.ConvertToNew() );
				}
			}
		}

		////

		public override TagCompound Save() {
			var tag = new TagCompound();

			this.SaveEmitterType( tag, "emitter", this.Emitters );
			this.SaveEmitterType( tag, "snd_emitter", this.SoundEmitters );
			this.SaveEmitterType( tag, "hologram", this.Holograms );

			return tag;
		}


		////////////////

		private bool LoadEmitterType<T>(
					IDictionary<ushort, IDictionary<ushort, T>> dict2d,
					TagCompound tag,
					string prefix )
					where T : BaseEmitterDefinition {
			dict2d.Clear();

			if( !tag.ContainsKey( prefix + "_count" ) ) {
				return true;
			}

			bool success = true;
			int count = tag.GetInt( prefix + "_count" );

			try {
				for( int i = 0; i < count; i++ ) {
					ushort tileX = (ushort)tag.GetInt( prefix + "_" + i + "_x" );
					ushort tileY = (ushort)tag.GetInt( prefix + "_" + i + "_y" );
					string rawDef = tag.GetString( prefix + "_" + i );

					var def = JsonConvert.DeserializeObject<T>( rawDef );
					def.Activate( tag.GetBool( prefix + "_" + i + "_on" ) );

					dict2d.Set2D( tileX, tileY, def );

					if( EmittersConfig.Instance.DebugModeInfo ) {
						LogHelpers.Log( "Loaded " + prefix + " " + i + " of " + count + " at " + tileX + ", " + tileY );
					}
				}
			} catch( Exception e ) {
				LogHelpers.Warn( e.ToString() );
				success = false;
			}

			return success;
		}

		private void SaveEmitterType<T>(
					TagCompound tag,
					string prefix,
					IDictionary<ushort, IDictionary<ushort, T>> dict2d )
					where T : BaseEmitterDefinition {
			int count = dict2d.Count2D();

			int i = 0;
			foreach( (ushort tileX, IDictionary<ushort, T> tileYs) in dict2d ) {
				foreach( (ushort tileY, T def) in tileYs ) {
					if( def == null ) {
						if( EmittersConfig.Instance.DebugModeInfo ) {
							LogHelpers.Log( "Could not save "+prefix+" at "+tileX+", "+tileY );
						}
						count--;
						continue;
					}

					tag[prefix+"_"+i+"_x"] = (int)tileX;
					tag[prefix+"_"+i+"_y"] = (int)tileY;
					tag[prefix+"_"+i] = (string)JsonConvert.SerializeObject( def );
					tag[prefix+"_"+i+"_on"] = (bool)def.IsActivated;
					i++;

					if( EmittersConfig.Instance.DebugModeInfo ) {
						LogHelpers.Log( "Saved "+prefix+" "+i+" of "+count+" at "+tileX+", "+tileY );
					}
				}
			}

			tag[prefix + "_count"] = count;
		}


		////////////////

		public override void NetReceive( BinaryReader reader ) {
			//this.Emitters.Clear();
			//this.SoundEmitters.Clear();
			//this.Holograms.Clear();

			try {
				int emitCount = reader.ReadInt32();
				int sndEmitCount = reader.ReadInt32();
				int hologramCount = reader.ReadInt32();

				if( EmittersConfig.Instance.DebugModeNetInfo ) {
					Main.NewText( "Receiving from server: "
						+emitCount+" emitters, "
						+sndEmitCount+" sound emitters, "
						+hologramCount+" holograms." );
					LogHelpers.Log( "Receiving from server: "
						+emitCount+" emitters, "
						+sndEmitCount+" sound emitters, "
						+hologramCount+" holograms." );
				}

				this.NetReceiveEmitterType( emitCount, reader, this.Emitters );
				this.NetReceiveEmitterType( sndEmitCount, reader, this.SoundEmitters );
				this.NetReceiveEmitterType( hologramCount, reader, this.Holograms );
			} catch { }
		}

		public override void NetSend( BinaryWriter writer ) {
			try {
				writer.Write( (int)this.Emitters.Count2D() );
				writer.Write( (int)this.SoundEmitters.Count2D() );
				writer.Write( (int)this.Holograms.Count2D() );

				if( EmittersConfig.Instance.DebugModeNetInfo ) {
					LogHelpers.Log( "Sending from server: "
						+ this.Emitters.Count2D() + " emitters, "
						+ this.SoundEmitters.Count2D() + " sound emitters, "
						+ this.Holograms.Count2D() + " holograms." );
				}

				this.NetSendEmitterType( writer, this.Emitters );
				this.NetSendEmitterType( writer, this.SoundEmitters );
				this.NetSendEmitterType( writer, this.Holograms );
			} catch { }
		}

		////

		private void NetReceiveEmitterType<T>(
					int count,
					BinaryReader reader,
					IDictionary<ushort, IDictionary<ushort, T>> dict2d )
					where T : BaseEmitterDefinition {
			//dict2d.Clear();

			for( int i = 0; i < count; i++ ) {
				ushort tileX = reader.ReadUInt16();
				ushort tileY = reader.ReadUInt16();
				var def = Activator.CreateInstance<T>();
				def.Read( reader );

				if( EmittersConfig.Instance.DebugModeNetInfo ) {
					LogHelpers.Log( " Receiving "+typeof(T).Name+" from server at "+tileX+", "+tileY
						+":\n  "+def.ToString() );
				}

				dict2d.Set2D( tileX, tileY, def );
			}
		}

		private void NetSendEmitterType<T>(
					BinaryWriter writer,
					IDictionary<ushort, IDictionary<ushort, T>> dict2d )
					where T : BaseEmitterDefinition {
			try {
				foreach( (ushort tileX, IDictionary<ushort, T> tileYs) in dict2d ) {
					foreach( (ushort tileY, T def) in tileYs ) {
						if( EmittersConfig.Instance.DebugModeNetInfo ) {
							LogHelpers.Log( " Sending "+typeof(T).Name+" from server at "+tileX+", "+tileY
								+ ":\n  " + def.ToString() );
						}

						writer.Write( tileX );
						writer.Write( tileY );
						def.Write( writer );
					}
				}
			} catch { }
		}
	}
}