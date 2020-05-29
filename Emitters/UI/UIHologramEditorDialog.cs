using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Theme;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Classes.UI.Elements.Slider;
using Emitters.Items;
using Emitters.Definitions;
using System.Diagnostics;
using Terraria.ModLoader.UI;
using Terraria.UI;


namespace Emitters.UI
{
	partial class UIHologramEditorDialog : UIDialog
	{
		private int HologramMode => (int) ModeSliderElem.RememberedInputValue;

		private UISlider ModeSliderElem;
		private UISlider TypeSliderElem;
		private UISlider ScaleSliderElem;
		private UISlider HueSliderElem;
		private UISlider SaturationSliderElem;
		private UISlider LightnessSliderElem;
		private UISlider AlphaSliderElem;
		private UISlider DirectionSliderElem;
		private UISlider RotationSliderElem;
		private UISlider OffsetXSliderElem;
		private UISlider OffsetYSliderElem;
		private UISlider FrameStartSliderElem;
		private UISlider FrameEndSliderElem;
		private UISlider FrameRateTicksSliderElem;
		private UICheckbox WorldLightingCheckbox;
		private UICheckbox CRTEffectCheckbox;
		private UITextPanelButton ApplyButton;

		internal List<UIThemedText> HologramUiTexts = new List<UIThemedText>();
		internal List<UIThemedElement> HologramElements = new List<UIThemedElement>();
		internal List<bool> UIModeActive = new List<bool>{false,false,false};

		internal enum hologramUIMode
		{
			MainSettings,
			ColorSettings,
			ShaderSettings
		}

		////

		private Item HologramItem = null;


		////////////////

		public UIHologramEditorDialog() : base(UITheme.Vanilla, 600, 500){ }

		////////////////

		public HologramDefinition CreateHologramDefinition()
		{
			return new HologramDefinition(
				mode: (int) ModeSliderElem.RememberedInputValue,
				type: (int) TypeSliderElem.RememberedInputValue,
				scale: ScaleSliderElem.RememberedInputValue,
				color: GetColor(),
				alpha: (byte) AlphaSliderElem.RememberedInputValue,
				direction: (int) DirectionSliderElem.RememberedInputValue,
				rotation: RotationSliderElem.RememberedInputValue,
				offsetX: (int) OffsetXSliderElem.RememberedInputValue,
				offsetY: (int) OffsetYSliderElem.RememberedInputValue,
				frameStart: (int) FrameStartSliderElem.RememberedInputValue,
				frameEnd: (int) FrameEndSliderElem.RememberedInputValue,
				frameRateTicks: (int) FrameRateTicksSliderElem.RememberedInputValue,
				worldLight: WorldLightingCheckbox.Selected,
				crtEffect: CRTEffectCheckbox.Selected,
				isActivated: true
			);
		}


		////////////////

		public Color GetColor()
		{
			float hue = HueSliderElem.RememberedInputValue;
			float saturation = SaturationSliderElem.RememberedInputValue;
			float lightness = LightnessSliderElem.RememberedInputValue;
			Color color = Main.hslToRgb(hue, saturation, lightness);
			return color;
		}

		////////////////

		//Apply UI values to Item
		//TODO: check if this is causing NPC/Projectile/Item count issues
		internal void SetItem(Item hologramItem)
		{
			HologramItem = hologramItem;

			var myitem = hologramItem.modItem as HologramItem;
			if (myitem.Def == null)
			{
				return;
			}
			PropertyInfo[] hologramProperties = typeof(HologramDefinition).GetProperties(BindingFlags.Public);
			PropertyInfo[] hologramUIProperties = typeof(UIHologramEditorDialog).GetProperties();
			Vector3 hsl = Main.rgbToHsl(myitem.Def.Color);
			int i = 0;
			int j = 0;

			foreach (PropertyInfo property in hologramUIProperties)
			{
				if (property.GetType() is UISlider)
				{
					if (hologramProperties[j].GetType() is Color)
					{
						HueSliderElem.SetValue(hsl.X);
						SaturationSliderElem.SetValue(hsl.Y);
						LightnessSliderElem.SetValue(hsl.Z);
						j += 3;
					}
					else
					{
						UISlider uiSlider = (UISlider)property.GetValue(hologramUIProperties[i]);
						hologramProperties[j].SetValue(myitem.Def, uiSlider.RememberedInputValue);
						j++;
					}
				}
				if (property.GetType() is UICheckbox)
				{
					UICheckbox checkbox = (UICheckbox)property.GetValue(hologramUIProperties[i]);
					hologramProperties[j].SetValue(myitem.Def, checkbox.Selected);
					j++;
				}
				i++;
			}
		}

		////////////////

		public void ApplySettingsToCurrentItem()
		{
			if (HologramItem == null)
			{
				throw new ModHelpersException("Missing item.");
			}

			var myitem = HologramItem.modItem as HologramItem;
			if (myitem == null)
			{
				Main.NewText("No hologram item selected. Changes not saved.", Color.Red);
				return;
			}

			myitem?.SetHologramDefinition(CreateHologramDefinition());
		}


		////////////////

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Main.LocalPlayer.mouseInterface = true;
		}

		/// <summary>
		/// Switch UI State and change elements based on state
		/// </summary>
		/// <param name="stateEnum"></param>
		/// 
		/// //TODO: Doesn't position right  when opening menu from item
		public void switchUIState(hologramUIMode stateEnum)
		{
			int i;
			switch (stateEnum)
			{
				case hologramUIMode.MainSettings:
					Debug.WriteLine("MainSettings");
					for (i = 0; i < HologramElements.Count; i++)
					{
						if (i >= 3 && i <= 6)
						{
							InnerContainer.RemoveChild(HologramElements[i]);
							InnerContainer.RemoveChild(HologramUiTexts[i]);
							HologramUiTexts[i].Hide();
						}
						else
						{
							//Main elements
							InnerContainer.Append(HologramElements[i]);
							InnerContainer.Append(HologramUiTexts[i]);
						}
					}
					//World Lighting Checkbox
					HologramUiTexts[14].Top.Set(yOffset - 56f, 0f);
					HologramUiTexts[14].Show();

					ApplyButton.Top.Set(yOffset - 56f,0f);
					
					//CRT Checkbox
					if (!HologramUiTexts[15].IsHidden)
					{
						HologramUiTexts[15].Hide();
						InnerContainer.RemoveChild(HologramUiTexts[15]);
					}
					//Reset all states then update current state
					for (i = 0; i < UIModeActive.Count; i++) UIModeActive[i] = false;
					UIModeActive[1] = true;

					//Set height appropriately
					OuterContainer.Height.Set(yOffset, 0f);
					RecalculateMe();
					break;

				case hologramUIMode.ColorSettings:
					Debug.WriteLine("ColorSettings");
					for (i = 0; i < HologramElements.Count; i++) {
						if (i >= 3 && i <= 6)
						{
							//Color elements
							InnerContainer.Append(HologramElements[i]);
							InnerContainer.Append( HologramUiTexts[i] );
							HologramUiTexts[i].Show();
						}
						else
						{
							InnerContainer.RemoveChild(HologramElements[i]);
							InnerContainer.RemoveChild( HologramUiTexts[i] );
						}
					}
					//World Lighting Checkbox
					HologramUiTexts[14].Top.Set(yOffsetColorPanel - 28f, 0f);
					HologramUiTexts[14].Show();
					ApplyButton.Top.Set(yOffsetColorPanel - 28f, 0f);

					//CRT checkbox
					if (!HologramUiTexts[15].IsHidden)
					{
						HologramUiTexts[15].Hide();
						InnerContainer.RemoveChild(HologramUiTexts[15]);
					}
					//Reset all states then update current state
					for (i = 0; i < UIModeActive.Count; i++) UIModeActive[i] = false;
					UIModeActive[2] = true;

					//Set height appropriately
					OuterContainer.Height.Set(yOffsetColorPanel + 28f,0f);
					RecalculateMe();
					break;

					//TODO: Implement Shader Tab
				case hologramUIMode.ShaderSettings:
					for (i = 0; i > HologramUiTexts.Count; i++) {
						InnerContainer.RemoveChild(HologramElements[i]);
						InnerContainer.RemoveChild(HologramUiTexts[i]);
					}

					HologramUiTexts[13].Hide();
					HologramUiTexts[14].Show();

					//Reset all states then update current state
					for (i = 0; i < UIModeActive.Count; i++) UIModeActive[i] = false;
					UIModeActive[3] = true;

					break;
			}

		}

		////////////////

		private HologramDefinition CachedHologramDef = null;

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
			HologramDefinition def = CreateHologramDefinition();
			def.CurrentFrame = CachedHologramDef?.CurrentFrame ?? 0;
			def.CurrentFrameElapsedTicks = CachedHologramDef?.CurrentFrameElapsedTicks ?? 0;
			CachedHologramDef = def;
			def.AnimateHologram(Main.MouseWorld, true);
		}

	}
}