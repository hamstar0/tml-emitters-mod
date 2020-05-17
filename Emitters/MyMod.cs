using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Emitters.UI;


namespace Emitters {
	public partial class EmittersMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-emitters-mod";


		////////////////

		public static EmittersMod Instance { get; private set; }



		////////////////

		internal Texture2D Emitter;
		internal Texture2D SoundEmitter;

		internal UIEmitterEditorDialog EmitterEditorDialog;
		internal UISoundEmitterEditorDialog SoundEmitterEditorDialog;


		////////////////

		public EmittersMod() {
			EmittersMod.Instance = this;
		}

		public override void Load() {
			if( !Main.dedServ ) {
				this.EmitterEditorDialog = new UIEmitterEditorDialog();
				this.SoundEmitterEditorDialog = new UISoundEmitterEditorDialog();
			}
		}

		public override void Unload() {
			EmittersMod.Instance = null;
		}

		////

		public override void PostSetupContent() {
			if( !Main.dedServ ) {
				this.Emitter = this.GetTexture( "Emitter" );
				this.SoundEmitter = this.GetTexture("Emitter");
			}
		}
	}
}