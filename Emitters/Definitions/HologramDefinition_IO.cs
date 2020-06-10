using System.IO;
using Microsoft.Xna.Framework;
using HamstarHelpers.Classes.Errors;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public override void Read( BinaryReader reader ) {
			this.Mode = (HologramMode)reader.ReadUInt16();
			this.Type = (int)reader.ReadUInt16();
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
