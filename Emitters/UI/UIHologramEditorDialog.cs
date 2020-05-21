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
	partial class UIHologramEditorDialog : UIDialog {
		private UISlider TypeSliderElem;
		private UISlider ScaleSliderElem;
		private UISlider HueSliderElem;
		private UISlider SaturationSliderElem;
		private UISlider LightnessSliderElem;
		private UISlider AlphaSliderElem;
		private UISlider DirectionSliderElem;
		private UISlider RotationSliderElem;
		private UISlider OffsetXSliderElem;
		private UISlider OffsetYSliderElem;
		private UISlider FrameSliderElem;
		private UICheckbox WorldLightingCheckbox;

		////

		private Item HologramItem = null;



		////////////////

		public UIHologramEditorDialog() : base( UITheme.Vanilla, 600, 424 ) { }


		////////////////

		public HologramDefinition CreateHologramDefinition() {
			return new HologramDefinition(
				type: (int)this.TypeSliderElem.RememberedInputValue,
				scale: this.ScaleSliderElem.RememberedInputValue,
				color: this.GetColor(),
				alpha: (byte)this.AlphaSliderElem.RememberedInputValue,
				direction: (int)this.DirectionSliderElem.RememberedInputValue,
				rotation: this.RotationSliderElem.RememberedInputValue,
				offsetX: (int)this.OffsetXSliderElem.RememberedInputValue,
				offsetY: (int)this.OffsetYSliderElem.RememberedInputValue,
				frame: (int)this.FrameSliderElem.RememberedInputValue,
				worldLight: this.WorldLightingCheckbox.Selected,
				isActivated: true
			);
		}


		////////////////

		public Color GetColor() {
			float hue = this.HueSliderElem.RememberedInputValue;
			float saturation = this.SaturationSliderElem.RememberedInputValue;
			float lightness = this.LightnessSliderElem.RememberedInputValue;
			Color color = Main.hslToRgb( hue, saturation, lightness );
			return color;
		}

		////////////////

		internal void SetItem( Item hologramItem ) {
			this.HologramItem = hologramItem;

			var myitem = hologramItem.modItem as HologramItem;
			if( myitem.Def == null ) {
				return;
			}

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );

			this.TypeSliderElem.SetValue( myitem.Def.Type.Type );
			this.ScaleSliderElem.SetValue( myitem.Def.Scale );
			this.HueSliderElem.SetValue( hsl.X );
			this.SaturationSliderElem.SetValue( hsl.Y );
			this.LightnessSliderElem.SetValue( hsl.Z );
			this.AlphaSliderElem.SetValue( myitem.Def.Alpha );
			this.DirectionSliderElem.SetValue( myitem.Def.Direction );
			this.RotationSliderElem.SetValue(myitem.Def.Rotation);
			this.OffsetXSliderElem.SetValue(myitem.Def.OffsetX);
			this.OffsetYSliderElem.SetValue(myitem.Def.OffsetY);
			this.FrameSliderElem.SetValue(myitem.Def.Frame);
			this.WorldLightingCheckbox.Selected = myitem.Def.WorldLighting;
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

		public override void Update( GameTime gameTime ) {
			base.Update( gameTime );
			Main.LocalPlayer.mouseInterface = true;
		}


		////////////////

		 private HologramDefinition CachedHologramDef = null;

		public override void Draw( SpriteBatch sb ) {
			base.Draw( sb );

			HologramDefinition def = this.CreateHologramDefinition();
			def.Timer = this.CachedHologramDef?.Timer ?? 0;

			this.CachedHologramDef = def;

			Main.instance.LoadNPC( def.Type.Type );

			def.AnimateHologram( Main.MouseWorld, true );
		}
	}
}
