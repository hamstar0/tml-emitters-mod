using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using HamstarHelpers.Classes.Errors;


namespace Emitters.Definitions {
	public enum HologramMode {
		NPC,
		Item,
		Projectile
	}



	public partial class HologramDefinition : BaseEmitterDefinition {
		public static bool IsBadType( HologramMode mode, int type ) {
			if( type < NPCID.Count ) {
				return false;
			}

			switch( mode ) {
			case HologramMode.NPC:
				return NPCLoader.GetNPC( type ) == null;
			case HologramMode.Item:
				return ItemLoader.GetItem( type ) == null;
			case HologramMode.Projectile:
				return ProjectileLoader.GetProjectile( type ) == null;
			default:
				throw new ModHelpersException( "Invalid hologram type" );
			}
		}

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
		public bool CrtEffect { get; set; }



		////////////////

		public EntityDefinition SetHologramType() {
			switch( this.Mode ) {
			case HologramMode.NPC:
				return new NPCDefinition( this.Type );
			case HologramMode.Item:
				return new ItemDefinition( this.Type );
			case HologramMode.Projectile:
				return new ProjectileDefinition( this.Type );
			default:
				throw new ModHelpersException( "Invalid hologram type" );
			}
		}

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
			this.CrtEffect = copy.CrtEffect;
			this.IsActivated = copy.IsActivated;

			this.CurrentFrame = this.FrameStart;
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
					bool crtEffect,
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
			this.CrtEffect = crtEffect;
			this.IsActivated = isActivated;

			this.CurrentFrame = frameStart;
		}

		////////////////

		public override BaseEmitterDefinition Read( BinaryReader reader ) {
			return new HologramDefinition(
				mode: (HologramMode)reader.ReadInt16(),
				type: (int)reader.ReadUInt16(),
				scale: (float)reader.ReadSingle(),
				color: new Color(
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte(),
					(byte)reader.ReadByte()
				),
				alpha: (byte)reader.ReadByte(),
				direction: (int)reader.ReadUInt16(),
				rotation: (float)reader.ReadSingle(),
				offsetX: (int)reader.ReadUInt16(),
				offsetY: (int)reader.ReadUInt16(),
				frameStart: (int)reader.ReadUInt16(),
				frameEnd: (int)reader.ReadUInt16(),
				frameRateTicks: (int)reader.ReadUInt16(),
				worldLight: (bool)reader.ReadBoolean(),
				crtEffect: (bool)reader.ReadBoolean(),
				isActivated: (bool)reader.ReadBoolean()
			);
		}

		public override void Write( BinaryWriter writer ) {
			writer.Write( (ushort)this.Mode );
			writer.Write( (ushort)this.Type );
			writer.Write( (float)this.Scale );
			writer.Write( (byte)this.Color.R );
			writer.Write( (byte)this.Color.G );
			writer.Write( (byte)this.Color.B );
			writer.Write( (byte)this.Alpha );
			writer.Write( (ushort)this.Direction );
			writer.Write( (float)this.Rotation );
			writer.Write( (ushort)this.OffsetX );
			writer.Write( (ushort)this.OffsetY );
			writer.Write( (ushort)this.FrameStart );
			writer.Write( (ushort)this.FrameEnd );
			writer.Write( (ushort)this.FrameRateTicks );
			writer.Write( (bool)this.WorldLighting );
			writer.Write( (bool)this.CrtEffect );
			writer.Write( (bool)this.IsActivated );
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
