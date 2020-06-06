using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Helpers.Debug;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UISoundEmitterEditorDialog : UIDialog {
		private UISlider TypeSliderElem;
		private UISlider StyleSliderElem;
		private UISlider VolumeSliderElem;
		private UISlider PitchSliderElem;
		private UISlider DelaySliderElem;

		////

		private Item SoundEmitterItem = null;



		////////////////

		public UISoundEmitterEditorDialog() : base( UITheme.Vanilla, 600, 272 ) { }


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
