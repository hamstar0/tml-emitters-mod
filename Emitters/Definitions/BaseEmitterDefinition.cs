using System.IO;
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
		
		public abstract BaseEmitterDefinition Read( BinaryReader reader );

		public abstract void Write( BinaryWriter writer );


		////////////////

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}
	}
}
