using Emitters.Definitions;
using HamstarHelpers.Classes.UI.Elements;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private bool IsModeBeingSet = false;    // TODO Recheck if these need to exist?
		private bool IsTabBeingSet = false; // TODO Recheck if these need to exist?



		////////////////

		public void SwitchTab( HologramUITab tab ) {
			if( this.IsTabBeingSet ) { return; }
			this.IsTabBeingSet = true;

			this.MainTabContainer.Hide();
			this.ColorTabContainer.Hide();
			this.ShaderTabContainer.Hide();
			this.MainTabContainer.Disable();
			this.ColorTabContainer.Disable();
			this.ShaderTabContainer.Disable();
			this.MainTabContainer.Height.Set( 0f, 0f );
			this.ColorTabContainer.Height.Set( 0f, 0f );
			this.ShaderTabContainer.Height.Set( 0f, 0f );

			switch( tab ) {
			case HologramUITab.Main:
				this.MainTabContainer.Height.Set( this.MainTabHeight, 0f );
				this.MainTabContainer.Show();
				this.MainTabContainer.Enable();
				break;
			case HologramUITab.Color:
				this.ColorTabContainer.Height.Set( this.ColorTabHeight, 0f );
				this.ColorTabContainer.Show();
				this.ColorTabContainer.Enable();
				break;
			case HologramUITab.Shader:
				this.ShaderTabContainer.Height.Set( this.ShaderTabHeight, 0f );
				this.ShaderTabContainer.Show();
				this.ShaderTabContainer.Enable();
				break;
			}

			this.CurrentTab = tab;

			this.Recalculate();

			this.IsTabBeingSet = false;
		}


		////////////////

		public void SetHologramMode( HologramMode mode ) {
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			if( mode != HologramMode.NPC && this.ModeNpcChoice.Selected ) {
				this.ModeNpcChoice.Selected = false;
				this.ModeNpcChoice.Recalculate();
			}
			if( mode != HologramMode.Item && this.ModeItemChoice.Selected ) {
				this.ModeItemChoice.Selected = false;
				this.ModeItemChoice.Recalculate();
			}
			if( mode != HologramMode.Projectile && this.ModeProjectileChoice.Selected ) {
				this.ModeProjectileChoice.Selected = false;
				this.ModeProjectileChoice.Recalculate();
			}
			if( mode != HologramMode.Gore && this.ModeGoreChoice.Selected ) {
				this.ModeGoreChoice.Selected = false;
				this.ModeGoreChoice.Recalculate();
			}


			this.CurrentMode = mode;

			this.IsModeBeingSet = false;
		}

		public void SetHologramShaderMode( HologramShaderMode mode ) {
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			if( mode != HologramShaderMode.Vanilla && this.ShaderVanillaChoice.Selected ) {
				this.ShaderVanillaChoice.Selected = false;
				this.ShaderVanillaChoice.Recalculate();
			}
			if( mode != HologramShaderMode.Custom && this.ShaderCustomChoice.Selected ) {
				this.ShaderCustomChoice.Selected = false;
				this.ShaderCustomChoice.Recalculate();
			}
			if( mode != HologramShaderMode.None && this.ShaderNoneChoice.Selected ) {
				this.ShaderNoneChoice.Selected = false;
				this.ShaderNoneChoice.Recalculate();
			}

			this.CurrentsShaderMode = mode;

			this.IsModeBeingSet = false;
		}
	}
}
