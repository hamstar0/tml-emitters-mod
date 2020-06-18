using System.IO;
using System.Dynamic;
using Terraria;
using HamstarHelpers.Classes.Errors;


namespace Emitters.Definitions {
	public enum EmitterType {
		Emitter,
		SoundEmitter,
		Hologram
	}




	public abstract class BaseEmitterDefinition {
		public bool IsActivated { get; set; } = true;



		////////////////
		
		public abstract void ReadDynamic( ExpandoObject obj );

		public abstract void Read( BinaryReader reader );

		public abstract void Write( BinaryWriter writer );


		////////////////

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}
	}
}
