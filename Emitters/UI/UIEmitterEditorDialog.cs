using System;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Terraria;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Helpers.DotNET.Reflection;
using Terraria.ModLoader;

namespace Emitters.UI {
	class UIEmitterEditorDialog : UIDialog {
		private UISlider TypeSliderElem;



		////////////////

		public UIEmitterEditorDialog() : base( UITheme.Vanilla, 480, 368 ) { }


		////////////////
		
		public override void InitializeComponents() {
			float yOffset = 0f;

			var textElem = new UIText( "Adjust Emitter", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );
			yOffset += 48f;

			this.InitializeWidgetsForMode( ref yOffset );
			this.InitializeWidgetsForType( ref yOffset );
			this.InitializeWidgetsForScale( ref yOffset );
			this.InitializeWidgetsForSpeedX( ref yOffset );
			this.InitializeWidgetsForSpeedY( ref yOffset );
			this.InitializeWidgetsForColor( ref yOffset );
			this.InitializeWidgetsForAlpha( ref yOffset );
			this.InitializeWidgetsForScatter( ref yOffset );
			this.InitializeWidgetsForHasGravity( ref yOffset );
			this.InitializeWidgetsForHasLight( ref yOffset );
		}

		////

		private void InitializeComponentForTitle( string title, bool isNewLine, ref float yOffset ) {
			var textElem = new UIText( title );
			textElem.Top.Set( yOffset, 0f );

			if( isNewLine ) {
				yOffset += 28f;
			}

			this.InnerContainer.Append( (UIElement)textElem );
		}

		////

		private void InitializeWidgetsForMode( ref float yOffset ) {
			this.InitializeComponentForTitle( "Mode:", false, ref yOffset );

			var modeOption1 = new UICheckbox( UITheme.Vanilla, "Dust", "" );
			modeOption1.Top.Set( yOffset, 0f );
			modeOption1.Left.Set( 64f, 0f );
			modeOption1.Selected = true;

			var modeOption2 = new UICheckbox( UITheme.Vanilla, "Gore", "" );
			modeOption2.Top.Set( yOffset, 0f );
			modeOption2.Left.Set( 128f, 0f );
			yOffset += 28f;

			//
			
			int dustCount, goreCount;
			if( !ReflectionHelpers.Get(typeof(ModDust), null, "DustCount", out dustCount) ) {
				return;
			}
			if( !ReflectionHelpers.Get(typeof(ModGore), null, "GoreCount", out goreCount ) ) {
				return;
			}

			bool block = false;

			modeOption1.OnSelectedChanged += () => {
				if( !block ) {
					block = true;
					modeOption2.Selected = !modeOption1.Selected;
					if( modeOption1.Selected ) {
						this.TypeSliderElem.SetRange( 0, dustCount );
					} else {
						this.TypeSliderElem.SetRange( 0, goreCount );
					}
					block = false;
				}
			};
			modeOption2.OnSelectedChanged += () => {
				if( !block ) {
					block = true;
					modeOption1.Selected = !modeOption2.Selected;
					if( modeOption2.Selected ) {
						this.TypeSliderElem.SetRange( 0, goreCount );
					} else {
						this.TypeSliderElem.SetRange( 0, dustCount );
					}
					block = false;
				}
			};

			//

			this.InnerContainer.Append( (UIElement)modeOption1 );
			this.InnerContainer.Append( (UIElement)modeOption2 );
		}

		private void InitializeWidgetsForType( ref float yOffset ) {
			this.InitializeComponentForTitle( "Type:", false, ref yOffset );

			this.TypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 200f );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.TypeSliderElem );
		}

		private void InitializeWidgetsForScale( ref float yOffset ) {
			this.InitializeComponentForTitle( "Scale:", false, ref yOffset );

			var sliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.1f,
				maxRange: 10f );
			sliderElem.Top.Set( yOffset, 0f );
			sliderElem.Left.Set( 64f, 0f );
			sliderElem.Width.Set( -64f, 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)sliderElem );
		}

		private void InitializeWidgetsForSpeedX( ref float yOffset ) {
			this.InitializeComponentForTitle( "Speed X:", false, ref yOffset );

			var sliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -20f,
				maxRange: 20f );
			sliderElem.Top.Set( yOffset, 0f );
			sliderElem.Left.Set( 64f, 0f );
			sliderElem.Width.Set( -64f, 1f );
			sliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)sliderElem );
		}

		private void InitializeWidgetsForSpeedY( ref float yOffset ) {
			this.InitializeComponentForTitle( "Speed Y:", false, ref yOffset );

			var sliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -20f,
				maxRange: 20f );
			sliderElem.Top.Set( yOffset, 0f );
			sliderElem.Left.Set( 64f, 0f );
			sliderElem.Width.Set( -64f, 1f );
			sliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)sliderElem );
		}

		private void InitializeWidgetsForColor( ref float yOffset ) {
			this.InitializeComponentForTitle( "Color:", false, ref yOffset );

			var hueSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: true,
				innerBarShader: DelegateMethods.ColorLerp_HSL_H );
			hueSliderElem.Top.Set( yOffset, 0f );
			hueSliderElem.Left.Set( 96f, 0f );
			hueSliderElem.Width.Set( -96f, 1f );
			hueSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InitializeComponentForTitle( "Intensity:", false, ref yOffset );

			var satSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: true );
			satSliderElem.Top.Set( yOffset, 0f );
			satSliderElem.Left.Set( 96f, 0f );
			satSliderElem.Width.Set( -96f, 1f );
			satSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)hueSliderElem );
			this.InnerContainer.Append( (UIElement)satSliderElem );
		}

		private void InitializeWidgetsForAlpha( ref float yOffset ) {
			this.InitializeComponentForTitle( "Alpha:", false, ref yOffset );

			var sliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f );
			sliderElem.Top.Set( yOffset, 0f );
			sliderElem.Left.Set( 64f, 0f );
			sliderElem.Width.Set( -64f, 1f );
			sliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)sliderElem );
		}

		private void InitializeWidgetsForScatter( ref float yOffset ) {
			this.InitializeComponentForTitle( "Scatter:", false, ref yOffset );

			var sliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f );
			sliderElem.Top.Set( yOffset, 0f );
			sliderElem.Left.Set( 64f, 0f );
			sliderElem.Width.Set( -64f, 1f );
			sliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)sliderElem );
		}

		private void InitializeWidgetsForHasGravity( ref float yOffset ) {
			var flagElem = new UICheckbox( UITheme.Vanilla, "Has Gravity", "" );
			flagElem.Top.Set( yOffset, 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)flagElem );
		}

		private void InitializeWidgetsForHasLight( ref float yOffset ) {
			var flagElem = new UICheckbox( UITheme.Vanilla, "Has Light", "" );
			flagElem.Top.Set( yOffset, 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)flagElem );
		}
	}
}
