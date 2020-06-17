using System;
using System.Collections.Generic;
using System.IO;
using System.Dynamic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public override void ReadDynamic( ExpandoObject obj ) {
			var objDict = (IDictionary<string, object>)obj;

			if( objDict.ContainsKey("Mode") ) {
				this.Mode = (HologramMode)(long)objDict["Mode"];
			} else {
				this.Mode = HologramMode.NPC;
			}

			this.Type = (int)(long)objDict["Type"];
			this.Scale = (float)(double)objDict["Scale"];
			this.Color = JsonConvert.DeserializeObject<Color>( "\""+objDict["Color"]+"\"" );
			this.Alpha = (byte)(long)objDict["Alpha"];
			this.Direction = (int)(long)objDict["Direction"];
			this.Rotation = (float)(double)objDict["Rotation"];
			this.OffsetX = (int)(long)objDict["OffsetX"];
			this.OffsetY = (int)(long)objDict["OffsetY"];
			this.FrameStart = (int)(long)objDict["FrameStart"];
			this.FrameEnd = (int)(long)objDict["FrameEnd"];
			this.FrameRateTicks = (int)(long)objDict["FrameRateTicks"];
			this.WorldLighting = (bool)objDict["WorldLighting"];

			if( objDict.ContainsKey("ShaderMode") ) {
				this.ShaderMode = (HologramShaderMode)(long)objDict["ShaderMode"];
				this.ShaderTime = (float)(double)objDict["ShaderTime"];
				this.ShaderType = (int)(long)objDict["ShaderType"];
			} else if( objDict.ContainsKey("CrtEffect") ) {
				this.ShaderMode = (bool)objDict["CrtEffect"]
					? HologramShaderMode.None
					: HologramShaderMode.Custom;
				this.ShaderTime = 1f;
				this.ShaderType = 0;
			} else {
				this.ShaderMode = HologramShaderMode.None;
				this.ShaderTime = 1f;
				this.ShaderType = 0;
			}

			this.IsActivated = (bool)objDict["IsActivated"];

			//Color color = this.Color;
			//color.A = this.Alpha;
			//this.Color = color;
		}

		////

		public override void Read( BinaryReader reader ) {
			this.Mode = (HologramMode)reader.ReadUInt16();

			this.Type = reader.ReadInt32();
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
			writer.Write( (int)this.Type );
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
