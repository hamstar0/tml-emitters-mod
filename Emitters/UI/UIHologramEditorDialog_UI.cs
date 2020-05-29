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
					if (!ReflectionHelpers.Get(typeof(ModNPC), null, "NPCCount", out int npcCount))
					{
						throw new ModHelpersException("Could not get NPC count.");
					}
					this.TypeSliderElem.SetRange(0,npcCount);
					break;
				case 2:
					//if (!ReflectionHelpers.Get(typeof(ModItem), null, "ItemCount", out int itemCount))
					//{
					//	throw new ModHelpersException("Could not get item count.");
					//}
					this.TypeSliderElem.SetRange(0, Main.item.Length - 1);
					break;
				case 3:
					//if (!ReflectionHelpers.Get(typeof(ModProjectile), null, "ProjectileCounta", out int projectileCount))
					//{
					//	throw new ModHelpersException("Could not get projectile count.");
					//}
					this.TypeSliderElem.SetRange(0, ProjectileLoader.ProjectileCount - 1);
					break;
				default:
					break;
			}
			this.IsSettingMode = false;
		}

		
	}
}
