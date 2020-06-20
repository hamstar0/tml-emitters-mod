using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Helpers.Debug;


namespace Emitters.Definitions {
	public enum HologramMode {
		NPC,
		Item,
		Projectile
	}

	public enum HologramShaderMode {
		None,
		Vanilla,
		Custom
	}



	public partial class HologramDefinition : BaseEmitterDefinition {
		public static bool IsBadType( HologramMode mode, int type ) {
			switch( mode ) {
			case HologramMode.NPC:
				if( type < Main.npcTexture.Length ) { return false; }
				return NPCLoader.GetNPC( type ) == null;
			case HologramMode.Item:
				if( type < Main.itemTexture.Length ) { return false; }
				return ItemLoader.GetItem( type ) == null;
			case HologramMode.Projectile:
				if( type < Main.projectileTexture.Length ) { return false; }
				return ProjectileLoader.GetProjectile( type ) == null;
			default:
				throw new ModHelpersException( "Invalid hologram type" );
			}
		}

		////

		public static Texture2D GetTexture( HologramMode mode, int type ) {
			switch( mode ) {
			case HologramMode.NPC:
				return Main.npcTexture[type];
			case HologramMode.Item:
				return Main.itemTexture[type];
			case HologramMode.Projectile:
				return Main.projectileTexture[type];
			default:
				throw new ModHelpersException( "Invalid hologram type" );
			}
		}

		public static int GetFrameCount( HologramMode mode, int type ) {
			switch( mode ) {
			case HologramMode.NPC:
				return Main.npcFrameCount[type];
			case HologramMode.Item:
				return 1;
			case HologramMode.Projectile:
				return Main.projFrames[type];
			default:
				throw new ModHelpersException( "Invalid hologram type" );
			}
		}



		////////////////

		public HologramMode Mode { get; set; }

		public int Type { get; set; }

		public float Scale { get; set; }

		public Color Color { get; set; }

		public byte Alpha { get; set; }

		public int Direction { get; set; }

		public float Rotation { get; set; }

		public int OffsetX { get; set; }

		public int OffsetY { get; set; }

		public int FrameStart { get; set; }

		public int FrameEnd { get; set; }

		public int FrameRateTicks { get; set; }

		public bool WorldLighting { get; set; }

		public HologramShaderMode ShaderMode { get; set; }

		public float ShaderTime { get; set; }

		public int ShaderType { get; set; }

		////

		public int CurrentFrame { get; internal set; }

		////

		[Obsolete( "use ShaderMode", true )]
		public bool CrtEffect {
			get => this.ShaderMode == HologramShaderMode.Custom;
			//set => this.ShaderMode = value;
			set => this.ShaderMode = this.ShaderMode;
		}



		////////////////

		internal int CurrentFrameElapsedTicks = 0;



		////////////////

		public HologramDefinition() { }

		public HologramDefinition( HologramDefinition copy ) {
			this.Mode = copy.Mode;
			this.Type = copy.Type;
			this.Scale = copy.Scale;
			this.Color = copy.Color;
			this.Alpha = copy.Alpha;
			this.Direction = copy.Direction;
			this.Rotation = copy.Rotation;
			this.OffsetX = copy.OffsetX;
			this.OffsetY = copy.OffsetY;
			this.FrameStart = copy.FrameStart;
			this.FrameEnd = copy.FrameEnd;
			this.FrameRateTicks = copy.FrameRateTicks;
			this.WorldLighting = copy.WorldLighting;
			this.ShaderMode = copy.ShaderMode;
			this.ShaderTime = copy.ShaderTime;
			this.ShaderType = copy.ShaderType;
			this.IsActivated = copy.IsActivated;

			this.CurrentFrameElapsedTicks = copy.CurrentFrameElapsedTicks;
			this.CurrentFrame = copy.CurrentFrame;
		}

		public HologramDefinition(
					HologramMode mode,
					int type,
					float scale,
					Color color,
					byte alpha,
					int direction,
					float rotation,
					int offsetX,
					int offsetY,
					int frameStart,
					int frameEnd,
					int frameRateTicks,
					bool worldLight,
					HologramShaderMode shaderMode,
					float shaderTime,
					int shaderType,
					bool isActivated ) {
			this.Mode = mode;
			this.Type = type;
			this.Scale = scale;
			this.Color = color;
			this.Alpha = alpha;
			this.Direction = direction;
			this.Rotation = rotation;
			this.OffsetX = offsetX;
			this.OffsetY = offsetY;
			this.FrameStart = frameStart;
			this.FrameEnd = frameEnd;
			this.FrameRateTicks = frameRateTicks;
			this.WorldLighting = worldLight;
			this.ShaderMode = shaderMode;
			this.ShaderTime = shaderTime;
			this.ShaderType = shaderType;
			this.IsActivated = isActivated;

			this.CurrentFrame = frameStart;
		}


		////////////////

		private void AnimateCurrentFrame() {
			if( ++this.CurrentFrameElapsedTicks <= this.FrameRateTicks ) {
				return;
			}

			int frameCount = HologramDefinition.GetFrameCount( this.Mode, this.Type );

			this.CurrentFrame++;
			this.CurrentFrameElapsedTicks = 0;

			if( (this.CurrentFrame > this.FrameEnd) || (this.CurrentFrame >= frameCount) ) {
				this.CurrentFrame = this.FrameStart;
			}
		}
	}
}
