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
		internal void SetItem( Item SoundEmitterItem ) {
			this.SoundEmitterItem = SoundEmitterItem;

			var myitem = SoundEmitterItem.modItem as SoundEmitterItem;
			if( myitem.Def == null ) {
				return;
			}

			this.TypeSliderElem.SetValue( myitem.Def.Type );
			this.StyleSliderElem.SetValue( myitem.Def.Style );
			this.VolumeSliderElem.SetValue( myitem.Def.Volume );
			this.PitchSliderElem.SetValue( myitem.Def.Pitch );
			this.DelaySliderElem.SetValue( myitem.Def.Delay );
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

			myitem?.SetSoundEmitterDefinition( this.CreateSoundEmitterDefinition() );
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
