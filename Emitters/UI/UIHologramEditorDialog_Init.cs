using Terraria.UI;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Timers;


namespace Emitters.UI {
	partial class UIHologramEditorDialog : UIDialog {
		public override void InitializeComponents() {
			float yOffset = 0f;

			// UI Header
			var textElem = new UIThemedText( UITheme.Vanilla, false, "Adjust Hologram", 1f, true );
			this.InnerContainer.Append( (UIElement)textElem );

			yOffset += 50f;

			this.InitializeTabButtons( ref yOffset );

			this.TabStartInnerHeight = yOffset;

			//

			this.InitializeTabContainers( yOffset, out this.MainTabContainer, out this.ColorTabContainer, out this.ShaderTabContainer );

			this.InitializeMainTab( this.MainTabContainer );
			this.InitializeColorTab( this.ColorTabContainer );
			this.InitializeShadersTab( this.ShaderTabContainer );

			this.MainTabHeight = this.MainTabContainer.Height.Pixels;
			this.ColorTabHeight = this.ColorTabContainer.Height.Pixels;
			this.ShaderTabHeight = this.ShaderTabContainer.Height.Pixels;

			yOffset += this.MainTabHeight;

			// Apply button
			this.ApplyButton = new UITextPanelButton( UITheme.Vanilla, "Apply" );
			this.ApplyButton.Top.Set( -22f, 1f );
			this.ApplyButton.Left.Set( -52f, 1f );
			this.ApplyButton.OnClick += ( _, __ ) => {
				this.Close();
				this.ApplySettingsToCurrentItem();
			};
			this.InnerContainer.Append( (UIElement)this.ApplyButton );

			yOffset += this.ApplyButton.GetOuterDimensions().Height;

			//

			this.FullDialogHeight = yOffset + 24f;

			//

			this.SwitchTab( HologramUITab.Main );
			Timers.SetTimer( 1, true, () => {
				this.SwitchTab( HologramUITab.Main );
				return false;
			} );
		}


		////////////////

		private void InitializeTabButtons( ref float yOffset ) {
			// Main tab button
			var mainTabBut = new UITextPanelButton( UITheme.Vanilla, "Main Tab" );
			mainTabBut.Top.Set( yOffset, 0f );
			//mainTabBut.Left.Set(-235f, 1f);
			mainTabBut.Height.Set( mainTabBut.GetOuterDimensions().Height + 4f, 0f );
			mainTabBut.OnClick += ( _, __ ) => {
				this.SwitchTab( HologramUITab.Main );
			};
			this.InnerContainer.Append( (UIElement)mainTabBut );

			// Color tab button
			var colorTabBut = new UITextPanelButton( UITheme.Vanilla, "Color Tab" );
			colorTabBut.Top.Set( yOffset, 0f );
			colorTabBut.Left.Set( -472f, 1f );
			colorTabBut.Height.Set( colorTabBut.GetOuterDimensions().Height - 4f, 0f );
			colorTabBut.OnClick += ( _, __ ) => {
				this.SwitchTab( HologramUITab.Color );
			};
			this.InnerContainer.Append( (UIElement)colorTabBut );

			// Shader tab button
			var shaderTabBut = new UITextPanelButton( UITheme.Vanilla, "Shader Tab" );
			shaderTabBut.Top.Set( yOffset, 0f );
			shaderTabBut.Left.Set( -372f, 1f );
			shaderTabBut.Height.Set( shaderTabBut.GetOuterDimensions().Height - 4f, 0f );
			shaderTabBut.OnClick += ( _, __ ) => {
				this.SwitchTab( HologramUITab.Shader );
			};
			this.InnerContainer.Append( (UIElement)shaderTabBut );

			yOffset += 36;

		}


		private void InitializeTabContainers(
					float yOffset,
					out UIThemedPanel mainTab,
					out UIThemedPanel colorTab,
					out UIThemedPanel shaderTab ) {
			mainTab = new UIThemedPanel( UITheme.Vanilla, false );
			mainTab.Width.Set( 0f, 1f );
			mainTab.Top.Set( yOffset, 0f );
			//mainTab.Hide();
			this.InnerContainer.Append( (UIElement)mainTab );

			colorTab = new UIThemedPanel( UITheme.Vanilla, false );
			colorTab.Width.Set( 0f, 1f );
			colorTab.Top.Set( yOffset, 0f );
			//colorTab.Hide();
			this.InnerContainer.Append( (UIElement)colorTab );

			shaderTab = new UIThemedPanel( UITheme.Vanilla, false );
			shaderTab.Width.Set( 0f, 1f );
			shaderTab.Top.Set( yOffset, 0f );
			//shaderTab.Hide();
			this.InnerContainer.Append( (UIElement)shaderTab );
		}


		////////////////

		private void InitializeTitle( UIElement container, string title, bool isNewLine, ref float yOffset ) {
			var textElem = new UIThemedText( UITheme.Vanilla, false, title );
			textElem.Top.Set( yOffset, 0f );

			if( isNewLine ) {
				yOffset += 28f;
			}

			container.Append( (UIElement)textElem );
		}
	}
}