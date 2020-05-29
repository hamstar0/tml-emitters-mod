using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Services.Timers;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;


namespace Emitters.UI
{
	partial class UIHologramEditorDialog : UIDialog
	{

		float yOffset = 0f;
		float yOffsetColorPanel = 86f;
		public override void InitializeComponents()
		{

			//UI Header
			var self = this;
			var textElem = new UIThemedText(UITheme.Vanilla, false, "Adjust Hologram", 1f, true);
			this.InnerContainer.Append((UIElement)textElem);

			yOffset += 50f;

			//Main tab button
			var mainTab = new UITextPanelButton(UITheme.Vanilla, "Main Tab");
			mainTab.Top.Set(yOffset, 0f);
			//mainTab.Left.Set(-235f, 1f);
			mainTab.Height.Set(mainTab.GetOuterDimensions().Height + 4f, 0f);
			mainTab.OnClick += (_, __) =>
			{
				if (!UIModeActive[1])
				{
					switchUIState(hologramUIMode.MainSettings);
				}
			};
			this.InnerContainer.Append((UIElement)mainTab);

			//Color tab button
			var colorTab = new UITextPanelButton(UITheme.Vanilla, "Color Tab");
			colorTab.Top.Set(yOffset, 0f);
			colorTab.Left.Set(-472f, 1f);
			colorTab.Height.Set(colorTab.GetOuterDimensions().Height - 4f, 0f);
			colorTab.OnClick += (_, __) =>
			{
				if (!UIModeActive[2])
				{
					switchUIState(hologramUIMode.ColorSettings);
				}
			};
			this.InnerContainer.Append((UIElement)colorTab);

			yOffset += 36f;

			//initialize elements
			this.InitializeWidgetsForMode(ref yOffset);
			this.InitializeWidgetsForType(ref yOffset);
			this.InitializeWidgetsForScale(ref yOffset);
			this.InitializeWidgetsForColor(ref yOffsetColorPanel);
			this.InitializeWidgetsForAlpha(ref yOffsetColorPanel);
			this.InitializeWidgetsForDirection(ref yOffset);
			this.InitializeWidgetsForRotation(ref yOffset);
			this.InitializeWidgetsForOffsetX(ref yOffset);
			this.InitializeWidgetsForOffsetY(ref yOffset);
			this.InitializeWidgetsForFrameStart(ref yOffset);
			this.InitializeWidgetsForFrameEnd(ref yOffset);
			this.InitializeWidgetsForFrameRateTicks(ref yOffset);
			this.InitializeWidgetsForWorldLighting(ref yOffset);
			this.InitializeWidgetsForCrtEffect(ref yOffset);

			Debug.WriteLine("InitialDisable");
			//Disable elements not in the main tab
			for (int i = 3; i <= 6; i++)
			{
				HologramUiTexts[i].RemoveChild(HologramUiTexts[i]);
				HologramUiTexts[i].Hide();
			}
			UIModeActive[1] = true;

			//Apply button
			ApplyButton = new UITextPanelButton(UITheme.Vanilla, "Apply");
			ApplyButton.Top.Set(yOffset - 28f, 0f);
			ApplyButton.Left.Set(-64f, 1f);
			ApplyButton.Height.Set(ApplyButton.GetOuterDimensions().Height + 4f, 0f);
			ApplyButton.OnClick += (_, __) =>
			{
				self.Close();
				self.ApplySettingsToCurrentItem();
			};

			this.InnerContainer.Append((UIElement)ApplyButton);

			yOffset += 28;
			OuterContainer.Height.Set(yOffset, 0f);
			//OuterContainer.Top.Set(108f, 0f);
		}


		////////////////

		private void InitializeComponentForTitle(string title, bool isNewLine, ref float yOffset)
		{
			var textElem = new UIThemedText(UITheme.Vanilla, false, title);
			textElem.Top.Set(yOffset, 0f);

			if (isNewLine)
			{
				yOffset += 28f;
			}

			this.InnerContainer.Append((UIElement)textElem);
			this.HologramUiTexts.Add(textElem);
		}

		////
		private void InitializeWidgetsForMode(ref float yOffset)
		{
			this.InitializeComponentForTitle("Mode:", false, ref yOffset);

			this.ModeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 3f);
			this.ModeSliderElem.Top.Set(yOffset, 0f);
			this.ModeSliderElem.Left.Set(64f, 0f);
			this.ModeSliderElem.Width.Set(-64f, 1f);
			this.ModeSliderElem.SetValue(1f);
			this.ModeSliderElem.PreOnChange += (value) =>
			{
				this.SetHologramMode((int)value);
				return value;
			};
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.ModeSliderElem);
			HologramElements.Add(this.ModeSliderElem);
		}

		private void InitializeWidgetsForType(ref float yOffset)
		{
			this.InitializeComponentForTitle("Type:", false, ref yOffset);
			this.TypeSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: Main.npcTexture.Length - 1);
			this.TypeSliderElem.Top.Set(yOffset, 0f);
			this.TypeSliderElem.Left.Set(64f, 0f);
			this.TypeSliderElem.Width.Set(-64f, 1f);
			this.TypeSliderElem.SetValue(1f);
			this.TypeSliderElem.PreOnChange += (value) =>
			{
				int mode = (int)ModeSliderElem.RememberedInputValue;
				if (TypeSliderElem.RememberedInputValue > NPCLoader.NPCCount)
				{
					return NPCLoader.NPCCount;
				}
				this.FrameStartSliderElem.SetRange(0f, (float)(EmitterUtils.GetFrameCount(mode, (int)value) - 1));
				this.FrameEndSliderElem.SetRange(0f, (float)(EmitterUtils.GetFrameCount(mode, (int)value) - 1)); 
				return value;

			};
			yOffset += 28f;
			this.InnerContainer.Append((UIElement)this.TypeSliderElem);
			HologramElements.Add(this.TypeSliderElem);
		}

		private void InitializeWidgetsForScale(ref float yOffset)
		{
			this.InitializeComponentForTitle("Scale:", false, ref yOffset);

			this.ScaleSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0.01f,
				maxRange: 10f);
			this.ScaleSliderElem.Top.Set(yOffset, 0f);
			this.ScaleSliderElem.Left.Set(64f, 0f);
			this.ScaleSliderElem.Width.Set(-64f, 1f);
			this.ScaleSliderElem.SetValue(1f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.ScaleSliderElem);
			HologramElements.Add(this.ScaleSliderElem);
		}

		private void InitializeWidgetsForColor(ref float yOffsetColorPanel)
		{
			this.InitializeComponentForTitle("Hue:", false, ref yOffsetColorPanel);

			this.HueSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false,
				innerBarShader: DelegateMethods.ColorLerp_HSL_H);
			this.HueSliderElem.Top.Set(yOffsetColorPanel, 0f);
			this.HueSliderElem.Left.Set(96f, 0f);
			this.HueSliderElem.Width.Set(-96f, 1f);
			this.HueSliderElem.SetValue(.53f);

			yOffsetColorPanel += 28f;

			this.InitializeComponentForTitle("Saturation:", false, ref yOffsetColorPanel);

			this.SaturationSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false);
			this.SaturationSliderElem.Top.Set(yOffsetColorPanel, 0f);
			this.SaturationSliderElem.Left.Set(96f, 0f);
			this.SaturationSliderElem.Width.Set(-96f, 1f);
			this.SaturationSliderElem.SetValue(1f);

			yOffsetColorPanel += 28f;

			this.InitializeComponentForTitle("Lightness:", false, ref yOffsetColorPanel);

			this.LightnessSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 1f,
				hideTextInput: false);
			this.LightnessSliderElem.Top.Set(yOffsetColorPanel, 0f);
			this.LightnessSliderElem.Left.Set(96f, 0f);
			this.LightnessSliderElem.Width.Set(-96f, 1f);
			this.LightnessSliderElem.SetValue(0.5f);

			yOffsetColorPanel += 28f;

			HologramElements.Add(this.HueSliderElem);
			HologramElements.Add(this.SaturationSliderElem);
			HologramElements.Add(this.LightnessSliderElem);

		}

		private void InitializeWidgetsForAlpha(ref float yOffsetColorPanel)
		{
			this.InitializeComponentForTitle("Alpha:", false, ref yOffsetColorPanel);

			this.AlphaSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: 255f);
			this.AlphaSliderElem.Top.Set(yOffsetColorPanel, 0f);
			this.AlphaSliderElem.Left.Set(64f, 0f);
			this.AlphaSliderElem.Width.Set(-64f, 1f);
			this.AlphaSliderElem.SetValue(255f);

			yOffsetColorPanel += 28f;

			HologramElements.Add(this.AlphaSliderElem);

			yOffsetColorPanel += 28f;
		}

		private void InitializeWidgetsForDirection(ref float yOffset)
		{
			this.InitializeComponentForTitle("Direction:", false, ref yOffset);

			this.DirectionSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: -1f,
				maxRange: 1f);
			this.DirectionSliderElem.Top.Set(yOffset, 0f);
			this.DirectionSliderElem.Left.Set(96f, 0f);
			this.DirectionSliderElem.Width.Set(-96f, 1f);
			this.DirectionSliderElem.SetValue(1f);
			this.DirectionSliderElem.PreOnChange += (value) =>
			{
				bool isRepeat = Timers.GetTimerTickDuration("HologramDirectionRepeat") >= 1;
				Timers.SetTimer("HologramDirectionRepeat", 2, true, () => false);
				if (isRepeat)
				{
					return null;
				}

				if (this.DirectionSliderElem.RememberedInputValue < 0f && this.DirectionSliderElem.RememberedInputValue != value)
				{
					value = 1f;
				}
				else
				{
					value = -1f;
				}
				return value;
			};
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.DirectionSliderElem);
			HologramElements.Add(this.DirectionSliderElem);
		}

		private void InitializeWidgetsForRotation(ref float yOffset)
		{
			this.InitializeComponentForTitle("Rotation:", false, ref yOffset);

			this.RotationSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: false,
				ticks: 0,
				minRange: 0f,
				maxRange: 360f);
			this.RotationSliderElem.Top.Set(yOffset, 0f);
			this.RotationSliderElem.Left.Set(96f, 0f);
			this.RotationSliderElem.Width.Set(-96f, 1f);
			this.RotationSliderElem.SetValue(0f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.RotationSliderElem);
			HologramElements.Add(this.RotationSliderElem);
		}

		private void InitializeWidgetsForOffsetX(ref float yOffset)
		{
			this.InitializeComponentForTitle("X Offset:", false, ref yOffset);

			this.OffsetXSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f);    //15f
			this.OffsetXSliderElem.Top.Set(yOffset, 0f);
			this.OffsetXSliderElem.Left.Set(96f, 0f);
			this.OffsetXSliderElem.Width.Set(-96f, 1f);
			this.OffsetXSliderElem.SetValue(0f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.OffsetXSliderElem);
			HologramElements.Add(this.OffsetXSliderElem);
		}

		private void InitializeWidgetsForOffsetY(ref float yOffset)
		{
			this.InitializeComponentForTitle("Y Offset:", false, ref yOffset);

			this.OffsetYSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: -256f,    //0f
				maxRange: 256f);    //15f
			this.OffsetYSliderElem.Top.Set(yOffset, 0f);
			this.OffsetYSliderElem.Left.Set(96f, 0f);
			this.OffsetYSliderElem.Width.Set(-96f, 1f);
			this.OffsetYSliderElem.SetValue(0f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.OffsetYSliderElem);
			HologramElements.Add(this.OffsetYSliderElem);
		}

		private void InitializeWidgetsForFrameStart(ref float yOffset)
		{
			this.InitializeComponentForTitle("Frame Start:", false, ref yOffset);

			this.FrameStartSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)(Main.npcFrameCount[1] - 1));
			this.FrameStartSliderElem.Top.Set(yOffset, 0f);
			this.FrameStartSliderElem.Left.Set(96f, 0f);
			this.FrameStartSliderElem.Width.Set(-96f, 1f);
			this.FrameStartSliderElem.SetValue(0f);
			this.FrameStartSliderElem.PreOnChange += (value) =>
			{
				if (value > this.FrameEndSliderElem.RememberedInputValue)
				{
					value = this.FrameEndSliderElem.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.FrameStartSliderElem);
			HologramElements.Add(this.FrameStartSliderElem);
		}

		private void InitializeWidgetsForFrameEnd(ref float yOffset)
		{
			this.InitializeComponentForTitle("Frame End:", false, ref yOffset);

			this.FrameEndSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 0f,
				maxRange: (float)(Main.npcFrameCount[1] - 1));
			this.FrameEndSliderElem.Top.Set(yOffset, 0f);
			this.FrameEndSliderElem.Left.Set(96f, 0f);
			this.FrameEndSliderElem.Width.Set(-96f, 1f);
			this.FrameEndSliderElem.SetValue(0f);
			this.FrameEndSliderElem.PreOnChange += (value) =>
			{
				if (value < this.FrameStartSliderElem.RememberedInputValue)
				{
					value = this.FrameStartSliderElem.RememberedInputValue;
				}
				return value;
			};
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.FrameEndSliderElem);
			HologramElements.Add(this.FrameEndSliderElem);
		}

		private void InitializeWidgetsForFrameRateTicks(ref float yOffset)
		{
			this.InitializeComponentForTitle("Frame Rate:", false, ref yOffset);

			this.FrameRateTicksSliderElem = new UISlider(
				theme: UITheme.Vanilla,
				hoverText: "",
				isInt: true,
				ticks: 0,
				minRange: 1f,
				maxRange: 60f * 5f);
			this.FrameRateTicksSliderElem.Top.Set(yOffset, 0f);
			this.FrameRateTicksSliderElem.Left.Set(96f, 0f);
			this.FrameRateTicksSliderElem.Width.Set(-96f, 1f);
			this.FrameRateTicksSliderElem.SetValue(8f);
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.FrameRateTicksSliderElem);
			HologramElements.Add(this.FrameRateTicksSliderElem);
		}

		private void InitializeWidgetsForWorldLighting(ref float yOffset)
		{
			this.WorldLightingCheckbox = new UICheckbox(UITheme.Vanilla, "World Lighting", "");
			this.WorldLightingCheckbox.Top.Set(yOffset, 0f);
			this.WorldLightingCheckbox.Selected = true;
			yOffset += 28f;

			this.InnerContainer.Append((UIElement)this.WorldLightingCheckbox);
			HologramUiTexts.Add(this.WorldLightingCheckbox);
		}
		private void InitializeWidgetsForCrtEffect(ref float yOffset)
		{
			this.CRTEffectCheckbox = new UICheckbox(UITheme.Vanilla, "CRT Effect", "");
			this.CRTEffectCheckbox.Top.Set(yOffset, 0f);
			this.CRTEffectCheckbox.Selected = false;
			this.CRTEffectCheckbox.Hide();
			//yOffset += 28f;

			HologramUiTexts.Add(this.CRTEffectCheckbox);
		}
	}
}