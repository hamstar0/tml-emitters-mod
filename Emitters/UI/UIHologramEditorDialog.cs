using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Emitters.Definitions;
using Emitters.Items;


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

		internal bool SetItem( Item hologramItem ) {
			var myitem = hologramItem.modItem as HologramItem;
			if( myitem.Def == null ) {
				return false;
			}

			this.HologramItem = hologramItem;

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );

			this.SetHologramMode( myitem.Def.Mode );
			this.SetHologramShaderMode( myitem.Def.ShaderMode );

			this.TypeSlider.SetValue( myitem.Def.Type );
			this.HueSlider.SetValue( hsl.X );
			this.SaturationSlider.SetValue( hsl.Y );
			this.LightnessSlider.SetValue( hsl.Z );
			this.WorldLightingFlag.Selected = myitem.Def.WorldLighting;

			return true;
		}

		////////////////

		public void ApplySettingsToCurrentItem() {
			if( this.HologramItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			var myitem = this.HologramItem.modItem as HologramItem;
			if( myitem == null ) {
				Main.NewText( "No hologram item selected. Changes not saved.", Color.Red );
				return;
			}

			myitem?.SetHologramDefinition( this.CreateHologramDefinition() );
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

			if( def.AnimateHologram(Main.MouseWorld, true) ) {
				def.DrawHologram( Main.MouseWorld, true );
			}
		}


		////////////////

		public HologramDefinition CreateHologramDefinition() {
			return new HologramDefinition(
				mode: (HologramMode)this.CurrentMode,
				type: (int)this.TypeSlider.RememberedInputValue,
				scale: this.ScaleSlider.RememberedInputValue,
				color: this.GetColor(),
				alpha: (byte)this.AlphaSlider.RememberedInputValue,
				direction: (int)this.DirectionSlider.RememberedInputValue,
				rotation: this.RotationSlider.RememberedInputValue,
				offsetX: (int)this.OffsetXSlider.RememberedInputValue,
				offsetY: (int)this.OffsetYSlider.RememberedInputValue,
				frameStart: (int)this.FrameStartSlider.RememberedInputValue,
				frameEnd: (int)this.FrameEndSlider.RememberedInputValue,
				frameRateTicks: (int)this.FrameRateTicksSlider.RememberedInputValue,
				worldLight: this.WorldLightingFlag.Selected,
				shaderMode: (HologramShaderMode)this.CurrentsShaderMode,
				shaderTime: this.ShadertTimeSlider.RememberedInputValue,
				shaderType: (int)this.ShaderTypeSlider.RememberedInputValue,
				isActivated: true
			);
		}
	}
}