using System.IO;
using Microsoft.Xna.Framework;


namespace Emitters.Definitions {
	public partial class EmitterDefinition : BaseEmitterDefinition {
		public override BaseEmitterDefinition Read( BinaryReader reader ) {
			return new EmitterDefinition(
				isGoreMode: (bool)reader.ReadBoolean(),
				type: (int)reader.ReadUInt16(),
				scale: (float)reader.ReadSingle(),
				delay: (int)reader.ReadInt16(),
				speedX: (float)reader.ReadSingle(),
				speedY: (float)reader.ReadSingle(),
				color: new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				transparency: (byte)reader.ReadByte(),
				scatter: (float)reader.ReadSingle(),
				hasGravity: (bool)reader.ReadBoolean(),
				hasLight: (bool)reader.ReadBoolean(),
				isActivated: (bool)reader.ReadBoolean()
			);
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
