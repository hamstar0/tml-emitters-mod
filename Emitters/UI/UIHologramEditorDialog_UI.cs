using Emitters.Definitions;
using HamstarHelpers.Classes.UI.Elements;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private bool IsModeBeingSet = false;	// TODO Recheck if these need to exist?
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

			this.RecalculateMe();

			this.IsTabBeingSet = false;
		}

		////////////////

		public void UISetHologramMode( HologramMode mode ) {
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			if( mode != HologramMode.NPC && this.NpcModeChoice.Selected ) {
				this.NpcModeChoice.Selected = false;
				this.NpcModeChoice.Recalculate();
			}
			if( mode != HologramMode.Item && this.ItemModeChoice.Selected ) {
				this.ItemModeChoice.Selected = false;
				this.ItemModeChoice.Recalculate();
			}
			if( mode != HologramMode.Projectile && this.ProjectileModeChoice.Selected ) {
				this.ProjectileModeChoice.Selected = false;
				this.ProjectileModeChoice.Recalculate();
			}

			this.CurrentMode = mode;

			this.IsModeBeingSet = false;
		}
		public void UISetHologramShaderMode(HologramUIShaderMode mode)
		{
			if( this.IsModeBeingSet ) { return; }
			this.IsModeBeingSet = true;

			if( mode != HologramUIShaderMode.VanillaMode && this.VanillaShadersCheckbox.Selected ) {
				this.VanillaShadersCheckbox.Selected = false;
				this.VanillaShadersCheckbox.Recalculate();
			}
			if( mode != HologramUIShaderMode.CustomMode && this.CustomShadersCheckbox.Selected ) {
				this.CustomShadersCheckbox.Selected = false;
				this.CustomShadersCheckbox.Recalculate();
			}
			if( mode != HologramUIShaderMode.NoShader && this.NoShaderCheckbox.Selected ) {
				this.NoShaderCheckbox.Selected = false;
				this.NoShaderCheckbox.Recalculate();
			}
			this.CurrentsShaderMode = mode;
			this.IsModeBeingSet = false;
		}
		////////////////

	}
}
