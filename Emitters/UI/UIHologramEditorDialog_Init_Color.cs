using Terraria;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private void InitializeColorTab( UIThemedPanel container ) {
			float yOffset = 0f;

			this.InitializeWidgetsForColor( container, ref yOffset );
			this.InitializeWidgetsForAlpha( container, ref yOffset );

			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsForColor( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Hue:", false, ref yOffset );

			this.HueSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false,
				innerBarShader: DelegateMethods.ColorLerp_HSL_H );
			this.HueSliderElem.Top.Set( yOffset, 0f );
			this.HueSliderElem.Left.Set( 96f, 0f );
			this.HueSliderElem.Width.Set( -96f, 1f );
			this.HueSliderElem.SetValue( .53f );
			container.Append( this.HueSliderElem );

			yOffset += 28f;

			this.InitializeComponentForTitle( container, "Saturation:", false, ref yOffset );

			this.SaturationSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false );
			this.SaturationSliderElem.Top.Set( yOffset, 0f );
			this.SaturationSliderElem.Left.Set( 96f, 0f );
			this.SaturationSliderElem.Width.Set( -96f, 1f );
			this.SaturationSliderElem.SetValue( 1f );
			container.Append( this.SaturationSliderElem );

			yOffset += 28f;

			this.InitializeComponentForTitle( container, "Lightness:", false, ref yOffset );

			this.LightnessSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false );
			this.LightnessSliderElem.Top.Set( yOffset, 0f );
			this.LightnessSliderElem.Left.Set( 96f, 0f );
			this.LightnessSliderElem.Width.Set( -96f, 1f );
			this.LightnessSliderElem.SetValue( 0.5f );
			container.Append( this.LightnessSliderElem );

			yOffset += 28f;
		}

		private void InitializeWidgetsForAlpha( UIThemedPanel container, ref float yOffsetColorPanel ) {
			this.InitializeComponentForTitle( container, "Alpha:", false, ref yOffsetColorPanel );

			this.AlphaSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 255f );
			this.AlphaSliderElem.Top.Set( yOffsetColorPanel, 0f );
			this.AlphaSliderElem.Left.Set( 64f, 0f );
			this.AlphaSliderElem.Width.Set( -64f, 1f );
			this.AlphaSliderElem.SetValue( 255f );

			yOffsetColorPanel += 28f;

			container.Append( this.AlphaSliderElem );

			yOffsetColorPanel += 28f;
		}
	}
}