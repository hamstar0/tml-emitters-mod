using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Terraria;

namespace Emitters.UI
{
	partial class UIHologramEditorDialog : UIDialog
	{
		private void InitializeShadersTab( UIThemedPanel container ) 
		{
			float yOffset = 0f;

			this.InitializeWidgetsforShaderTime(container, ref yOffset);
			this.InitializeWidgetsForCrtEffect( container, ref yOffset );
			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsforShaderTime( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Shader Time:", false, ref yOffset );

			this.ShadertTimeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false
			);
			this.ShadertTimeSliderElem.Top.Set( yOffset, 0f );
			this.ShadertTimeSliderElem.Left.Set( 96f, 0f );
			this.ShadertTimeSliderElem.Width.Set( -96f, 1f );
			this.ShadertTimeSliderElem.SetValue( .5f );
			container.Append( this.ShadertTimeSliderElem );

			yOffset += 28f;
		}
		private void InitializeWidgetsForCrtEffect( UIThemedPanel container, ref float yOffset ) {
			this.CRTEffectCheckbox = new UICheckbox( UITheme.Vanilla, "CRT Effect", "" );
			this.CRTEffectCheckbox.Top.Set( yOffset, 0f );
			this.CRTEffectCheckbox.Selected = false;
			container.Append( this.CRTEffectCheckbox );
			yOffset += 28f;
		}
	}
}
