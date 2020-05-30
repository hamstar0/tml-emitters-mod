using Terraria;
using Terraria.UI;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		public override void InitializeComponents() {
			float yOffset = 0f;


			// UI Header
			var textElem = new UIThemedText( UITheme.Vanilla, false, "Adjust Hologram", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );

			yOffset += 50f;

			this.InitializeTabButtons( ref yOffset );

			this.TabStartHeight = yOffset;
			this.InitializeTabContainers( yOffset, out this.MainTabElem, out this.ColorTabElem, out this.ShaderTabElem );

			this.InitializeMainTab( this.MainTabElem );
			this.InitializeColorTab( this.ColorTabElem );
			//this.InitializeShadersTab( this.ShaderTabElem );

			yOffset += this.MainTabElem.Height.Pixels;

			// Apply button
			this.ApplyButton = new UITextPanelButton( UITheme.Vanilla, "Apply" );
			this.ApplyButton.Top.Set( yOffset - 28f, 0f );
			this.ApplyButton.Left.Set( -64f, 1f );
			this.ApplyButton.Height.Set( this.ApplyButton.GetOuterDimensions().Height + 4f, 0f );
			this.ApplyButton.OnClick += ( _, __ ) => {
				this.Close();
				this.ApplySettingsToCurrentItem();
			};

			this.InnerContainer.Append( (UIElement)this.ApplyButton );

			yOffset += 28;
			this.OuterContainer.Height.Set( yOffset, 0f );
			//this.OuterContainer.Top.Set( 108f, 0f );
		}


		////////////////

		private void InitializeTabButtons( ref float yOffset ) {
			// Main tab button
			var mainTabBut = new UITextPanelButton( UITheme.Vanilla, "Main Tab" );
			mainTabBut.Top.Set( yOffset, 0f );
			//mainTab.Left.Set(-235f, 1f);
			mainTabBut.Height.Set( mainTabBut.GetOuterDimensions().Height + 4f, 0f );
			mainTabBut.OnClick += ( _, __ ) => {
				this.SwitchTab( HologramUITab.MainSettings );
			};
			this.InnerContainer.Append( (UIElement)mainTabBut );

			// Color tab button
			var colorTabBut = new UITextPanelButton( UITheme.Vanilla, "Color Tab" );
			colorTabBut.Top.Set( yOffset, 0f );
			colorTabBut.Left.Set( -472f, 1f );
			colorTabBut.Height.Set( colorTabBut.GetOuterDimensions().Height - 4f, 0f );
			colorTabBut.OnClick += ( _, __ ) => {
				this.SwitchTab( HologramUITab.ColorSettings );
			};
			this.InnerContainer.Append( (UIElement)colorTabBut );

			yOffset += 36f;
		}


		private void InitializeTabContainers(
					float yOffset,
					out UIThemedPanel mainTab,
					out UIThemedPanel colorTab,
					out UIThemedPanel shaderTab ) {
			mainTab = new UIThemedPanel( UITheme.Vanilla, false );
			mainTab.Top.Set( yOffset, 0f );
			mainTab.Hide();
			this.InnerContainer.Append( (UIElement)mainTab );

			colorTab = new UIThemedPanel( UITheme.Vanilla, false );
			colorTab.Top.Set( yOffset, 0f );
			colorTab.Hide();
			this.InnerContainer.Append( (UIElement)colorTab );

			shaderTab = new UIThemedPanel( UITheme.Vanilla, false );
			shaderTab.Top.Set( yOffset, 0f );
			shaderTab.Hide();
			this.InnerContainer.Append( (UIElement)shaderTab );
		}


		////////////////

		private void InitializeComponentForTitle( UIElement container, string title, bool isNewLine, ref float yOffset ) {
			var textElem = new UIThemedText( UITheme.Vanilla, false, title );
			textElem.Top.Set( yOffset, 0f );

			if( isNewLine ) {
				yOffset += 28f;
			}

			container.Append( (UIElement)textElem );
		}
	}
}