using System;
using Terraria.ModLoader;


namespace Emitters {
	class EmittersPlayer : ModPlayer {
		public override bool PreItemCheck() {
			if( EmittersMod.Instance.EditorButton.PressEditorButtonIfInteracting() ) {
				return false;
			}
			return base.PreItemCheck();
		}
	}
}
