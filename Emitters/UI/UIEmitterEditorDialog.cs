﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.Errors;
using Emitters.Items;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		private bool IsGoreMode = false;

		////

		private UISlider TypeSliderElem;
		private UISlider ScaleSliderElem;
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

		public UIEmitterEditorDialog() : base( UITheme.Vanilla, 480, 368 ) { }


		////////////////

		public Color GetColor() {
			float hue = this.HueSliderElem.RememberedInputValue;
			float intensity = this.IntensitySliderElem.RememberedInputValue;
			
			return Main.hslToRgb( hue, intensity, 1f );//0.5f?
		}


		////////////////

		internal void SetItem( Item emitterItem ) {
			this.EmitterItem = emitterItem;

			var myitem = emitterItem.modItem as EmitterItem;
			if( myitem.Def == null ) {
				return;
			}

			Vector3 hsl = Main.rgbToHsl( myitem.Def.Color );

			this.TypeSliderElem.SetValue( myitem.Def.Type );
			this.ScaleSliderElem.SetValue( myitem.Def.Scale );
			this.SpeedXSliderElem.SetValue( myitem.Def.SpeedX );
			this.SpeedYSliderElem.SetValue( myitem.Def.SpeedY );
			this.HueSliderElem.SetValue( (float)hsl.X );
			this.IntensitySliderElem.SetValue( (float)hsl.Y );
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

			myitem.SetEmitterDefinition( new EmitterDefinition {
				IsGoreMode = this.IsGoreMode,
				Type = (int)this.TypeSliderElem.RememberedInputValue,
				Scale = this.ScaleSliderElem.RememberedInputValue,
				SpeedX = this.SpeedXSliderElem.RememberedInputValue,
				SpeedY = this.SpeedYSliderElem.RememberedInputValue,
				Color = this.GetColor(),
				Alpha = this.AlphaSliderElem.RememberedInputValue,
				Scatter = this.ScatterSliderElem.RememberedInputValue,
				HasGravity = this.HasGravityCheckbox.Selected,
				HasLight = this.HasLightCheckbox.Selected,
			} );
		}
	}
}
