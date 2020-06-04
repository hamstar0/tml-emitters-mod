using HamstarHelpers.Classes.UI.Elements;


namespace Emitters.UI
{
	partial class UIHologramEditorDialog : UIDialog {
		private bool IsModeBeingSet = false;	// TODO Recheck if these need to exist?
		private bool IsTabBeingSet = false; // TODO Recheck if these need to exist?



		////////////////

		public void SwitchTab( HologramUITab tab ) {
			if( this.IsTabBeingSet ) { return; }
			this.IsTabBeingSet = true;

			float tabHeight = 0f;

			this.InnerContainer.RemoveChild(MainTabElem);			
			this.InnerContainer.RemoveChild(ColorTabElem);
			this.InnerContainer.RemoveChild(ShaderTabElem);
			this.InnerContainer.RemoveChild(ApplyButton);
			switch( tab ) {
			case HologramUITab.MainSettings:
				tabHeight = this.MainTabElem.Height.Pixels;
				this.InnerContainer.Append(this.HologramUIContainers[0]);
				break;
			case HologramUITab.ColorSettings:
				tabHeight = this.ColorTabElem.Height.Pixels;
				this.InnerContainer.Append(this.HologramUIContainers[1]);
				break;
			case HologramUITab.ShaderSettings:
				tabHeight = this.ShaderTabElem.Height.Pixels;
				this.InnerContainer.Append(this.HologramUIContainers[2]);
				break;
			}
			this.InnerContainer.Append(this.HologramUIContainers[3]);
			this.OuterContainer.Top.Set( this.TabStartHeight - 28f, 0f );
			this.OuterContainer.Height.Set( this.TabStartHeight + tabHeight + 32f, 0f );
			this.RecalculateMe();
			this.IsTabBeingSet = false;
		}


		////////////////

		//////////////////

		public void UISetHologramMode(HologramUIMode mode)
		{
			if( this.IsModeBeingSet ) { return; }

			NpcModeCheckbox.Selected = false;
			ItemModeCheckbox.Selected = false;
			ProjectileModeCheckbox.Selected = false;

			this.IsModeBeingSet = true;
			switch (mode)
			{
				case HologramUIMode.NPCMode:
					NpcModeCheckbox.Selected = true;
					break;
				case HologramUIMode.ItemMode:
					ItemModeCheckbox.Selected = true;
					break;
				case HologramUIMode.ProjectileMode:
					ProjectileModeCheckbox.Selected = true;
					break;
			}
			this.CurrentMode = mode;
			this.IsModeBeingSet = false;
		}

		////////////////

	}
}
