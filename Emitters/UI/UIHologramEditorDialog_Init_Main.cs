using Terraria;
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

			container.Height.Set( yOffset, 0f );
		}

		////////////////
		/// 
		private void InitializeWidgetsForMode( UIThemedPanel container, ref float yOffset ) 
		{

			this.InitializeComponentForTitle( container, "Mode:", false, ref yOffset );

			this.NpcModeCheckbox = new UICheckbox( UITheme.Vanilla, "NPC", "" );
			this.NpcModeCheckbox.Top.Set( yOffset, 0f );
			this.NpcModeCheckbox.Left.Set( 64f, 0f );
			this.NpcModeCheckbox.Selected = true;
			this.NpcModeCheckbox.OnSelectedChanged += () => {
				this.UISetHologramMode( HologramUIMode.NPCMode );
			};

			this.ItemModeCheckbox = new UICheckbox( UITheme.Vanilla, "Item", "" );
			this.ItemModeCheckbox.Top.Set( yOffset, 0f );
			this.ItemModeCheckbox.Left.Set( 128f, 0f );
			this.ItemModeCheckbox.OnSelectedChanged += () => {
				this.UISetHologramMode( HologramUIMode.ItemMode );
			};

			this.ProjectileModeCheckbox = new UICheckbox( UITheme.Vanilla, "Projectile", "" );
			this.ProjectileModeCheckbox.Top.Set( yOffset, 0f );
			this.ProjectileModeCheckbox.Left.Set( 192f, 0f );
			this.ProjectileModeCheckbox.OnSelectedChanged += () => {
				this.UISetHologramMode( HologramUIMode.ProjectileMode );
			};
			yOffset += 28f;

			container.Append( (UIElement)this.NpcModeCheckbox );
			container.Append( (UIElement)this.ItemModeCheckbox );
			container.Append( (UIElement)this.ProjectileModeCheckbox );
		}

		private void InitializeWidgetsForType( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Type:", false, ref yOffset );
			this.TypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: Main.npcTexture.Length - 1 );
			this.TypeSliderElem.Top.Set( yOffset, 0f );
			this.TypeSliderElem.Left.Set( 64f, 0f );
			this.TypeSliderElem.Width.Set( -64f, 1f );
			this.TypeSliderElem.SetValue( 1f );
			this.TypeSliderElem.PreOnChange += ( value ) =>
			{
				var mode = (HologramMode) this.CurrentMode;
				this.FrameStartSliderElem.SetRange( 0f, (float)( HologramDefinition.GetFrameCount(mode, (int)value) - 1 ) );
				this.FrameEndSliderElem.SetRange( 0f, (float)( HologramDefinition.GetFrameCount(mode, (int)value) - 1 ) );
				return value;

			};
			yOffset += 28f;
			container.Append( (UIElement)this.TypeSliderElem );
		}

		private void InitializeWidgetsForScale( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Scale:", false, ref yOffset );

			this.ScaleSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 10f );
			this.ScaleSliderElem.Top.Set( yOffset, 0f );
			this.ScaleSliderElem.Left.Set( 64f, 0f );
			this.ScaleSliderElem.Width.Set( -64f, 1f );
			this.ScaleSliderElem.SetValue( 1f );
			yOffset += 28f;

			container.Append( (UIElement)this.ScaleSliderElem );
		}

		private void InitializeWidgetsForDirection( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Direction:", false, ref yOffset );

			this.DirectionSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -1f,
				maxRange: 1f );
			this.DirectionSliderElem.Top.Set( yOffset, 0f );
			this.DirectionSliderElem.Left.Set( 96f, 0f );
			this.DirectionSliderElem.Width.Set( -96f, 1f );
			this.DirectionSliderElem.SetValue( 1f );
			this.DirectionSliderElem.PreOnChange += ( value ) => {
				bool isRepeat = Timers.GetTimerTickDuration( "HologramDirectionRepeat" ) >= 1;
				Timers.SetTimer( "HologramDirectionRepeat", 2, true, () => false );
				if( isRepeat ) {
					return null;
				}

				if( this.DirectionSliderElem.RememberedInputValue < 0f && this.DirectionSliderElem.RememberedInputValue != value ) {
					value = 1f;
				} else {
					value = -1f;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.DirectionSliderElem );
		}

		private void InitializeWidgetsForRotation( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Rotation:", false, ref yOffset );

			this.RotationSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 360f );
			this.RotationSliderElem.Top.Set( yOffset, 0f );
			this.RotationSliderElem.Left.Set( 96f, 0f );
			this.RotationSliderElem.Width.Set( -96f, 1f );
			this.RotationSliderElem.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.RotationSliderElem );
		}

		private void InitializeWidgetsForOffsetX( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "X Offset:", false, ref yOffset );

			this.OffsetXSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f );    //15f
			this.OffsetXSliderElem.Top.Set( yOffset, 0f );
			this.OffsetXSliderElem.Left.Set( 96f, 0f );
			this.OffsetXSliderElem.Width.Set( -96f, 1f );
			this.OffsetXSliderElem.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.OffsetXSliderElem );
		}

		private void InitializeWidgetsForOffsetY( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Y Offset:", false, ref yOffset );

			this.OffsetYSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f );    //15f
			this.OffsetYSliderElem.Top.Set( yOffset, 0f );
			this.OffsetYSliderElem.Left.Set( 96f, 0f );
			this.OffsetYSliderElem.Width.Set( -96f, 1f );
			this.OffsetYSliderElem.SetValue( 0f );
			yOffset += 28f;

			container.Append( (UIElement)this.OffsetYSliderElem );
		}

		private void InitializeWidgetsForFrameStart( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Frame Start:", false, ref yOffset );

			this.FrameStartSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)( Main.npcFrameCount[1] - 1 ) );
			this.FrameStartSliderElem.Top.Set( yOffset, 0f );
			this.FrameStartSliderElem.Left.Set( 96f, 0f );
			this.FrameStartSliderElem.Width.Set( -96f, 1f );
			this.FrameStartSliderElem.SetValue( 0f );
			this.FrameStartSliderElem.PreOnChange += ( value ) => {
				if( value > this.FrameEndSliderElem.RememberedInputValue ) {
					value = this.FrameEndSliderElem.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.FrameStartSliderElem );
		}

		private void InitializeWidgetsForFrameEnd( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Frame End:", false, ref yOffset );

			this.FrameEndSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)( Main.npcFrameCount[1] - 1 ) );
			this.FrameEndSliderElem.Top.Set( yOffset, 0f );
			this.FrameEndSliderElem.Left.Set( 96f, 0f );
			this.FrameEndSliderElem.Width.Set( -96f, 1f );
			this.FrameEndSliderElem.SetValue( 0f );
			this.FrameEndSliderElem.PreOnChange += ( value ) => {
				if( value < this.FrameStartSliderElem.RememberedInputValue ) {
					value = this.FrameStartSliderElem.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			container.Append( (UIElement)this.FrameEndSliderElem );
		}

		private void InitializeWidgetsForFrameRateTicks( UIThemedPanel container, ref float yOffset ) {
			this.InitializeComponentForTitle( container, "Frame Rate:", false, ref yOffset );

			this.FrameRateTicksSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 60f * 5f );
			this.FrameRateTicksSliderElem.Top.Set( yOffset, 0f );
			this.FrameRateTicksSliderElem.Left.Set( 96f, 0f );
			this.FrameRateTicksSliderElem.Width.Set( -96f, 1f );
			this.FrameRateTicksSliderElem.SetValue( 8f );
			yOffset += 28f;

			container.Append( (UIElement)this.FrameRateTicksSliderElem );
		}

		private void InitializeWidgetsForWorldLighting( UIThemedPanel container, ref float yOffset ) {
			this.WorldLightingCheckbox = new UICheckbox( UITheme.Vanilla, "World Lighting", "" );
			this.WorldLightingCheckbox.Top.Set( yOffset, 0f );
			this.WorldLightingCheckbox.Selected = true;
			yOffset += 28f;

			container.Append( (UIElement)this.WorldLightingCheckbox );
		}

	}
}