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

		//private UISlider ModeSliderElem;
		private UICheckbox NpcModeChoice;
		private UICheckbox ItemModeChoice;
		private UICheckbox ProjectileModeChoice;
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

		private UISlider ShadertTimeSlider;
		private UICheckbox CRTEffectFlag;

		//
		private UITextPanelButton ApplyButton;


		////////////////

		private Item HologramItem = null;



		////////////////

		public UIHologramEditorDialog() : base( UITheme.Vanilla, 600, 500 ) { }

		////////////////
		
		public HologramDefinition CreateHologramDefinition() {
			return new HologramDefinition(
				mode: (HologramMode)this.CurrentMode,
				type: (int)TypeSlider.RememberedInputValue,
				scale: ScaleSlider.RememberedInputValue,
				color: GetColor(),
				alpha: (byte)AlphaSlider.RememberedInputValue,
				direction: (int)DirectionSlider.RememberedInputValue,
				rotation: RotationSlider.RememberedInputValue,
				offsetX: (int)OffsetXSlider.RememberedInputValue,
				offsetY: (int)OffsetYSlider.RememberedInputValue,
				frameStart: (int)FrameStartSlider.RememberedInputValue,
				frameEnd: (int)FrameEndSlider.RememberedInputValue,
				frameRateTicks: (int)FrameRateTicksSlider.RememberedInputValue,
				worldLight: WorldLightingFlag.Selected,
				crtEffect: CRTEffectFlag.Selected,
				shaderTime: ShadertTimeSlider.RememberedInputValue,
				isActivated: true
			);
		}


		////////////////

		public Color GetColor() {
			float hue = HueSlider.RememberedInputValue;
			float saturation = SaturationSlider.RememberedInputValue;
			float lightness = LightnessSlider.RememberedInputValue;
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
			myitem.Def.Mode = (HologramMode)this.CurrentMode;
			this.TypeSlider.SetValue( myitem.Def.Type );
			this.HueSlider.SetValue( hsl.X );
			this.SaturationSlider.SetValue( hsl.Y );
			this.LightnessSlider.SetValue( hsl.Z );
			this.WorldLightingFlag.Selected = myitem.Def.WorldLighting;
			this.CRTEffectFlag.Selected = myitem.Def.CrtEffect;

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

		public override void RecalculateMe() {
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

			this.OuterContainer.Top.Set( this.FullDialogHeight * -0.5f, 0.5f );
			this.OuterContainer.Height.Set( this.FullDialogHeight - this.MainTabHeight + tabHeight, 0f );

			base.RecalculateMe();
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

			if( def.AnimateHologram(Main.MouseWorld, true) ) {
				def.DrawHologram( Main.MouseWorld, true );
			}
		}
	}
}