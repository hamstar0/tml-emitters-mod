using System;
using Terraria;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		public override void InitializeComponents() {
			var self = this;
			float yOffset = 0f;

			var textElem = new UIText( "Adjust Emitter", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );
			yOffset += 48f;

			this.InitializeWidgetsForMode( ref yOffset );
			this.InitializeWidgetsForType( ref yOffset );
			this.InitializeWidgetsForScale( ref yOffset );
			this.InitializeWidgetsForDelay( ref yOffset );
			this.InitializeWidgetsForSpeedX( ref yOffset );
			this.InitializeWidgetsForSpeedY( ref yOffset );
			this.InitializeWidgetsForColor( ref yOffset );
			this.InitializeWidgetsForAlpha( ref yOffset );
			this.InitializeWidgetsForScatter( ref yOffset );
			this.InitializeWidgetsForHasGravity( ref yOffset );
			this.InitializeWidgetsForHasLight( ref yOffset );
			yOffset -= 28f;

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
				throw new ModHelpersException( "Could not get dust count." );
			}
			if( !ReflectionHelpers.Get(typeof(ModGore), null, "GoreCount", out goreCount ) ) {
				throw new ModHelpersException( "Could not get gore count." );
			}

			//

			bool block = false;
			void updateMode( bool isGore ) {
				if( block ) {
					return;
				}

				block = true;

				this.IsGoreMode = isGore;
				modeOption1.Selected = isGore;
				modeOption2.Selected = !isGore;

				if( isGore ) {
					this.TypeSliderElem.SetRange( 0, dustCount );
				} else {
					this.TypeSliderElem.SetRange( 0, goreCount );
				}

				block = false;
			}

			modeOption1.OnSelectedChanged += () => {
				updateMode( true );
			};
			modeOption2.OnSelectedChanged += () => {
				updateMode( false );
			};

			//

			this.InnerContainer.Append( (UIElement)modeOption1 );
			this.InnerContainer.Append( (UIElement)modeOption2 );
		}

		private void InitializeWidgetsForType( ref float yOffset ) {
			int dustCount;
			if( !ReflectionHelpers.Get( typeof( ModDust ), null, "DustCount", out dustCount ) ) {
				throw new ModHelpersException( "Could not get dust count." );
			}

			this.InitializeComponentForTitle( "Type:", false, ref yOffset );

			this.TypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)dustCount );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
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
				minRange: 0.1f,
				maxRange: 10f );
			this.ScaleSliderElem.Top.Set( yOffset, 0f );
			this.ScaleSliderElem.Left.Set( 64f, 0f );
			this.ScaleSliderElem.Width.Set( -64f, 1f );
			this.ScaleSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.ScaleSliderElem );
		}

		private void InitializeWidgetsForDelay( ref float yOffset ) {
			this.InitializeComponentForTitle( "Delay:", false, ref yOffset );
			
			this.DelaySliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 60f * 3f );
			this.DelaySliderElem.Top.Set( yOffset, 0f );
			this.DelaySliderElem.Left.Set( 64f, 0f );
			this.DelaySliderElem.Width.Set( -64f, 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.DelaySliderElem );
		}

		private void InitializeWidgetsForSpeedX( ref float yOffset ) {
			this.InitializeComponentForTitle( "Speed X:", false, ref yOffset );

			this.SpeedXSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -20f,
				maxRange: 20f );
			this.SpeedXSliderElem.Top.Set( yOffset, 0f );
			this.SpeedXSliderElem.Left.Set( 64f, 0f );
			this.SpeedXSliderElem.Width.Set( -64f, 1f );
			this.SpeedXSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.SpeedXSliderElem );
		}

		private void InitializeWidgetsForSpeedY( ref float yOffset ) {
			this.InitializeComponentForTitle( "Speed Y:", false, ref yOffset );

			this.SpeedYSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -20f,
				maxRange: 20f );
			this.SpeedYSliderElem.Top.Set( yOffset, 0f );
			this.SpeedYSliderElem.Left.Set( 64f, 0f );
			this.SpeedYSliderElem.Width.Set( -64f, 1f );
			this.SpeedYSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.SpeedYSliderElem );
		}

		private void InitializeWidgetsForColor( ref float yOffset ) {
			this.InitializeComponentForTitle( "Color:", false, ref yOffset );

			this.HueSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: true,
				innerBarShader: DelegateMethods.ColorLerp_HSL_H );
			this.HueSliderElem.Top.Set( yOffset, 0f );
			this.HueSliderElem.Left.Set( 96f, 0f );
			this.HueSliderElem.Width.Set( -96f, 1f );
			this.HueSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InitializeComponentForTitle( "Intensity:", false, ref yOffset );

			this.IntensitySliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: true );
			this.IntensitySliderElem.Top.Set( yOffset, 0f );
			this.IntensitySliderElem.Left.Set( 96f, 0f );
			this.IntensitySliderElem.Width.Set( -96f, 1f );
			this.IntensitySliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.HueSliderElem );
			this.InnerContainer.Append( (UIElement)this.IntensitySliderElem );
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
			this.AlphaSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.AlphaSliderElem );
		}

		private void InitializeWidgetsForScatter( ref float yOffset ) {
			this.InitializeComponentForTitle( "Scatter:", false, ref yOffset );

			this.ScatterSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 100f );
			this.ScatterSliderElem.Top.Set( yOffset, 0f );
			this.ScatterSliderElem.Left.Set( 64f, 0f );
			this.ScatterSliderElem.Width.Set( -64f, 1f );
			this.ScatterSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.ScatterSliderElem );
		}

		private void InitializeWidgetsForHasGravity( ref float yOffset ) {
			this.HasGravityCheckbox = new UICheckbox( UITheme.Vanilla, "Has Gravity", "" );
			this.HasGravityCheckbox.Top.Set( yOffset, 0f );
			this.HasGravityCheckbox.Selected = true;
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.HasGravityCheckbox );
		}

		private void InitializeWidgetsForHasLight( ref float yOffset ) {
			this.HasLightCheckbox = new UICheckbox( UITheme.Vanilla, "Has Light", "" );
			this.HasLightCheckbox.Top.Set( yOffset, 0f );
			this.HasLightCheckbox.Selected = true;
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.HasLightCheckbox );
		}
	}
}
