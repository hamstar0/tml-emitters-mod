using Emitters.Definitions;
using Emitters.Items;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;


namespace Emitters.UI
{
	public enum HologramUITab {
		MainSettings,
		ColorSettings,
		ShaderSettings
	}
	public enum HologramUIMode {
		NPCMode,
		ItemMode,
		ProjectileMode
	}



	partial class UIHologramEditorDialog : UIDialog {
		public HologramUITab CurrentTab { get; private set; } = HologramUITab.MainSettings;
		public HologramUIMode CurrentMode { get; private set; } = HologramUIMode.NPCMode;

		////////////////
		
		private float TabStartHeight = 86f;

		////

		private UIThemedPanel MainTabElem;
		private UIThemedPanel ColorTabElem;
		private UIThemedPanel ShaderTabElem;

		//

		//private UISlider ModeSliderElem;
		private UICheckbox NpcModeCheckbox;
		private UICheckbox ItemModeCheckbox;
		private UICheckbox ProjectileModeCheckbox;
		private UISlider TypeSliderElem;
		private UISlider ScaleSliderElem;
		private UISlider DirectionSliderElem;
		private UISlider RotationSliderElem;
		private UISlider OffsetXSliderElem;
		private UISlider OffsetYSliderElem;
		private UISlider FrameStartSliderElem;
		private UISlider FrameEndSliderElem;
		private UISlider FrameRateTicksSliderElem;
		private UICheckbox WorldLightingCheckbox;

		//

		private UISlider HueSliderElem;
		private UISlider SaturationSliderElem;
		private UISlider LightnessSliderElem;
		private UISlider AlphaSliderElem;

		//

		private UISlider ShadertTimeSliderElem;
		private UICheckbox CRTEffectCheckbox;

		//
		private UITextPanelButton ApplyButton;
		private List<UIElement> HologramUIContainers = new List<UIElement>();
		////

		private Item HologramItem = null;



		////////////////

		public UIHologramEditorDialog() : base( UITheme.Vanilla, 600, 500 ) { }

		////////////////
		
		public HologramDefinition CreateHologramDefinition() {
			return new HologramDefinition(
				mode: (HologramMode)this.CurrentMode,
				type: (int)TypeSliderElem.RememberedInputValue,
				scale: ScaleSliderElem.RememberedInputValue,
				color: GetColor(),
				alpha: (byte)AlphaSliderElem.RememberedInputValue,
				direction: (int)DirectionSliderElem.RememberedInputValue,
				rotation: RotationSliderElem.RememberedInputValue,
				offsetX: (int)OffsetXSliderElem.RememberedInputValue,
				offsetY: (int)OffsetYSliderElem.RememberedInputValue,
				frameStart: (int)FrameStartSliderElem.RememberedInputValue,
				frameEnd: (int)FrameEndSliderElem.RememberedInputValue,
				frameRateTicks: (int)FrameRateTicksSliderElem.RememberedInputValue,
				worldLight: WorldLightingCheckbox.Selected,
				crtEffect: CRTEffectCheckbox.Selected,
				shaderTime: ShadertTimeSliderElem.RememberedInputValue,
				isActivated: true
			);
		}


		////////////////

		public Color GetColor() {
			float hue = HueSliderElem.RememberedInputValue;
			float saturation = SaturationSliderElem.RememberedInputValue;
			float lightness = LightnessSliderElem.RememberedInputValue;
			Color color = Main.hslToRgb( hue, saturation, lightness );
			return color;
		}

		internal bool SetItem( Item hologramItem ) {
			var myitem = hologramItem.modItem as HologramItem;
			if( myitem.Def == null ) {
				return false;
			}

			this.HologramItem = hologramItem;

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );
			myitem.Def.Mode = (HologramMode) CurrentMode;
			this.TypeSliderElem.SetValue( myitem.Def.Type );
			this.HueSliderElem.SetValue( hsl.X );
			this.SaturationSliderElem.SetValue( hsl.Y );
			this.LightnessSliderElem.SetValue( hsl.Z );
			this.WorldLightingCheckbox.Selected = myitem.Def.WorldLighting;
			this.CRTEffectCheckbox.Selected = myitem.Def.CrtEffect;

			return true;
		}

		////////////////

		public void ApplySettingsToCurrentItem() {
			if( this.HologramItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			HologramItem myitem = this.HologramItem.modItem as HologramItem;
			if( myitem == null ) {
				Main.NewText( "No hologram item selected. Changes not saved.", Color.Red );
				return;
			}

			myitem?.SetHologramDefinition( this.CreateHologramDefinition() );
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

			HologramDefinition def = CreateHologramDefinition();
			def.CurrentFrame = CachedHologramDef?.CurrentFrame ?? 0;
			def.CurrentFrameElapsedTicks = CachedHologramDef?.CurrentFrameElapsedTicks ?? 0;

			this.CachedHologramDef = def;

			def.AnimateHologram( Main.MouseWorld, true );
		}
	}
}