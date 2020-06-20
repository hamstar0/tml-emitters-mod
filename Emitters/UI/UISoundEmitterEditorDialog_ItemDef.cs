using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.Debug;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UISoundEmitterEditorDialog : UIDialog {
		internal void SetItem( Item soundEmitterItem ) {
			var def = BaseEmitterDefinition.CreateOrGetDefForItem<SoundEmitterDefinition>( soundEmitterItem );

			this.SoundEmitterItem = soundEmitterItem;

			this.TypeSliderElem.SetValue( def.Type );
			this.StyleSliderElem.SetValue( def.Style );
			this.VolumeSliderElem.SetValue( def.Volume );
			this.PitchSliderElem.SetValue( def.Pitch );
			this.DelaySliderElem.SetValue( def.Delay );
		}


		////

		public void ApplySettingsToCurrentItem() {
			if( this.SoundEmitterItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			var myitem = this.SoundEmitterItem.modItem as SoundEmitterItem;
			if( myitem == null ) {
				Main.NewText( "No sound emitter item selected. Changes not saved.", Color.Red );
				return;
			}

			myitem.SetDefinition( this.CreateSoundEmitterDefinition() );
		}


		////////////////

		public SoundEmitterDefinition CreateSoundEmitterDefinition() {
			return new SoundEmitterDefinition(
				type: (int)this.TypeSliderElem.RememberedInputValue,
				style: (int)this.StyleSliderElem.RememberedInputValue,
				volume: this.VolumeSliderElem.RememberedInputValue,
				pitch: this.PitchSliderElem.RememberedInputValue,
				delay: (int)this.DelaySliderElem.RememberedInputValue,
				isActivated: true
			);
		}
	}
}
