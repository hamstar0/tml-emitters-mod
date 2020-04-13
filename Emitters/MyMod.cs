using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Emitters.UI;


namespace Emitters {
	public class EmittersMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-emitters-mod";


		////////////////

		public static EmittersMod Instance { get; private set; }



		////////////////

		private IDictionary<short, IDictionary<short, EmitterDefinition>> Emitters
			= new Dictionary<short, IDictionary<short, EmitterDefinition>>();

		////

		internal UIEmitterEditorDialog EmitterEditorDialog;



		////////////////

		public EmittersMod() {
			EmittersMod.Instance = this;
		}

		public override void Load() {
			if( !Main.dedServ ) {
				this.EmitterEditorDialog = new UIEmitterEditorDialog();
			}
		}

		public override void Unload() {
			EmittersMod.Instance = null;
		}
	}
}