using System.IO;


namespace Emitters.Definitions {
	public partial class SoundEmitterDefinition : BaseEmitterDefinition {
		public override BaseEmitterDefinition Read( BinaryReader reader ) {
			return new SoundEmitterDefinition(
				type: (int)reader.ReadUInt16(),
				style: (int)reader.ReadUInt16(),
				volume: (float)reader.ReadSingle(),
				pitch: (float)reader.ReadSingle(),
				delay: (int)reader.ReadInt16(),
				isActivated: (bool)reader.ReadBoolean()
			);
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
