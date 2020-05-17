using System;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.DotNET.Reflection;
using UISlider = Emitters.Libraries.Classes.UI.UISlider;


namespace Emitters.UI {
	partial class UISoundEmitterEditorDialog : UIDialog {
		public override void InitializeComponents() {
			var self = this;
			float yOffset = 0f;

			var textElem = new UIText( "Adjust SoundEmitter", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );
			yOffset += 48f;

			this.InitializeWidgetsForType( ref yOffset );
			this.IntializeWidgetsForStyle( ref yOffset );
			this.InitializeWidgetsForVolume( ref yOffset );
			this.InitializeWidgetsForPitch(ref yOffset);
			this.InitializeWidgetsForDelay( ref yOffset );
			yOffset -= 28f;

			var applyButton = new UITextPanelButton( UITheme.Vanilla, "Apply" );
			applyButton.Top.Set( yOffset + 50f, 0f );
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
				minRange:-1f,
				maxRange: 50f );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
			this.TypeSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.TypeSliderElem );
		}

		private void IntializeWidgetsForStyle(ref float yOffset){
			this.InitializeComponentForTitle("Style:", false, ref yOffset);

			this.StyleSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 39f );		//Temporary until slider is adjusted for style 
			this.StyleSliderElem.Top.Set(yOffset, 0f);
			this.StyleSliderElem.Left.Set(64f, 0f);
			this.StyleSliderElem.Width.Set(-64f, 1f);
			this.StyleSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.StyleSliderElem);
		}

		private void InitializeWidgetsForVolume( ref float yOffset ) {
			this.InitializeComponentForTitle( "Volume:", false, ref yOffset );

			this.VolumeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.1f,
				maxRange: 1f );
			this.VolumeSliderElem.Top.Set( yOffset, 0f );
			this.VolumeSliderElem.Left.Set( 64f, 0f );
			this.VolumeSliderElem.Width.Set( -64f, 1f );
			this.VolumeSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.VolumeSliderElem );
		}

		private void InitializeWidgetsForPitch(ref float yOffset)
		{
			this.InitializeComponentForTitle("Pitch:", false, ref yOffset);

			this.PitchSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -4f,
				maxRange: 4f); //Pitch goes by octave so a reasonable range should suffice
			this.PitchSliderElem.Top.Set(yOffset, 0f);
			this.PitchSliderElem.Left.Set(64f, 0f);
			this.PitchSliderElem.Width.Set(-64f, 1f);
			this.PitchSliderElem.SetValue(0f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.PitchSliderElem);
		}

		private void InitializeWidgetsForDelay( ref float yOffset ) {
			this.InitializeComponentForTitle( "Delay:", false, ref yOffset );
			
			this.DelaySliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 200f ); //Not sure how big we should make this.
			this.DelaySliderElem.Top.Set( yOffset, 0f );
			this.DelaySliderElem.Left.Set( 64f, 0f );
			this.DelaySliderElem.Width.Set( -64f, 1f );
			this.DelaySliderElem.SetValue(100f);
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.DelaySliderElem );
		}
	}
}
