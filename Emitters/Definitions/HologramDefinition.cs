using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace Emitters.Definitions {
	public partial class HologramDefinition {
		public static HologramDefinition Read( BinaryReader reader ) {
			return new HologramDefinition(
				mode: (int)reader.ReadInt16(),
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

		public static void Write( HologramDefinition def, BinaryWriter writer ) {
			writer.Write((ushort)def.Mode);
			writer.Write( (ushort)def.Type );
			writer.Write( (float)def.Scale );
			writer.Write( (byte)def.Color.R );
			writer.Write( (byte)def.Color.G );
			writer.Write( (byte)def.Color.B );
			writer.Write( (byte)def.Alpha );
			writer.Write( (ushort)def.Direction );
			writer.Write( (float)def.Rotation );
			writer.Write( (ushort)def.OffsetX );
			writer.Write( (ushort)def.OffsetY );
			writer.Write( (ushort)def.FrameStart );
			writer.Write( (ushort)def.FrameEnd );
			writer.Write( (ushort)def.FrameRateTicks );
			writer.Write( (bool)def.WorldLighting );
			writer.Write( (bool)def.CrtEffect );
			writer.Write( (bool)def.IsActivated );
		}

		////////////////

		public int Mode { get ; set; }
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

		////

		public bool IsActivated { get; set; } = true;

		
		public object SetHologramType()
		{
			switch (this.Mode)
			{
				case 1:
					return new NPCDefinition[this.Type];
				case 2:
					return new ItemDefinition[this.Type];
				case 3:
					return new NPCDefinition[this.Type];
			}
			return new object();
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
					int mode,
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
					bool isActivated )
		{
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

		public void Activate( bool isActivated ) {
			this.IsActivated = isActivated;
		}


		////////////////

		private void AnimateCurrentFrame() {
			if( ++this.CurrentFrameElapsedTicks <= this.FrameRateTicks ) {
				return;
			}
			int frameCount = EmitterUtils.GetFrameCount(this.Mode,this.Type);
			this.CurrentFrame++;
			this.CurrentFrameElapsedTicks = 0;

			if ((this.CurrentFrame > this.FrameEnd) || (this.CurrentFrame >= frameCount))
			{
				this.CurrentFrame = this.FrameStart;
			}
		}

		private bool CheckIfNull()
		{
			if (this.Type >= NPCID.Count)
			{
				switch (this.Mode)
				{
					case 1:
						if (NPCLoader.GetNPC(this.Type) == null) {
							return true;
						}

						break;
					case 2:
						if (ItemLoader.GetItem(this.Type) == null) {
							return true;
						}

						break;
					case 3:
						if (ProjectileLoader.GetProjectile(this.Type) == null) {
							return true;
						}

						break;
				}
			}

			return false;

		}
	}
}
