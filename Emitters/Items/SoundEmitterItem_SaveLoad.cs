using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem, IBaseEmitterItem<SoundEmitterDefinition> {
		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey( "SndEmitterType" ) ) {
				return;
			}

			try {
				this.SetDefinition( new SoundEmitterDefinition(
					type: tag.GetInt( "SndEmitterType" ),
					style: tag.GetInt( "SndEmitterStyle" ),
					volume: tag.GetFloat( "SndEmitterVolume" ),
					pitch: tag.GetFloat( "SndEmitterPitch" ),
					delay: tag.GetInt( "SndEmitterDelay" ),
					isActivated: tag.GetBool( "SndEmitterIsActivated" )
				) );
			} catch { }
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}

			return new TagCompound {
				{ "SndEmitterType", (int)this.Def.Type },
				{ "SndEmitterStyle", (int)this.Def.Style },
				{ "SndEmitterVolume", (float)this.Def.Volume },
				{ "SndEmitterPitch", (float)this.Def.Pitch },
				{ "SndEmitterDelay", (int)this.Def.Delay },
				{ "SndEmitterIsActivated", (bool)this.Def.IsActivated },
			};
		}
	}
}