using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public override BaseEmitterDefinition Read( BinaryReader reader ) {
			return new HologramDefinition(
				mode: (HologramMode)reader.ReadUInt16(),
				type: (int)reader.ReadUInt16(),
				scale: (float)reader.ReadSingle(),
				color: new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				alpha: (byte)reader.ReadByte(),
				direction: (int)reader.ReadUInt16(),
				rotation: (float)reader.ReadSingle(),
				offsetX: (int)reader.ReadUInt16(),
				offsetY: (int)reader.ReadUInt16(),
				frameStart: (int)reader.ReadUInt16(),
				frameEnd: (int)reader.ReadUInt16(),
				frameRateTicks: (int)reader.ReadUInt16(),
				worldLight: (bool)reader.ReadBoolean(),
				shaderMode: (HologramShaderMode)reader.ReadUInt16(),
				shaderTime: (float)reader.ReadSingle(),
				shaderType: (int)reader.ReadUInt16(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (ushort)this.Mode );
			writer.Write( (ushort)this.Type );
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
