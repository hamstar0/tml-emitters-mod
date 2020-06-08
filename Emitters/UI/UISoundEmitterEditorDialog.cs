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

			Vector2 scrCenter = Main.screenPosition + ( new Vector2( Main.screenWidth, Main.screenHeight ) * 0.5f );
			Vector2 pos = Main.screenPosition
				+ ( new Vector2( Main.mouseX, Main.mouseY ) * Main.UIScale );
			pos = ( pos - scrCenter ) / Main.GameZoomTarget;
			pos += scrCenter;

			def.AnimateSoundEmitter( pos );
		}
	}
}
