using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		internal void SetItem( Item emitterItem ) {
			this.EmitterItem = emitterItem;

			var myitem = emitterItem.modItem as EmitterItem;
			if( myitem.Def == null ) {
				return;
			}

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );

			this.SetGoreMode( myitem.Def.IsGoreMode );
			this.TypeSliderElem.SetValue( myitem.Def.Type );
			this.ScaleSliderElem.SetValue( myitem.Def.Scale );
			this.DelaySliderElem.SetValue( myitem.Def.Delay );
			this.SpeedXSliderElem.SetValue( myitem.Def.SpeedX );
			this.SpeedYSliderElem.SetValue( myitem.Def.SpeedY );
			this.HueSliderElem.SetValue( hsl.X );
			this.SaturationSliderElem.SetValue( hsl.Y );
			this.AlphaSliderElem.SetValue( myitem.Def.Alpha );
			this.ScatterSliderElem.SetValue( myitem.Def.Scatter );
			this.HasGravityCheckbox.Selected = myitem.Def.HasGravity;
			this.HasLightCheckbox.Selected = myitem.Def.HasLight;
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

			myitem?.SetEmitterDefinition( this.CreateEmitterDefinition() );
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
				alpha: (byte)this.AlphaSliderElem.RememberedInputValue,
				scatter: this.ScatterSliderElem.RememberedInputValue,
				hasGravity: this.HasGravityCheckbox.Selected,
				hasLight: this.HasLightCheckbox.Selected,
				isActivated: true
			);
		}
	}
}
