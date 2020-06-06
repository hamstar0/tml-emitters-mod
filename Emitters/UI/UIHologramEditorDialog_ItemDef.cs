using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using Emitters.Definitions;
using Emitters.Items;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
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

			myitem?.SetHologramDefinition( this.CreateHologramDefinition() );
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