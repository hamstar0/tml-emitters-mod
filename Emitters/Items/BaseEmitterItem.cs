using System;
using Terraria;
using Emitters.Definitions;


namespace Emitters.Items {
	public interface IBaseEmitterItem {
		void OpenUI( Item emitterItem );
	}




	public interface IBaseEmitterItem<T> : IBaseEmitterItem where T : BaseEmitterDefinition {
		T Def { get; }


		////////////////

		void SetDefinition( T def );
	}
}