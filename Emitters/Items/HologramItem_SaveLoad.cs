using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class HologramItem : ModItem, IBaseEmitterItem<HologramDefinition> {
		public override void Load( TagCompound tag ) {
			try {
				HologramMode mode;
				if( !tag.ContainsKey( "HologramMode" ) ) {
					mode = HologramMode.NPC;
				} else {
					mode = (HologramMode)tag.GetInt( "HologramMode" );
				}

				string entDefRaw = tag.GetString( "HologramType" );
				int type = HologramDefinition.GetEntDef( mode, entDefRaw ).Type;

				var shaderMode = HologramShaderMode.None;
				if( tag.ContainsKey("HologramShaderMode") ) {
					shaderMode = (HologramShaderMode)tag.GetInt( "HologramShaderMode" );
				} else if( tag.ContainsKey("HologramCRTEffect") ) {
					shaderMode = tag.GetBool("HologramCRTEffect")
						? HologramShaderMode.Custom
						: HologramShaderMode.None;
				}

				int shaderType = 0;
				if( tag.ContainsKey("HologramShaderType") ) {
					shaderType = tag.GetInt( "HologramShaderType" );
				}

				float shaderTime = 1f;
				if( tag.ContainsKey("HologramShaderTime") ) {
					shaderTime = tag.GetFloat( "HologramShaderTime" );
				}

				this.SetDefinition( new HologramDefinition(
					mode: mode,
					type: type,
					scale: tag.GetFloat( "HologramScale" ),
					color: new Color(
						tag.GetByte( "HologramColorR" ),
						tag.GetByte( "HologramColorG" ),
						tag.GetByte( "HologramColorB" )
					),
					alpha: tag.GetByte( "HologramAlpha" ),
					direction: tag.GetInt( "HologramDirection" ),
					rotation: tag.GetFloat( "HologramRotation" ),
					offsetX: tag.GetInt( "HologramOffsetX" ),
					offsetY: tag.GetInt( "HologramOffsetY" ),
					frameStart: tag.GetInt( "HologramFrameStart" ),
					frameEnd: tag.GetInt( "HologramFrameEnd" ),
					frameRateTicks: tag.GetInt( "HologramFrameRateTicks" ),
					worldLight: tag.GetBool( "HologramWorldLighting" ),
					shaderMode: shaderMode,
					shaderType: shaderType,
					shaderTime: shaderTime,
					isActivated: tag.GetBool( "HologramIsActivated" )
				) );
			} catch { }
		}

		public override TagCompound Save() {
			if( this.Def == null ) {
				return new TagCompound();
			}

			return new TagCompound {
				{ "HologramMode", (int)this.Def.Mode },
				{ "HologramType", (string)HologramDefinition.GetEntDef(this.Def.Mode, this.Def.Type).ToString() },
				{ "HologramScale", (float)this.Def.Scale },
				{ "HologramColorR", (byte)this.Def.Color.R },
				{ "HologramColorG", (byte)this.Def.Color.G },
				{ "HologramColorB", (byte)this.Def.Color.B },
				{ "HologramAlpha", (byte)this.Def.Alpha },
				{ "HologramDirection", (int)this.Def.Direction },
				{ "HologramRotation", (float)this.Def.Rotation },
				{ "HologramOffsetX", (int)this.Def.OffsetX },
				{ "HologramOffsetY", (int)this.Def.OffsetY },
				{ "HologramFrameStart", (int)this.Def.FrameStart },
				{ "HologramFrameEnd", (int)this.Def.FrameEnd },
				{ "HologramFrameRateTicks", (int)this.Def.FrameRateTicks },
				{ "HologramWorldLighting", (bool)this.Def.WorldLighting },
				{ "HologramShaderMode", (int)this.Def.ShaderMode },
				{ "HologramShaderType", (int)this.Def.ShaderType },
				{ "HologramShaderTime", (float)this.Def.ShaderTime},
				{ "HologramIsActivated", (bool)this.Def.IsActivated },
			};
		}
	}
}