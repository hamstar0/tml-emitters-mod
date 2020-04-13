using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;


namespace Emitters {
	public class EmitterTypeDefinition {
		public string Name { get; }
		public int Type { get; }
	}




	public class EmitterDefinition {
		public EmitterTypeDefinition Type { get; }
		public float Scale { get; }
		public float SpeedX { get; }
		public float SpeedY { get; }
		public Color Color { get; }
		public float Alpha { get; }
		public float Scatter { get; }
		public bool HasGravity { get; }
		public bool HasLight { get; }
	}
}
