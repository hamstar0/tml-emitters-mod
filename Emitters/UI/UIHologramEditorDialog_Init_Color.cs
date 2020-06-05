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

			yOffset += 16f;

			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsForColor( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Hue:", false, ref yOffset );

			this.HueSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false,
				innerBarShader: DelegateMethods.ColorLerp_HSL_H );
			this.HueSlider.Top.Set( yOffset, 0f );
			this.HueSlider.Left.Set( 96f, 0f );
			this.HueSlider.Width.Set( -96f, 1f );
			this.HueSlider.SetValue( .53f );
			container.Append( this.HueSlider );

			yOffset += 28f;

			this.InitializeTitle( container, "Saturation:", false, ref yOffset );

			this.SaturationSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false );
			this.SaturationSlider.Top.Set( yOffset, 0f );
			this.SaturationSlider.Left.Set( 96f, 0f );
			this.SaturationSlider.Width.Set( -96f, 1f );
			this.SaturationSlider.SetValue( 1f );
			container.Append( this.SaturationSlider );

			yOffset += 28f;

			this.InitializeTitle( container, "Lightness:", false, ref yOffset );

			this.LightnessSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false );
			this.LightnessSlider.Top.Set( yOffset, 0f );
			this.LightnessSlider.Left.Set( 96f, 0f );
			this.LightnessSlider.Width.Set( -96f, 1f );
			this.LightnessSlider.SetValue( 0.5f );
			container.Append( this.LightnessSlider );

			yOffset += 28f;
		}

		private void InitializeWidgetsForAlpha( UIThemedPanel container, ref float yOffsetColorPanel ) {
			this.InitializeTitle( container, "Alpha:", false, ref yOffsetColorPanel );

			this.AlphaSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 255f );
			this.AlphaSlider.Top.Set( yOffsetColorPanel, 0f );
			this.AlphaSlider.Left.Set( 64f, 0f );
			this.AlphaSlider.Width.Set( -64f, 1f );
			this.AlphaSlider.SetValue( 255f );

			yOffsetColorPanel += 28f;

			container.Append( this.AlphaSlider );

			yOffsetColorPanel += 28f;
		}
	}
}