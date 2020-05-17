using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Helpers.Debug;
using Emitters.Items;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UISoundEmitterEditorDialog : UIDialog {

		////////////////

		private UISlider TypeSliderElem;
		private UISlider StyleSliderElem;
		private UISlider VolumeSliderElem;
		private UISlider PitchSliderElem;
		private UISlider DelaySliderElem;

		////

		private Item SoundEmitterItem = null;



		////////////////

		public UISoundEmitterEditorDialog() : base( UITheme.Vanilla, 600, 400 ) { }


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


		////////////////


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


		////////////////

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

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );
			Main.LocalPlayer.mouseInterface = true;
		}


		////////////////

		 private SoundEmitterDefinition CachedSoundEmitterDef = null;

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			SoundEmitterDefinition def = this.CreateSoundEmitterDefinition();
			def.Timer = this.CachedSoundEmitterDef?.Timer ?? 0;
			this.CachedSoundEmitterDef = def;

			def.AnimateSoundEmitter( Main.MouseWorld );
		}
	}
}
