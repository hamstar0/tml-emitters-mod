using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		private bool IsModeBeingSet = false; // TODO Recheck if these need to exist?



		////////////////

		public void SetGoreMode( bool isGoreMode ) {
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			this.ModeDustFlagElem.Selected = !isGoreMode;
			this.ModeGoreFlagElem.Selected = isGoreMode;

			if( !isGoreMode ) {
				if( !ReflectionHelpers.Get( typeof(ModDust), null, "DustCount", out int dustCount) ) {
					throw new ModHelpersException( "Could not get dust count." );
				}

				this.TypeSliderElem.SetRange( 0, dustCount-1 );
			} else {
				if( !ReflectionHelpers.Get( typeof(ModGore), null, "GoreCount", out int goreCount ) ) {
					throw new ModHelpersException( "Could not get gore count." );
				}

				this.TypeSliderElem.SetRange( 0, goreCount-1 );
			}

			this.IsModeBeingSet = false;
		}
	}
}
