using System;
using System.IO;
using System.Dynamic;
using Terraria;
using HamstarHelpers.Classes.Errors;
using Emitters.Items;


namespace Emitters.Definitions {
	public enum EmitterType {
		Emitter,
		SoundEmitter,
		Hologram
	}



	
	public abstract class BaseEmitterDefinition {
		public static T CreateOrGetDefForItem<T>( Item item )
					where T : BaseEmitterDefinition {
			var modItem = item.modItem;
			if( modItem == null ) {
				throw new ModHelpersException( "Not an emitter or mod item." );
			}

			var myitem = modItem as IBaseEmitterItem<T>;
			if( myitem == null ) {
				myitem.SetDefinition( (T)Activator.CreateInstance(typeof(T)) );
			}

			return myitem.Def;
		}



		////////////////

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
