using Terraria;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private void InitializeShadersTab( UIThemedPanel container ) {
			float yOffset = 0f;

			this.InitializeWidgetsforShaderTime( container, ref yOffset );
			this.InitializeWidgetsForCrtEffect( container, ref yOffset );

			yOffset += 16f;

			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsforShaderTime( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Cycle Seconds:", false, ref yOffset );

			this.ShadertTimeSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 60f,
				hideTextInput: false
			);
			this.ShadertTimeSlider.Top.Set( yOffset, 0f );
			this.ShadertTimeSlider.Left.Set( 96f, 0f );
			this.ShadertTimeSlider.Width.Set( -96f, 1f );
			this.ShadertTimeSlider.SetValue( 1f );
			container.Append( this.ShadertTimeSlider );

			yOffset += 28f;
		}

		private void InitializeWidgetsForCrtEffect( UIThemedPanel container, ref float yOffset ) {
			this.CRTEffectFlag = new UICheckbox( UITheme.Vanilla, "CRT Effect", "" );
			this.CRTEffectFlag.Top.Set( yOffset, 0f );
			this.CRTEffectFlag.Selected = false;
			this.CRTEffectFlag.OnSelectedChanged += () => { };
			container.Append( this.CRTEffectFlag );

			yOffset += 28f;
		}
	}
}
