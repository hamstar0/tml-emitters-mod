using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System.Collections.Generic;
using Terraria.ModLoader;
using Emitters.UI;
using System.Linq;
using Terraria.UI;
using Emitters.Definitions;
using Microsoft.Xna.Framework;

namespace Emitters {
	public partial class EmittersMod : Mod
	{
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-emitters-mod";


		////////////////

		public static EmittersMod Instance { get; private set; }



		////////////////

		internal Texture2D EmitterTex;
		internal Texture2D SoundEmitterTex;
		internal Texture2D HologramTex;

		internal UIEmitterEditorDialog EmitterEditorDialog;
		internal UIHologramEditorDialog HologramEditorDialog;
		internal UISoundEmitterEditorDialog SoundEmitterEditorDialog;


		////////////////

		public EmittersMod()
		{
			EmittersMod.Instance = this;
		}

		public override void Load()
		{
			if (!Main.dedServ)
			{
				this.EmitterEditorDialog = new UIEmitterEditorDialog();
				this.SoundEmitterEditorDialog = new UISoundEmitterEditorDialog();
				this.HologramEditorDialog = new UIHologramEditorDialog();
			}
		}

		public override void Unload()
		{
			EmittersMod.Instance = null;
		}

		////

		public override void PostSetupContent()
		{
			if (!Main.dedServ)
			{
				this.EmitterTex = this.GetTexture("Definitions/Emitter");
				this.SoundEmitterTex = this.GetTexture("Definitions/SoundEmitter");
				this.HologramTex = this.GetTexture("Definitions/Hologram");
			}
		}

		//public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		//{
		//	int cursorIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Cursor"));
		//	if (cursorIndex == -1) { return; }

		//	layers.Insert(cursorIndex, new LegacyGameInterfaceLayer(
		//		"Emitters: DrawPreveiw",
		//		delegate {
		//			if (HologramEditorDialog.IsOpen)
		//			{
		//				HologramDefinition def = HologramEditorDialog.CreateHologramDefinition();
		//				HologramEditorDialog.Draw(Main.spriteBatch);
		//			}
		//			return true;
		//		},
		//			InterfaceScaleType.UI)
		//		);

		//}
	}
}