using System;
using Emitters.Definitions;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.Config;

namespace Emitters
{
	class EmitterUtils
	{
		public static int GetFrameCount(int mode, int type)
		{
			switch (mode)
			{
				case 1:
					return Main.npcFrameCount[type];
				case 2:
					return 1;
				case 3:
					return Main.projFrames[type];
				default:
					return 1;
			}
		}

		public static Texture2D GetTexture(int mode, int type)
		{
			switch (mode)
			{
				case 1:
					return Main.npcTexture[type];
				case 2:
					return Main.itemTexture[type];
				case 3:
					return Main.projectileTexture[type];
				default:
					return Main.npcTexture[type];
			}

		}
	}
}
