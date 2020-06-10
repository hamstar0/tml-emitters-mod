using System;
using Terraria.ModLoader;


namespace Emitters {
	class EmittersPlayer : ModPlayer {
		public override bool PreItemCheck() {
			if( EmittersMod.Instance.RunEditorButtonIfInteracting() ) {
				return false;
			}
			return base.PreItemCheck();
		}
	}
}
