using Terraria;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using Terraria;
using Terraria.UI;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		private void InitializeShadersTab( UIThemedPanel container ) {
			float yOffset = 0f;
			this.InitializeWidgetsforShaderType(container, ref yOffset );
			this.InitializeWidgetsforShaderTime(container, ref yOffset );
			this.InitializeWidgetsForShaderMode( container, ref yOffset );
			
			yOffset += 16f;
			container.Height.Set( yOffset, 0f );
		}


		////////////////
		private void InitializeWidgetsforShaderType( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Shader Type:", false, ref yOffset );

			this.ShaderTypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange:  EmittersMod.Instance.ShaderData.Count-1,
				hideTextInput: false
			);
			this.ShaderTypeSliderElem.Top.Set( yOffset, 0f );
			this.ShaderTypeSliderElem.Left.Set( 128f, 0f );
			this.ShaderTypeSliderElem.Width.Set( -96f, 1f );
			this.ShaderTypeSliderElem.SetValue(0f);
			container.Append( this.ShaderTypeSliderElem );

			yOffset += 28f;
		}

		private void InitializeWidgetsforShaderTime( UIThemedPanel container, ref float yOffset ) {
			this.InitializeTitle( container, "Cycle Seconds:", false, ref yOffset );

			this.ShadertTimeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -8f,
				maxRange: 8f,
				hideTextInput: false
			);
			this.ShadertTimeSliderElem.Top.Set( yOffset, 0f );
			this.ShadertTimeSliderElem.Left.Set( 96f, 0f );
			this.ShadertTimeSliderElem.Width.Set( -96f, 1f );
			this.ShadertTimeSliderElem.SetValue( 1f );
			container.Append( this.ShadertTimeSliderElem );

			yOffset += 28f;
		}

	
		private void InitializeWidgetsForShaderMode( UIThemedPanel container, ref float yOffset ) 
		{

			this.InitializeTitle( container, "Shader Mode:", false, ref yOffset );

			
			this.NoShaderCheckbox = new UICheckbox( UITheme.Vanilla, "None", "" );
			this.NoShaderCheckbox.Top.Set( yOffset, 0f );
			this.NoShaderCheckbox.Left.Set( 128f, 0f );
			this.NoShaderCheckbox.OnSelectedChanged += () => {
				this.UISetHologramShaderMode( HologramUIShaderMode.NoShader );
			};

			this.VanillaShadersCheckbox = new UICheckbox( UITheme.Vanilla, "Vanilla", "" );
			this.VanillaShadersCheckbox.Top.Set( yOffset, 0f );
			this.VanillaShadersCheckbox.Left.Set( 228f, 0f );
			this.VanillaShadersCheckbox.Selected = true;
			this.VanillaShadersCheckbox.OnSelectedChanged += () => {
				this.UISetHologramShaderMode( HologramUIShaderMode.VanillaMode );
				this.ShadertTimeSliderElem.SetRange(-8f,8f);
				this.ShaderTypeSliderElem.SetRange(0f,EmittersMod.Instance.ShaderData.Count-1);
				this.ShadertTimeSliderElem.SetValue(1f);
				this.ShaderTypeSliderElem.SetValue(0f);
			};

			this.CustomShadersCheckbox = new UICheckbox( UITheme.Vanilla, "Custom", "" );
			this.CustomShadersCheckbox.Top.Set( yOffset, 0f );
			this.CustomShadersCheckbox.Left.Set( 328f, 0f );
			this.CustomShadersCheckbox.OnSelectedChanged += () => {
				this.UISetHologramShaderMode( HologramUIShaderMode.CustomMode );
				this.ShadertTimeSliderElem.SetRange(-0.01f,60f);
				this.ShaderTypeSliderElem.SetRange(0f,0f);
				this.ShadertTimeSliderElem.SetValue(1f);
				this.ShaderTypeSliderElem.SetValue(0f);
			};
			yOffset += 28f;
			container.Append((UIElement)this.NoShaderCheckbox);
			container.Append( (UIElement)this.VanillaShadersCheckbox );
			container.Append( (UIElement)this.CustomShadersCheckbox );

		}

	}
}
