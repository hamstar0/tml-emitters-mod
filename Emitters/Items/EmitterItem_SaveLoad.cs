using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem, IBaseEmitterItem {
		public override void Load( TagCompound tag ) {
			if( !tag.ContainsKey("EmitterMode") ) {
				return;
			}

			try {
				this.Def = new EmitterDefinition(
					isGoreMode: tag.GetBool( "EmitterMode" ),
					type: tag.GetInt( "EmitterType" ),
					scale: tag.GetFloat( "EmitterScale" ),
					delay: tag.GetInt( "EmitterDelay" ),
					speedX: tag.GetFloat( "EmitterSpeedX" ),
					speedY: tag.GetFloat( "EmitterSpeedY" ),
					color: new Color(
						tag.GetByte( "EmitterColorR" ),
						tag.GetByte( "EmitterColorG" ),
						tag.GetByte( "EmitterColorB" )
					),
					transparency: tag.GetByte( "EmitterAlpha" ),
					scatter: tag.GetFloat( "EmitterScatter" ),
					hasGravity: tag.GetBool( "EmitterHasGrav" ),
					hasLight: tag.GetBool( "EmitterHasLight" ),
					isActivated: tag.GetBool( "EmitterIsActivated" )
				);
			} catch { }
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}

			return new TagCompound {
				{ "EmitterMode", (bool)this.Def.IsGoreMode },
				{ "EmitterType", (int)this.Def.Type },
				{ "EmitterScale", (float)this.Def.Scale },
				{ "EmitterDelay", (int)this.Def.Delay },
				{ "EmitterSpeedX", (float)this.Def.SpeedX },
				{ "EmitterSpeedY", (float)this.Def.SpeedY },
				{ "EmitterColorR", (byte)this.Def.Color.R },
				{ "EmitterColorG", (byte)this.Def.Color.G },
				{ "EmitterColorB", (byte)this.Def.Color.B },
				{ "EmitterAlpha", (byte)this.Def.Transparency },
				{ "EmitterScatter", (float)this.Def.Scatter },
				{ "EmitterHasGrav", (bool)this.Def.HasGravity },
				{ "EmitterHasLight", (bool)this.Def.HasLight },
				{ "EmitterIsActivated", (bool)this.Def.IsActivated },
			};
		}
	}
}