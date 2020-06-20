using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using Emitters.Definitions;
using Emitters.Items;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		internal void SetItem( Item hologramItem ) {
			var def = BaseEmitterDefinition.CreateOrGetDefForItem<HologramDefinition>( hologramItem );

			this.HologramItem = hologramItem;

			Vector3 hsl = Main.rgbToHsl( def.Color );

			 this.SetHologramMode( def.Mode );
			this.TypeSlider.SetValue( def.Type );
			this.ScaleSlider.SetValue( def.Scale );
			this.DirectionSlider.SetValue( def.Direction );
			this.RotationSlider.SetValue( def.Rotation );
			this.OffsetXSlider.SetValue( def.OffsetX );
			this.OffsetYSlider.SetValue( def.OffsetY );
			this.FrameStartSlider.SetValue( def.FrameStart );
			this.FrameEndSlider.SetValue( def.FrameEnd );
			this.FrameRateTicksSlider.SetValue( def.FrameRateTicks );
			this.WorldLightingFlag.Selected = def.WorldLighting;

			this.HueSlider.SetValue( hsl.X );
			this.SaturationSlider.SetValue( hsl.Y );
			this.LightnessSlider.SetValue( hsl.Z );
			this.AlphaSlider.SetValue( def.Alpha );

			 this.SetHologramShaderMode( def.ShaderMode );
			this.ShaderTypeSlider.SetValue( def.ShaderType );
			this.ShadertTimeSlider.SetValue( def.ShaderTime );
		}


		////

		public void ApplySettingsToCurrentItem() {
			if( this.HologramItem == null ) {
				throw new ModHelpersException( "Missing item." );
			}

			var myitem = this.HologramItem.modItem as HologramItem;
			if( myitem == null ) {
				Main.NewText( "No hologram item selected. Changes not saved.", Color.Red );
				return;
			}

			myitem.SetDefinition( this.CreateHologramDefinition() );
		}


		////////////////

		public HologramDefinition CreateHologramDefinition() {
			var mode = (HologramMode)this.CurrentMode;

			return new HologramDefinition(
				mode: mode,
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