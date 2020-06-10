using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
		public override void Read( BinaryReader reader ) {
			this.IsGoreMode = (bool)reader.ReadBoolean();
			this.Type = (int)reader.ReadUInt16();
			this.Scale = (float)reader.ReadSingle();
			this.Delay = (int)reader.ReadUInt16();
			this.SpeedX = (float)reader.ReadSingle();
			this.SpeedY = (float)reader.ReadSingle();
			this.Color = new Color(
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte(),
				(byte)reader.ReadByte()
			);
			this.Transparency = (byte)reader.ReadByte();
			this.Scatter = (float)reader.ReadSingle();
			this.HasGravity = (bool)reader.ReadBoolean();
			this.HasLight = (bool)reader.ReadBoolean();
			this.IsActivated = (bool)reader.ReadBoolean();
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (bool)this.IsGoreMode );
			writer.Write( (ushort)this.Type );
			writer.Write( (float)this.Scale );
			writer.Write( (ushort)this.Delay );
			writer.Write( (float)this.SpeedX );
			writer.Write( (float)this.SpeedY );
			writer.Write( (byte)this.Color.R );
			writer.Write( (byte)this.Color.G );
			writer.Write( (byte)this.Color.B );
			writer.Write( (byte)this.Transparency );
			writer.Write( (float)this.Scatter );
			writer.Write( (bool)this.HasGravity );
			writer.Write( (bool)this.HasLight );
			writer.Write( (bool)this.IsActivated );
		}
	}
}
