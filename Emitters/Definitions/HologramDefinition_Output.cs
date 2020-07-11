using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.DotNET.Reflection;
using Emitters.Terraria.ModLoader.Config;


namespace Emitters.Definitions {
	public partial class HologramDefinition : BaseEmitterDefinition {
		public static EntityDefinition GetEntDef( HologramMode mode, int type ) {
			switch( mode ) {
			case HologramMode.NPC:
				return new NPCDefinition( type );
			case HologramMode.Item:
				return new ItemDefinition( type );
			case HologramMode.Projectile:
				return new ProjectileDefinition( type );
			case HologramMode.Gore:
				return new GoreDefinition( type );
			default:
				throw new NotImplementedException( "No such mode." );
			}
		}

		public static EntityDefinition GetEntDef( HologramMode mode, string rawDef ) {
			switch( mode ) {
			case HologramMode.NPC:
				return NPCDefinition.FromString( rawDef );
			case HologramMode.Item:
				return ItemDefinition.FromString( rawDef );
			case HologramMode.Projectile:
				return ProjectileDefinition.FromString( rawDef );
			case HologramMode.Gore:
				return GoreDefinition.FromString( rawDef );
			default:
				throw new NotImplementedException( "No such mode.." );
			}
		}

		public static EntityDefinition GetTypeDef( HologramMode mode, object objLiteral ) {
			if( !ReflectionHelpers.Get(objLiteral, "mod", out string mod) ) {
				return null;
			}
			if( !ReflectionHelpers.Get(objLiteral, "name", out string name) ) {
				return null;
			}

			switch( mode ) {
			case HologramMode.NPC:
				return new NPCDefinition( mod, name );
			case HologramMode.Item:
				return new ItemDefinition( mod, name );
			case HologramMode.Projectile:
				return new ProjectileDefinition( mod, name );
			case HologramMode.Gore:
				return new GoreDefinition( mod, name );
			default:
				throw new NotImplementedException( "No such mode.." );
			}
		}



		////////////////

		public void Output(
					out HologramMode mode,
					out int type,
					out float scale,
					out Color color,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frameStart,
					out int frameEnd,
					out int frameRateTicks,
					out bool worldLight,
					out HologramShaderMode shaderMode,
					out float shaderTime,
					out int shaderType,
					out bool isActivated ) {
			mode = this.Mode;
			type = this.Type;
			scale = this.Scale;
			color = this.Color;
			alpha = this.Alpha;
			direction = this.Direction;
			rotation = this.Rotation;
			offsetX = this.OffsetX;
			offsetY = this.OffsetY;
			frameStart = this.FrameStart;
			frameEnd = this.FrameEnd;
			frameRateTicks = this.FrameRateTicks;
			worldLight = this.WorldLighting;
			shaderMode = this.ShaderMode;
			shaderTime = this.ShaderTime;
			shaderType = this.ShaderType;
			isActivated = this.IsActivated;
		}

		public void Output(
					out HologramMode mode,
					out int type,
					out float scale,
					out byte colorR,
					out byte colorG,
					out byte colorB,
					out byte alpha,
					out int direction,
					out float rotation,
					out int offsetX,
					out int offsetY,
					out int frameStart,
					out int frameEnd,
					out int frameRateTicks,
					out bool worldLight,
					out HologramShaderMode shaderMode,
					out float shaderTime,
					out int shaderType,
					out bool isActivated ) {
			Color color;

			this.Output(
				mode: out mode,
				type: out type,
				scale: out scale,
				color: out color,
				alpha: out alpha,
				direction: out direction,
				rotation: out rotation,
				offsetX: out offsetX,
				offsetY: out offsetY,
				frameStart: out frameStart,
				frameEnd: out frameEnd,
				frameRateTicks: out frameRateTicks,
				worldLight: out worldLight,
				shaderMode: out shaderMode,
				shaderTime: out shaderTime,
				shaderType: out shaderType,
				isActivated: out isActivated
			);
			colorR = color.R;
			colorG = color.G;
			colorB = color.B;
		}


		////////////////

		public string RenderType() {
			return this.Type.ToString();
		}
		public string RenderMode() {
			return this.Mode.ToString();
		}
		public string RenderScale() {
			return (this.Scale * 100f).ToString("N0") + "%";
		}
		public string RenderColor() {
			Color color = this.Color;
			color.A = this.Alpha;
			return color.ToString();
		}
		public string RenderAlpha() {
			return this.Alpha.ToString();
		}
		public string RenderDirection() {
			return this.Direction.ToString( "N0" );
		}
		public string RenderRotation() {
			return this.Rotation.ToString( "N2" );
		}
		public string RenderOffsetX() {
			return this.OffsetX.ToString( "N2" );
		}
		public string RenderOffsetY() {
			return this.OffsetY.ToString( "N2" );
		}
		public string RenderOffset() {
			return this.RenderOffsetX() + ", " + this.RenderOffsetY();
		}
		public string RenderFrameStart() {
			return this.FrameStart.ToString();
		}
		public string RenderFrameEnd() {
			return this.FrameEnd.ToString();
		}
		public string RenderFrameRateTicks() {
			return this.FrameRateTicks.ToString();
		}
		public string RenderFrame() {
			return "#" + this.CurrentFrame
				+ " between " + this.FrameStart
				+ " and " + this.FrameEnd
				+ " (rate: " + this.FrameRateTicks + ")";
		}
		public string RenderShaderMode() {
			return this.ShaderMode.ToString();
		}
		public string RenderShaderTime() {
			return this.ShaderTime.ToString( "N3" );
		}
		public string RenderShaderType() {
			return this.ShaderType.ToString();
		}


		////////////////

		public override string ToString() {
			return this.ToString( false );
		}

		public string ToString( bool newLines ) {
			string[] fields = this.ToStringFields();
			if( newLines ) {
				return string.Join( "\n ", fields );
			} else {
				return string.Join( ",  ", fields );
			}
		}

		public string[] ToStringFields() {
			return new string[] {
				"Hologram Definition:",
				/*"\n"+*/"Type: " + this.RenderType(),
				/*"\n"+*/"Mode: " + this.RenderMode(),
				/*"\n"+*/"Scale: " + this.RenderScale(),
				/*"\n"+*/"Color: " + this.RenderColor(),
				/*"\n"+*/"Direction: " + this.RenderDirection(),
				/*"\n"+*/"Rotation: " + this.RenderRotation(),
				/*"\n"+*/"Offset: " + this.RenderOffset(),
				/*"\n"+*/"Frame: " + this.RenderFrame(),
				/*"\n"+*/"World Light: " + this.WorldLighting,
				/*"\n"+*/"Shader Mode: " + this.RenderShaderMode(),
				/*"\n"+*/"Shader Type: " + this.ShaderType,
				/*"\n"+*/"Shader Time: " + this.ShaderTime,
				/*"\n"+*/"Is Activated: " + this.IsActivated
			};
		}
	}
}
