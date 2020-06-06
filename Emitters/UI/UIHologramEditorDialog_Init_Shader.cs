using Terraria;
using Terraria.UI;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Emitters.Definitions;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private void InitializeShadersTab( UIThemedPanel container ) {
			float yOffset = 0f;
			this.InitializeWidgetsforShaderType( container, ref yOffset );
			this.InitializeWidgetsforShaderTime( container, ref yOffset );
			this.InitializeWidgetsForShaderMode( container, ref yOffset );

			yOffset += 16f;
			container.Height.Set( yOffset, 0f );
		}


		////////////////

		private void InitializeWidgetsforShaderType( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Shader Type:", false, ref yOffset );

			this.ShaderTypeSlider = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: EmittersMod.Instance.MyArmorShaders.Count - 1,
				hideTextInput: false
			);
			this.ShaderTypeSlider.Top.Set( yOffset, 0f );
			this.ShaderTypeSlider.Left.Set( 128f, 0f );
			this.ShaderTypeSlider.Width.Set( -128f, 1f );
			this.ShaderTypeSlider.SetValue( 0f );
			container.Append( this.ShaderTypeSlider );

			yOffset += 28f;
		}

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
			this.ShadertTimeSlider.Left.Set( 128f, 0f );
			this.ShadertTimeSlider.Width.Set( -128f, 1f );
			this.ShadertTimeSlider.SetValue( 1f );
			container.Append( this.ShadertTimeSlider );

			yOffset += 28f;
		}


		private void InitializeWidgetsForShaderMode( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Shader Mode:", false, ref yOffset );

			this.ShaderNoneChoice = new UICheckbox( UITheme.Vanilla, "None", "" );
			this.ShaderNoneChoice.Top.Set( yOffset, 0f );
			this.ShaderNoneChoice.Left.Set( 128f, 0f );
			this.ShaderNoneChoice.Selected = true;
			this.ShaderNoneChoice.OnSelectedChanged += () => {
				this.SetHologramShaderMode( HologramShaderMode.None );
				this.ShaderTypeSlider.SetRange( 0f, 0f );
				this.ShaderTypeSlider.SetValue( 0f );
			};

			this.ShaderVanillaChoice = new UICheckbox( UITheme.Vanilla, "Vanilla", "" );
			this.ShaderVanillaChoice.Top.Set( yOffset, 0f );
			this.ShaderVanillaChoice.Left.Set( 228f, 0f );
			this.ShaderVanillaChoice.Selected = true;
			this.ShaderVanillaChoice.OnSelectedChanged += () => {
				this.SetHologramShaderMode( HologramShaderMode.Vanilla );
				this.ShaderTypeSlider.SetRange( 0f, EmittersMod.Instance.MyArmorShaders.Count - 1 );    //ArmorShaders.Count?
				this.ShaderTypeSlider.SetValue( 0f );
			};

			this.ShaderCustomChoice = new UICheckbox( UITheme.Vanilla, "Custom", "" );
			this.ShaderCustomChoice.Top.Set( yOffset, 0f );
			this.ShaderCustomChoice.Left.Set( 328f, 0f );
			this.ShaderCustomChoice.OnSelectedChanged += () => {
				this.SetHologramShaderMode( HologramShaderMode.Custom );
				this.ShaderTypeSlider.SetRange( 0f, 0f );
				this.ShaderTypeSlider.SetValue( 0f );
			};

			container.Append( (UIElement)this.ShaderNoneChoice );
			container.Append( (UIElement)this.ShaderVanillaChoice );
			container.Append( (UIElement)this.ShaderCustomChoice );

			yOffset += 28f;
		}
	}
}
