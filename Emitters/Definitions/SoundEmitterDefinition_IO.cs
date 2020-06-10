using System.IO;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		public override void Read( BinaryReader reader ) {
			this.Type = (int)reader.ReadUInt16();
			this.Style = (int)reader.ReadUInt16();
			this.Volume = (float)reader.ReadSingle();
			this.Pitch = (float)reader.ReadSingle();
			this.Delay = (int)reader.ReadUInt16();
			this.IsActivated = (bool)reader.ReadBoolean();
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (ushort)this.Type );
			writer.Write( (ushort)this.Style );
			writer.Write( (float)this.Volume );
			writer.Write( (float)this.Pitch );
			writer.Write( (ushort)this.Delay );
			writer.Write( (bool)this.IsActivated );
		}
	}
}
