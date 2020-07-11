﻿using Terraria;
using Terraria.UI;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Services.Timers;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private void InitializeMainTab( UIThemedPanel container ) {
			float yOffset = 0f;

			this.InitializeWidgetsForMode( container, ref yOffset );
			this.InitializeWidgetsForType( container, ref yOffset );
			this.InitializeWidgetsForScale( container, ref yOffset );
			this.InitializeWidgetsForDirection( container, ref yOffset );
			this.InitializeWidgetsForRotation( container, ref yOffset );
			this.InitializeWidgetsForOffsetX( container, ref yOffset );
			this.InitializeWidgetsForOffsetY( container, ref yOffset );
			this.InitializeWidgetsForFrameStart( container, ref yOffset );
			this.InitializeWidgetsForFrameEnd( container, ref yOffset );
			this.InitializeWidgetsForFrameRateTicks( container, ref yOffset );
			this.InitializeWidgetsForWorldLighting( container, ref yOffset );

			yOffset += 16f;

			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsForMode( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Mode:", false, ref yOffset );

			this.ModeNpcChoice = new UICheckbox( UITheme.Vanilla, "NPC", "" );
			this.ModeNpcChoice.Top.Set( yOffset, 0f );
			this.ModeNpcChoice.Left.Set( 64f, 0f );
			this.ModeNpcChoice.Selected = true;
			this.ModeNpcChoice.OnSelectedChanged += () => {
				this.SetHologramMode( HologramMode.NPC );
			};

			this.ModeItemChoice = new UICheckbox( UITheme.Vanilla, "Item", "" );
			this.ModeItemChoice.Top.Set( yOffset, 0f );
			this.ModeItemChoice.Left.Set( 128f, 0f );
			this.ModeItemChoice.OnSelectedChanged += () => {
				this.SetHologramMode( HologramMode.Item );
			};

			this.ModeProjectileChoice = new UICheckbox( UITheme.Vanilla, "Projectile", "" );
			this.ModeProjectileChoice.Top.Set( yOffset, 0f );
			this.ModeProjectileChoice.Left.Set( 192f, 0f );
			this.ModeProjectileChoice.OnSelectedChanged += () => {
				this.SetHologramMode( HologramMode.Projectile );
			};

			this.ModeGoreChoice = new UICheckbox( UITheme.Vanilla, "Gore", "" );
			this.ModeGoreChoice.Top.Set( yOffset, 0f );
			this.ModeGoreChoice.Left.Set( 288, 0f );
			this.ModeGoreChoice.OnSelectedChanged += () => {
				this.SetHologramMode( HologramMode.Gore );
			};
			yOffset += 28f;

			container.Append( (UIElement)this.ModeNpcChoice );
			container.Append( (UIElement)this.ModeItemChoice );
			container.Append( (UIElement)this.ModeProjectileChoice );
			container.Append( (UIElement)this.ModeGoreChoice );
		}

		private void InitializeWidgetsForType( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Type:", false, ref yOffset );
			this.TypeSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: Main.npcTexture.Length - 1 );
			this.TypeSlider.Top.Set( yOffset, 0f );
			this.TypeSlider.Left.Set( 64f, 0f );
			this.TypeSlider.Width.Set( -64f, 1f );
			this.TypeSlider.SetValue( 1f );
			this.TypeSlider.PreOnChange += ( value ) => {
				var mode = (HologramMode) this.CurrentMode;
				this.FrameStartSlider.SetRange( 0f, (float)( HologramDefinition.GetFrameCount(mode, (int)value) - 1 ) );
				this.FrameEndSlider.SetRange( 0f, (float)( HologramDefinition.GetFrameCount(mode, (int)value) - 1 ) );
				return value;

			};
			yOffset += 28f;
			container.Append( (UIElement)this.TypeSlider );
		}

		private void InitializeWidgetsForScale( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Scale:", false, ref yOffset );

			this.ScaleSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 10f );
			this.ScaleSlider.Top.Set( yOffset, 0f );
			this.ScaleSlider.Left.Set( 64f, 0f );
			this.ScaleSlider.Width.Set( -64f, 1f );
			this.ScaleSlider.SetValue( 1f );
			yOffset += 28f;

			container.Append( (UIElement)this.ScaleSlider );
		}

		private void InitializeWidgetsForDirection( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Direction:", false, ref yOffset );

			this.DirectionSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -1f,
				maxRange: 1f );
			this.DirectionSlider.Top.Set( yOffset, 0f );
			this.DirectionSlider.Left.Set( 96f, 0f );
			this.DirectionSlider.Width.Set( -96f, 1f );
			this.DirectionSlider.SetValue( 1f );
			this.DirectionSlider.PreOnChange += ( value ) => {
				bool isRepeat = Timers.GetTimerTickDuration( "HologramDirectionRepeat" ) >= 1;
				Timers.SetTimer( "HologramDirectionRepeat", 2, true, () => false );
				if( isRepeat ) {
					return null;
				}

				if( this.DirectionSlider.RememberedInputValue < 0f && this.DirectionSlider.RememberedInputValue != value ) {
					value = 1f;
				} else {
					value = -1f;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.DirectionSlider );
		}

		private void InitializeWidgetsForRotation( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Rotation:", false, ref yOffset );

			this.RotationSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 360f );
			this.RotationSlider.Top.Set( yOffset, 0f );
			this.RotationSlider.Left.Set( 96f, 0f );
			this.RotationSlider.Width.Set( -96f, 1f );
			this.RotationSlider.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.RotationSlider );
		}

		private void InitializeWidgetsForOffsetX( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "X Offset:", false, ref yOffset );

			this.OffsetXSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f );    //15f
			this.OffsetXSlider.Top.Set( yOffset, 0f );
			this.OffsetXSlider.Left.Set( 96f, 0f );
			this.OffsetXSlider.Width.Set( -96f, 1f );
			this.OffsetXSlider.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.OffsetXSlider );
		}

		private void InitializeWidgetsForOffsetY( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Y Offset:", false, ref yOffset );

			this.OffsetYSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f );    //15f
			this.OffsetYSlider.Top.Set( yOffset, 0f );
			this.OffsetYSlider.Left.Set( 96f, 0f );
			this.OffsetYSlider.Width.Set( -96f, 1f );
			this.OffsetYSlider.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.OffsetYSlider );
		}

		private void InitializeWidgetsForFrameStart( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Frame Start:", false, ref yOffset );

			this.FrameStartSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)( Main.npcFrameCount[1] - 1 ) );
			this.FrameStartSlider.Top.Set( yOffset, 0f );
			this.FrameStartSlider.Left.Set( 96f, 0f );
			this.FrameStartSlider.Width.Set( -96f, 1f );
			this.FrameStartSlider.SetValue( 0f );
			this.FrameStartSlider.PreOnChange += ( value ) => {
				if( value > this.FrameEndSlider.RememberedInputValue ) {
					value = this.FrameEndSlider.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.FrameStartSlider );
		}

		private void InitializeWidgetsForFrameEnd( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Frame End:", false, ref yOffset );

			this.FrameEndSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)( Main.npcFrameCount[1] - 1 ) );
			this.FrameEndSlider.Top.Set( yOffset, 0f );
			this.FrameEndSlider.Left.Set( 96f, 0f );
			this.FrameEndSlider.Width.Set( -96f, 1f );
			this.FrameEndSlider.SetValue( 0f );
			this.FrameEndSlider.PreOnChange += ( value ) => {
				if( value < this.FrameStartSlider.RememberedInputValue ) {
					value = this.FrameStartSlider.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.FrameEndSlider );
		}

		private void InitializeWidgetsForFrameRateTicks( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Frame Rate:", false, ref yOffset );

			this.FrameRateTicksSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 60f * 5f );
			this.FrameRateTicksSlider.Top.Set( yOffset, 0f );
			this.FrameRateTicksSlider.Left.Set( 96f, 0f );
			this.FrameRateTicksSlider.Width.Set( -96f, 1f );
			this.FrameRateTicksSlider.SetValue( 8f );
			yOffset += 28f;

			container.Append( (UIElement)this.FrameRateTicksSlider );
		}

		private void InitializeWidgetsForWorldLighting( UIThemedPanel container, ref float yOffset ) {
			this.WorldLightingFlag = new UICheckbox( UITheme.Vanilla, "World Lighting", "" );
			this.WorldLightingFlag.Top.Set( yOffset, 0f );
			this.WorldLightingFlag.Selected = true;
			yOffset += 28f;

			container.Append( (UIElement)this.WorldLightingFlag );
		}
	}
}