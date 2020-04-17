using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.Debug;
using Emitters.Items;
using UISlider = Emitters.Libraries.Classes.UI.UISlider;


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
		private UISlider IntensitySliderElem;
		private UISlider AlphaSliderElem;
		private UISlider ScatterSliderElem;
		private UICheckbox HasGravityCheckbox;
		private UICheckbox HasLightCheckbox;

		////

		private Item EmitterItem = null;



		////////////////

		public UIEmitterEditorDialog() : base( UITheme.Vanilla, 600, 400 ) { }


		////////////////

		public EmitterDefinition CreateEmitterDefinition() {
			return new EmitterDefinition(
				isGoreMode: this.IsGoreMode,
				type: (int)this.TypeSliderElem.RememberedInputValue,
				scale: this.ScaleSliderElem.RememberedInputValue,
				delay: (int)this.DelaySliderElem.RememberedInputValue,
				speedX: this.SpeedXSliderElem.RememberedInputValue,
				speedY: this.SpeedYSliderElem.RememberedInputValue,
				color: this.GetColor(),
				alpha: (byte)this.AlphaSliderElem.RememberedInputValue,
				scatter: this.ScatterSliderElem.RememberedInputValue,
				hasGravity: this.HasGravityCheckbox.Selected,
				hasLight: this.HasLightCheckbox.Selected,
				isActivated: true
			);
		}


		////////////////

		public Color GetColor() {
			float hue = this.HueSliderElem.RememberedInputValue;
			float intensity = this.IntensitySliderElem.RememberedInputValue;

			Color color = Main.hslToRgb( hue, intensity, 0.5f );
			return color;
		}

		////////////////

		internal void SetItem( Item emitterItem ) {
			this.EmitterItem = emitterItem;

			var myitem = emitterItem.modItem as EmitterItem;
			if( myitem.Def == null ) {
				return;
			}

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );

			this.SetGoreMode( myitem.Def.IsGoreMode );
			this.TypeSliderElem.SetValue( myitem.Def.Type );
			this.ScaleSliderElem.SetValue( myitem.Def.Scale );
			this.DelaySliderElem.SetValue( myitem.Def.Delay );
			this.SpeedXSliderElem.SetValue( myitem.Def.SpeedX );
			this.SpeedYSliderElem.SetValue( myitem.Def.SpeedY );
			this.HueSliderElem.SetValue( hsl.X );
			this.IntensitySliderElem.SetValue( hsl.Y );
			this.AlphaSliderElem.SetValue( myitem.Def.Alpha );
			this.ScatterSliderElem.SetValue( myitem.Def.Scatter );
			this.HasGravityCheckbox.Selected = myitem.Def.HasGravity;
			this.HasLightCheckbox.Selected = myitem.Def.HasLight;
		}


		////////////////

		public void ApplySettingsToCurrentItem() {
			if( this.EmitterItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			var myitem = this.EmitterItem.modItem as EmitterItem;

			myitem.SetEmitterDefinition( this.CreateEmitterDefinition() );
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

			def.AnimateEmitter( Main.MouseWorld );
		}
	}
}
