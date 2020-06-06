using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Emitters.UI;


namespace Emitters {
	public partial class EmittersMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-emitters-mod";


		////////////////

		public static EmittersMod Instance { get; private set; }



		////////////////

		internal Texture2D EmitterTex;
		internal Texture2D SoundEmitterTex;
		internal Texture2D HologramTex;
		internal UIEmitterEditorDialog EmitterEditorDialog;
		internal UISoundEmitterEditorDialog SoundEmitterEditorDialog;
		internal UIHologramEditorDialog HologramEditorDialog;

		internal Effect HologramFX;
		public List<ArmorShaderData> ShaderData;
		internal List<VanillaArmorShaderData> ArmorShaderReflections = new List<VanillaArmorShaderData>();

		public List<string> ShaderPassNames = new List<string>();



		////////////////

		public EmittersMod() {
			EmittersMod.Instance = this;
		}

		public override void Load() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				this.LoadShaders();

				this.EmitterEditorDialog = new UIEmitterEditorDialog();
				this.SoundEmitterEditorDialog = new UISoundEmitterEditorDialog();
				this.HologramEditorDialog = new UIHologramEditorDialog();
				this.HologramFX = this.GetEffect( "Effects/ScanlinesCRT" );
				//var scanlinesCRT = new Ref<Effect>( this.HologramFX );
				//GameShaders.Misc["Emitters:ScanlinesPS"] = new MiscShaderData( scanlinesCRT, "P0" )
				//	.UseImage( "Images/Misc/Perlin" );	//?

				IL.Terraria.Wiring.HitWireSingle += HookWireHit;
			}
		}

		private void LoadShaders() {
			this.ShaderData = VanillaArmorShaderData.GetShaderList();

			foreach( ArmorShaderData shader in this.ShaderData ) {
				if( shader == null ) {
					continue;
				}

				var passName = VanillaArmorShaderData.GetPassName( shader );
				this.ShaderPassNames.Add( passName );
				System.Diagnostics.Debug.WriteLine( "PassName: " + passName );

				var armorShader = new VanillaArmorShaderData( Main.PixelShaderRef, passName ) {
					UColor = VanillaArmorShaderData.GetPrimaryColor( shader ),
					USecondaryColor = VanillaArmorShaderData.GetSecondaryColor( shader ),
					UOpacity = VanillaArmorShaderData.GetOpacity( shader ),
					USaturation = VanillaArmorShaderData.GetSaturation( shader )
				};

				this.ArmorShaderReflections.Add( armorShader );
			}
		}


		public override void Unload() {
			EmittersMod.Instance = null;
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