using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		internal void SetItem( Item emitterItem ) {
			var def = BaseEmitterDefinition.CreateOrGetDefForItem<EmitterDefinition>( emitterItem );

			this.EmitterItem = emitterItem;

			Vector3 hsl = Main.rgbToHsl( def.Color );

			this.SetGoreMode( def.IsGoreMode );
			this.TypeSliderElem.SetValue( def.Type );
			this.ScaleSliderElem.SetValue( def.Scale );
			this.DelaySliderElem.SetValue( def.Delay );
			this.SpeedXSliderElem.SetValue( def.SpeedX );
			this.SpeedYSliderElem.SetValue( def.SpeedY );
			this.HueSliderElem.SetValue( hsl.X );
			this.SaturationSliderElem.SetValue( hsl.Y );
			this.TransparencySliderElem.SetValue( def.Transparency );
			this.ScatterSliderElem.SetValue( def.Scatter );
			this.HasGravityCheckbox.Selected = def.HasGravity;
			this.HasLightCheckbox.Selected = def.HasLight;
		}


		////

		public void ApplySettingsToCurrentItem() {
			if( this.EmitterItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			var myitem = this.EmitterItem.modItem as EmitterItem;
			if( myitem == null ) {
				Main.NewText( "No emitter item selected. Changes not saved.", Color.Red );
				return;
			}

			myitem.SetDefinition( this.CreateEmitterDefinition() );
		}


		////////////////

		public EmitterDefinition CreateEmitterDefinition() {
			return new EmitterDefinition(
				isGoreMode: this.IsGoreMode,
				type: (int)this.TypeSliderElem.RememberedInputValue,
				scale: this.ScaleSliderElem.RememberedInputValue,
				delay: (int)this.DelaySliderElem.RememberedInputValue,
				speedX: this.SpeedXSliderElem.RememberedInputValue,
				speedY: this.SpeedYSliderElem.RememberedInputValue,
				color: this.GetColor(),
				transparency: (byte)this.TransparencySliderElem.RememberedInputValue,
				scatter: this.ScatterSliderElem.RememberedInputValue,
				hasGravity: this.HasGravityCheckbox.Selected,
				hasLight: this.HasLightCheckbox.Selected,
				isActivated: true
			);
		}
	}
}
