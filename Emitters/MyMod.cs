using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Reflection;
using Emitters.UI;
using Emitters.Effects;


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
		public List<ArmorShaderData> ArmorShaders;
		internal List<EmitterArmorShaderData> MyArmorShaders = new List<EmitterArmorShaderData>();

		private Texture2D EditorButtonTex;
		private Action EditorButtonAction = null;


		////////////////

		public int EditorButtonWidth => this.EditorButtonTex.Width;
		public int EditorButtonHeight => this.EditorButtonTex.Height;



		////////////////

		public EmittersMod() {
			EmittersMod.Instance = this;
		}

		public override void Load() {
			if( !Main.dedServ && Main.netMode != NetmodeID.Server ) {
				ReflectionHelpers.Get( typeof(UICommon), null, "ButtonConfigTexture", out this.EditorButtonTex );

				this.LoadArmorShaders();

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

		private void LoadArmorShaders() {
			this.ArmorShaders = EmitterArmorShaderData.GetVanillaArmorShaders();

			foreach( ArmorShaderData baseShaderData in this.ArmorShaders ) {
				if( baseShaderData == null ) {
					continue;
				}

				var passName = EmitterArmorShaderData.GetPassName( baseShaderData );
				var shaderData = new EmitterArmorShaderData( Main.PixelShaderRef, baseShaderData, passName );

				this.MyArmorShaders.Add( shaderData );
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
	}
}