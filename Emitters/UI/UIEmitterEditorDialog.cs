using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		private bool IsGoreMode => this.ModeGoreFlagElem.Selected;


		////////////////

		private UICheckbox ModeDustFlagElem;
		private UICheckbox ModeGoreFlagElem;
		private UISlider TypeSliderElem;
		private UISlider ScaleSliderElem;
		private UISlider DelaySliderElem;
		private UISlider SpeedXSliderElem;
		private UISlider SpeedYSliderElem;
		private UISlider HueSliderElem;
		private UISlider SaturationSliderElem;
		private UISlider TransparencySliderElem;
		private UISlider ScatterSliderElem;
		private UICheckbox HasGravityCheckbox;
		private UICheckbox HasLightCheckbox;

		////

		private Item EmitterItem = null;



		////////////////

		public UIEmitterEditorDialog() : base( UITheme.Vanilla, 600, 400 ) { }


		////////////////

		public Color GetColor() {
			float hue = this.HueSliderElem.RememberedInputValue;
			float saturation = this.SaturationSliderElem.RememberedInputValue;

			Color color = Main.hslToRgb( hue, saturation, 0.5f );
			return color;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );
			Main.LocalPlayer.mouseInterface = true;
		}


		////////////////

		 private EmitterDefinition CachedEmitterDef = null;

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			EmitterDefinition def = this.CreateEmitterDefinition();
			def.Timer = this.CachedEmitterDef?.Timer ?? 0;
			this.CachedEmitterDef = def;

			Vector2 scrCenter = Main.screenPosition + (new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f);
			Vector2 pos = Main.screenPosition
				+ (new Vector2(Main.mouseX, Main.mouseY) * Main.UIScale);
			pos = (pos - scrCenter) / Main.GameZoomTarget;
			pos += scrCenter;

			def.AnimateEmitter( pos );
		}
	}
}
