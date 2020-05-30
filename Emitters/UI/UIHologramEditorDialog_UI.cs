using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Services.Timers;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private bool IsModeBeingSet = false;	// TODO Recheck if these need to exist?
		private bool IsTabBeingSet = false; // TODO Recheck if these need to exist?



		////////////////

		public void SwitchTab( HologramUITab tab ) {
			if( this.IsTabBeingSet ) { return; }
			this.IsTabBeingSet = true;

			float tabHeight = 0f;

			this.MainTabElem.Hide();
			this.ColorTabElem.Hide();
			this.ShaderTabElem.Hide();

			switch( tab ) {
			case HologramUITab.MainSettings:
				tabHeight = this.MainTabElem.Height.Pixels;
				this.MainTabElem.Show();
				break;
			case HologramUITab.ColorSettings:
				tabHeight = this.ColorTabElem.Height.Pixels;
				this.ColorTabElem.Show();
				break;
			case HologramUITab.ShaderSettings:
				tabHeight = this.ShaderTabElem.Height.Pixels;
				this.ShaderTabElem.Show();
				break;
			}

			this.OuterContainer.Height.Set( this.TabStartHeight + tabHeight, 0f );
			this.RecalculateMe();

			// I'll look into why this doesn't recalc right away, later:
			Timers.SetTimer( "HologramDialogTabs", 2, true, () => {
				this.RecalculateMe();
				return false;
			} );

			this.IsTabBeingSet = false;
		}


		////////////////

		public void SetHologramMode( int hologramMode ) {
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			switch( hologramMode ) {
			case 1:
				this.TypeSliderElem.SetRange( 0, NPCLoader.NPCCount );
				break;
			case 2:
				this.TypeSliderElem.SetRange( 0, ItemLoader.ItemCount - 1 );
				break;
			case 3:
				this.TypeSliderElem.SetRange( 0, ProjectileLoader.ProjectileCount - 1 );
				break;
			default:
				break;
			}

			this.IsModeBeingSet = false;
		}
	}
}
