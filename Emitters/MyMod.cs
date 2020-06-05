using Emitters.UI;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emitters
{
	public partial class EmittersMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-emitters-mod";


		////////////////

		public static EmittersMod Instance { get; private set; }



		////////////////

		internal Texture2D EmitterTex;
		internal Texture2D SoundEmitterTex;
		internal Texture2D HologramTex;
		internal Effect HologramFX;
		internal UIEmitterEditorDialog EmitterEditorDialog;
		internal UISoundEmitterEditorDialog SoundEmitterEditorDialog;
		internal UIHologramEditorDialog HologramEditorDialog;
		public List<ArmorShaderData> ShaderData;
		internal List<ArmorShaderReflection> ArmorShaderReflections = new List<ArmorShaderReflection>();
		public List<string> passNames = new List<string>();
		////////////////

		public EmittersMod() {
			EmittersMod.Instance = this;
		}

		public override void Load()
		{
			if (!Main.dedServ && Main.netMode != NetmodeID.Server)
			{
				{
					ShaderData = new List<ArmorShaderData>(VanillaShaderReflection.GetShaderList());
					foreach (var t in ShaderData)
					{
						if (t != null)
						{
							var passName = VanillaShaderReflection.GetPassName(t);
							passNames.Add(passName);
							System.Diagnostics.Debug.WriteLine("PassName: " + passName)
								;
							var armorShader = new ArmorShaderReflection(Main.PixelShaderRef, passName)
							{
								UColor = VanillaShaderReflection.GetPrimaryColor(t),
								USecondaryColor = VanillaShaderReflection.GetSecondaryColor(t),
								UOpacity = VanillaShaderReflection.GetOpacity(t),
								USaturation = VanillaShaderReflection.GetSaturation(t)
							};

							ArmorShaderReflections.Add(armorShader);
						}
					}

					this.EmitterEditorDialog = new UIEmitterEditorDialog();
					this.SoundEmitterEditorDialog = new UISoundEmitterEditorDialog();
					this.HologramEditorDialog = new UIHologramEditorDialog();
					this.HologramFX = this.GetEffect("Effects/ScanlinesCRT");
					//var scanlinesCRT = new Ref<Effect>( this.HologramFX );
					//GameShaders.Misc["Emitters:ScanlinesPS"] = new MiscShaderData( scanlinesCRT, "P0" )
					//	.UseImage( "Images/Misc/Perlin" );	//?
				}

				IL.Terraria.Wiring.HitWireSingle += HookWireHit;
			}
		}


		public override void Unload() {
			EmittersMod.Instance = null;
			ShaderData = null;
			ArmorShaderReflections = null;
			passNames = null;
		}

		////

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.EmitterTex = this.GetTexture( "Definitions/Emitter" );
				this.SoundEmitterTex = this.GetTexture( "Definitions/SoundEmitter" );
				this.HologramTex = this.GetTexture( "Definitions/Hologram" );
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