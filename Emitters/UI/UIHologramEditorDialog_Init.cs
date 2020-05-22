using System;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Helpers.Debug;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		public override void InitializeComponents() {
			var self = this;
			float yOffset = 0f;

			var textElem = new UIText( "Adjust Hologram", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );
			yOffset += 48f;

			this.InitializeWidgetsForType( ref yOffset );
			this.InitializeWidgetsForScale( ref yOffset );
			this.InitializeWidgetsForColor( ref yOffset );
			this.InitializeWidgetsForAlpha( ref yOffset );
			this.InitializeWidgetsForDirection( ref yOffset );
			this.InitializeWidgetsForRotation( ref yOffset );
			this.InitializeWidgetsForOffsetX( ref yOffset );
			this.InitializeWidgetsForOffsetY( ref yOffset );
			this.InitializeWidgetsForFrame( ref yOffset );
			this.InitializeWidgetsForWorldLighting( ref yOffset );
			yOffset -= 15f;

			var applyButton = new UITextPanelButton( UITheme.Vanilla, "Apply" );
			applyButton.Top.Set( yOffset, 0f );
			applyButton.Left.Set( -64f, 1f );
			applyButton.Height.Set( applyButton.GetOuterDimensions().Height + 4f, 0f );
			applyButton.OnClick += ( _, __ ) => {
				self.Close();
				self.ApplySettingsToCurrentItem();
			};
			this.InnerContainer.Append( (UIElement)applyButton );
		}


		////////////////

		private void InitializeComponentForTitle( string title, bool isNewLine, ref float yOffset ) {
			var textElem = new UIText( title );
			textElem.Top.Set( yOffset, 0f );

			if( isNewLine ) {
				yOffset += 28f;
			}

			this.InnerContainer.Append( (UIElement)textElem );
		}

		////

		private void InitializeWidgetsForType( ref float yOffset ) {
			this.InitializeComponentForTitle( "Type:", false, ref yOffset );

			this.TypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: Main.npcTexture.Length );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
			this.TypeSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.TypeSliderElem );
		}

		private void InitializeWidgetsForScale( ref float yOffset ) {
			this.InitializeComponentForTitle( "Scale:", false, ref yOffset );

			this.ScaleSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 10f );
			this.ScaleSliderElem.Top.Set( yOffset, 0f );
			this.ScaleSliderElem.Left.Set( 64f, 0f );
			this.ScaleSliderElem.Width.Set( -64f, 1f );
			this.ScaleSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.ScaleSliderElem );
		}

		private void InitializeWidgetsForColor( ref float yOffset ) {
			this.InitializeComponentForTitle( "Hue:", false, ref yOffset );

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
			this.HueSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InitializeComponentForTitle( "Saturation:", false, ref yOffset );

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
			yOffset += 28f;

			this.InitializeComponentForTitle( "Lightness:", false, ref yOffset );

			this.LightnessSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: true );
			this.LightnessSliderElem.Top.Set( yOffset, 0f );
			this.LightnessSliderElem.Left.Set( 96f, 0f );
			this.LightnessSliderElem.Width.Set( -96f, 1f );
			this.LightnessSliderElem.SetValue( 0.5f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.HueSliderElem );
			this.InnerContainer.Append( (UIElement)this.SaturationSliderElem );
			this.InnerContainer.Append( (UIElement)this.LightnessSliderElem );
		}

		private void InitializeWidgetsForAlpha( ref float yOffset ) {
			this.InitializeComponentForTitle( "Alpha:", false, ref yOffset );

			this.AlphaSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 255f );
			this.AlphaSliderElem.Top.Set( yOffset, 0f );
			this.AlphaSliderElem.Left.Set( 64f, 0f );
			this.AlphaSliderElem.Width.Set( -64f, 1f );
			this.AlphaSliderElem.SetValue( 255f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.AlphaSliderElem );
		}

		private void InitializeWidgetsForDirection( ref float yOffset ) {
			this.InitializeComponentForTitle( "Direction:", false, ref yOffset );

			bool isChangingDirection = false;

			this.DirectionSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -1f,
				maxRange: 1f );
			this.DirectionSliderElem.Top.Set( yOffset, 0f );
			this.DirectionSliderElem.Left.Set( 64f, 0f );
			this.DirectionSliderElem.Width.Set( -64f, 1f );
			this.DirectionSliderElem.SetValue( 1f );
			this.DirectionSliderElem.PreOnChange += (value) => {
				if( isChangingDirection ) {
					return null;
				}
				isChangingDirection = true;

				if( value >= 0f ) {
					this.DirectionSliderElem.SetValue( 1f );
				} else {
					this.DirectionSliderElem.SetValue( -1f );
				}

				isChangingDirection = false;
				return null;
			};
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.DirectionSliderElem );
		}

		private void InitializeWidgetsForRotation( ref float yOffset ) {
			this.InitializeComponentForTitle( "Rotation:", false, ref yOffset );

			this.RotationSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 360f );
			this.RotationSliderElem.Top.Set( yOffset, 0f );
			this.RotationSliderElem.Left.Set( 64f, 0f );
			this.RotationSliderElem.Width.Set( -64f, 1f );
			this.RotationSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.RotationSliderElem );
		}

		private void InitializeWidgetsForOffsetX( ref float yOffset ) {
			this.InitializeComponentForTitle( "X Offset:", false, ref yOffset );

			this.OffsetXSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,	//0f
				maxRange: 256f );	//15f
			this.OffsetXSliderElem.Top.Set( yOffset, 0f );
			this.OffsetXSliderElem.Left.Set( 64f, 0f );
			this.OffsetXSliderElem.Width.Set( -64f, 1f );
			this.OffsetXSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.OffsetXSliderElem );
		}
		private void InitializeWidgetsForOffsetY( ref float yOffset ) {

			this.InitializeComponentForTitle( "Y Offset:", false, ref yOffset );

			this.OffsetYSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,	//0f
				maxRange: 256f );	//15f
			this.OffsetYSliderElem.Top.Set( yOffset, 0f );
			this.OffsetYSliderElem.Left.Set( 64f, 0f );
			this.OffsetYSliderElem.Width.Set( -64f, 1f );
			this.OffsetYSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.OffsetYSliderElem );
		}

		private void InitializeWidgetsForFrame( ref float yOffset ) {
			this.InitializeComponentForTitle( "Frame:", false, ref yOffset );

			this.FrameSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 60f );
			this.FrameSliderElem.Top.Set( yOffset, 0f );
			this.FrameSliderElem.Left.Set( 64f, 0f );
			this.FrameSliderElem.Width.Set( -64f, 1f );
			this.FrameSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.FrameSliderElem );
		}

		private void InitializeWidgetsForWorldLighting( ref float yOffset ) {
			this.WorldLightingCheckbox = new UICheckbox( UITheme.Vanilla, "World Lighting", "" );
			this.WorldLightingCheckbox.Top.Set( yOffset, 0f );
			this.WorldLightingCheckbox.Selected = true;
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.WorldLightingCheckbox );
		}
	}
}
