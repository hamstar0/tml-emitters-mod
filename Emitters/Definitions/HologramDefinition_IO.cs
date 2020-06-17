using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using Microsoft.Xna.Framework;
using HamstarHelpers.Classes.Errors;
using Terraria.ModLoader.Config;
using Newtonsoft.Json;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public void Read( dynamic obj ) {
			ExpandoObject eo = JsonConvert.DeserializeObject( JsonConvert.SerializeObject(obj) );
			var eod = (IDictionary<string, object>)eo;

			if( eod.ContainsKey("Mode") ) {

			}
			this.Mode = (HologramMode)obj.Mode;
			this.TypeDef = NPCDefinition.FromString( rawType );
			this.Scale = (float)reader.ReadSingle();
			this.Color = new Color(
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte()
			);
			this.Alpha = (byte)reader.ReadByte();
			this.Direction = (int)reader.ReadUInt16();
			this.Rotation = (float)reader.ReadSingle();
			this.OffsetX = (int)reader.ReadUInt16();
			this.OffsetY = (int)reader.ReadUInt16();
			this.FrameStart = (int)reader.ReadUInt16();
			this.FrameEnd = (int)reader.ReadUInt16();
			this.FrameRateTicks = (int)reader.ReadUInt16();
			this.WorldLighting = (bool)reader.ReadBoolean();
			this.ShaderMode = (HologramShaderMode)reader.ReadUInt16();
			this.ShaderTime = (float)reader.ReadSingle();
			this.ShaderType = (int)reader.ReadUInt16();
			this.IsActivated = (bool)reader.ReadBoolean();
		}

		////

		public override void Read( BinaryReader reader ) {
			this.Mode = (HologramMode)reader.ReadUInt16();

			string rawType = reader.ReadString();

			switch( this.Mode ) {
			case HologramMode.NPC:
				this.TypeDef = NPCDefinition.FromString( rawType );
				break;
			case HologramMode.Item:
				this.TypeDef = NPCDefinition.FromString( rawType );
				break;
			case HologramMode.Projectile:
				this.TypeDef = NPCDefinition.FromString( rawType );
				break;
			default:
				throw new NotImplementedException( "Invalid mode." );
			}

			this.Scale = (float)reader.ReadSingle();
			this.Color = new Color(
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte()
			);
			this.Alpha = (byte)reader.ReadByte();
			this.Direction = (int)reader.ReadUInt16();
			this.Rotation = (float)reader.ReadSingle();
			this.OffsetX = (int)reader.ReadUInt16();
			this.OffsetY = (int)reader.ReadUInt16();
			this.FrameStart = (int)reader.ReadUInt16();
			this.FrameEnd = (int)reader.ReadUInt16();
			this.FrameRateTicks = (int)reader.ReadUInt16();
			this.WorldLighting = (bool)reader.ReadBoolean();
			this.ShaderMode = (HologramShaderMode)reader.ReadUInt16();
			this.ShaderTime = (float)reader.ReadSingle();
			this.ShaderType = (int)reader.ReadUInt16();
			this.IsActivated = (bool)reader.ReadBoolean();
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (ushort)this.Mode );
			writer.Write( (string)this.TypeDef.ToString() );
			writer.Write( (float)this.Scale );
			writer.Write( (byte)this.Color.R );
			writer.Write( (byte)this.Color.G );
			writer.Write( (byte)this.Color.B );
			writer.Write( (byte)this.Alpha );
			writer.Write( (ushort)this.Direction );
			writer.Write( (float)this.Rotation );
			writer.Write( (ushort)this.OffsetX );
			writer.Write( (ushort)this.OffsetY );
			writer.Write( (ushort)this.FrameStart );
			writer.Write( (ushort)this.FrameEnd );
			writer.Write( (ushort)this.FrameRateTicks );
			writer.Write( (bool)this.WorldLighting );
			writer.Write( (ushort)this.ShaderMode );
			writer.Write( (float)this.ShaderTime );
			writer.Write( (ushort)this.ShaderType );
			writer.Write( (bool)this.IsActivated );
		}
	}
}
