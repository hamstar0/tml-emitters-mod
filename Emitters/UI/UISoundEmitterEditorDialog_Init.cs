using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;


namespace Emitters.UI {
	partial class UISoundEmitterEditorDialog : UIDialog {
		public override void InitializeComponents() {
			var self = this;
			float yOffset = 0f;

			var textElem = new UIText( "Adjust Sound Emitter", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );
			yOffset += 48f;

			this.InitializeWidgetsForType( ref yOffset );
			this.IntializeWidgetsForStyle( ref yOffset );
			this.InitializeWidgetsForVolume( ref yOffset );
			this.InitializeWidgetsForPitch( ref yOffset );
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
				minRange: 0f,
				maxRange: 50f );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
			this.TypeSliderElem.SetValue( 1f );
			this.TypeSliderElem.PreOnChange += (value) => {
				if( value > 41f && value < 50f ) {
					if( this.TypeSliderElem.RememberedInputValue <= 41 ) {
						value = 50f;
					} else {
						value = 41f;
					}
				}
				this.UpdateStyleSlider( (int)value );
				return value;
			};
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.TypeSliderElem );
		}

		private void IntializeWidgetsForStyle( ref float yOffset ) {
			this.InitializeComponentForTitle( "Style:", false, ref yOffset );

			this.StyleSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -1f,
				maxRange: -1f );        //Temporary until slider is adjusted for style 
			this.StyleSliderElem.Top.Set( yOffset, 0f );
			this.StyleSliderElem.Left.Set( 64f, 0f );
			this.StyleSliderElem.Width.Set( -64f, 1f );
			this.StyleSliderElem.SetValue( -1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.StyleSliderElem );
		}

		private void InitializeWidgetsForVolume( ref float yOffset ) {
			this.InitializeComponentForTitle( "Volume:", false, ref yOffset );

			this.VolumeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 1f );
			this.VolumeSliderElem.Top.Set( yOffset, 0f );
			this.VolumeSliderElem.Left.Set( 64f, 0f );
			this.VolumeSliderElem.Width.Set( -64f, 1f );
			this.VolumeSliderElem.SetValue( 1f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.VolumeSliderElem );
		}

		private void InitializeWidgetsForPitch( ref float yOffset ) {
			this.InitializeComponentForTitle( "Pitch:", false, ref yOffset );

			this.PitchSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -1f,
				maxRange: 1f );
			this.PitchSliderElem.Top.Set( yOffset, 0f );
			this.PitchSliderElem.Left.Set( 64f, 0f );
			this.PitchSliderElem.Width.Set( -64f, 1f );
			this.PitchSliderElem.SetValue( 0f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.PitchSliderElem );
		}

		private void InitializeWidgetsForDelay( ref float yOffset ) {
			this.InitializeComponentForTitle( "Delay:", false, ref yOffset );

			this.DelaySliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 4f,
				maxRange: 60f * 60f );
			this.DelaySliderElem.Top.Set( yOffset, 0f );
			this.DelaySliderElem.Left.Set( 64f, 0f );
			this.DelaySliderElem.Width.Set( -64f, 1f );
			this.DelaySliderElem.SetValue( 100f );
			yOffset += 28f;

			this.InnerContainer.Append( (UIElement)this.DelaySliderElem );
		}


		////////////////

		private void UpdateStyleSlider( int sndType ) {
			switch( sndType ) {
			case 2:
				this.StyleSliderElem.SetRange( 0f, (float)SoundLoader.SoundCount(SoundType.Item) );	//125f
				break;
			case 3:
				this.StyleSliderElem.SetRange( 1f, (float)SoundLoader.SoundCount(SoundType.NPCHit) );	//57f
				break;
			case 4:
				this.StyleSliderElem.SetRange( 0f, (float)SoundLoader.SoundCount(SoundType.NPCKilled) ); //62f
				break;
			case 14:
				this.StyleSliderElem.SetRange( 0f, 0f );    //Main.soundInstanceZombie
				break;
			case 15:
				this.StyleSliderElem.SetRange( 0f, (float)Main.soundInstanceRoar.Length - 1f );	//2f
				break;
			case 19:
				this.StyleSliderElem.SetRange( 0f, (float)Main.soundInstanceSplash.Length - 1f );	//1f
				break;
			case 28:
				this.StyleSliderElem.SetRange( 0f, (float)Main.soundInstanceMech.Length - 1f );	//0f
				break;
			case 29:
			case 32:
				this.StyleSliderElem.SetRange( 0f, (float)Main.soundInstanceZombie.Length - 1f );	//105f
				break;
			case 34:
			case 35:
				this.StyleSliderElem.SetRange( 0f, 50f );
				break;
			case 36:
			case 39:
				this.StyleSliderElem.SetRange( 0f, (float)Main.soundInstanceDrip.Length - 1f );	//2f
				break;
			case 50:
				this.StyleSliderElem.SetRange( 0f, (float)SoundLoader.SoundCount(SoundType.Custom) );
				break;
			default:
				this.StyleSliderElem.SetRange( -1f, -1f );
				break;
			}
		}
	}
}
