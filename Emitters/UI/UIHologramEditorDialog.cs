using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Emitters.Definitions;
using Emitters.Helpers.UI;


namespace Emitters.UI {
	public enum HologramUITab {
		Main,
		Color,
		Shader
	}




	partial class UIHologramEditorDialog : UIDialog {
		public HologramUITab CurrentTab { get; private set; } = HologramUITab.Main;
		public HologramMode CurrentMode { get; private set; } = HologramMode.NPC;
		public HologramShaderMode CurrentsShaderMode { get; private set; } = HologramShaderMode.Vanilla;


		////////////////

		private float TabStartInnerHeight = 86f;

		////

		private UIThemedPanel MainTabContainer;
		private UIThemedPanel ColorTabContainer;
		private UIThemedPanel ShaderTabContainer;

		private float FullDialogHeight;
		private float MainTabHeight;
		private float ColorTabHeight;
		private float ShaderTabHeight;

		//

		private UICheckbox ModeNpcChoice;
		private UICheckbox ModeItemChoice;
		private UICheckbox ModeProjectileChoice;
		private UISlider TypeSlider;
		private UISlider ScaleSlider;
		private UISlider DirectionSlider;
		private UISlider RotationSlider;
		private UISlider OffsetXSlider;
		private UISlider OffsetYSlider;
		private UISlider FrameStartSlider;
		private UISlider FrameEndSlider;
		private UISlider FrameRateTicksSlider;
		private UICheckbox WorldLightingFlag;

		//

		private UISlider HueSlider;
		private UISlider SaturationSlider;
		private UISlider LightnessSlider;
		private UISlider AlphaSlider;

		//

		private UISlider ShaderTypeSlider;
		private UISlider ShadertTimeSlider;
		private UICheckbox ShaderVanillaChoice;
		private UICheckbox ShaderCustomChoice;
		private UICheckbox ShaderNoneChoice;

		//
		private UITextPanelButton ApplyButton;


		////////////////

		private Item HologramItem = null;



		////////////////

		public UIHologramEditorDialog() : base( UITheme.Vanilla, 600, 500 ) { }


		////////////////

		public Color GetColor() {
			float hue = this.HueSlider.RememberedInputValue;
			float saturation = this.SaturationSlider.RememberedInputValue;
			float lightness = this.LightnessSlider.RememberedInputValue;
			Color color = Main.hslToRgb( hue, saturation, lightness );
			return color;
		}


		////////////////

		public override void Recalculate() {
			float tabHeight = 0;

			switch( this.CurrentTab ) {
			case HologramUITab.Main:
				tabHeight = this.MainTabHeight;
				break;
			case HologramUITab.Color:
				tabHeight = this.ColorTabHeight;
				break;
			case HologramUITab.Shader:
				tabHeight = this.ShaderTabHeight;
				break;
			}

			this.SetTopPosition( this.FullDialogHeight * -0.5f, 0.5f, 0f );
			this.OuterContainer?.Height.Set( (this.FullDialogHeight - this.MainTabHeight) + tabHeight, 0f );

			base.Recalculate();
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );
			Main.LocalPlayer.mouseInterface = true;
		}


		////////////////

		private HologramDefinition CachedHologramDef = null;

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			HologramDefinition def = this.CreateHologramDefinition();
			def.CurrentFrame = this.CachedHologramDef?.CurrentFrame ?? 0;
			def.CurrentFrameElapsedTicks = this.CachedHologramDef?.CurrentFrameElapsedTicks ?? 0;

			this.CachedHologramDef = def;

			var mouseScr = new Vector2( Main.mouseX, Main.mouseY );
			mouseScr = UIZoomHelpers.ApplyZoomFromScreenCenter( mouseScr, null, true, null, null );
			var mouseWld = mouseScr + Main.screenPosition;

			if( def.AnimateHologram(mouseWld, true) ) {
				def.DrawHologram( sb, mouseWld, true );
			}
		}
	}
}