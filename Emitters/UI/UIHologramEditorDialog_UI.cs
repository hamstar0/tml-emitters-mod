using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.DotNET.Reflection;
using On.Terraria;
using Main = Terraria.Main;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog
	{

		public bool IsSettingMode = false;

		////////////////
		/// TODO: Check for non reflection ways ie. Main.npcTexture.length
		public void SetHologramMode( int hologramMode ) {
			if( this.IsSettingMode ) { return; }
			this.IsSettingMode = true;

			hologramMode = (int)this.ModeSliderElem.RememberedInputValue;

			switch (hologramMode)
			{
				case 1:
					this.TypeSliderElem.SetRange(0,NPCLoader.NPCCount);
					break;
				case 2:
					this.TypeSliderElem.SetRange(0, ItemLoader.ItemCount - 1);
					break;
				case 3:
					this.TypeSliderElem.SetRange(0, ProjectileLoader.ProjectileCount - 1);
					break;
				default:
					break;
			}
			this.IsSettingMode = false;
		}

		
	}
}
