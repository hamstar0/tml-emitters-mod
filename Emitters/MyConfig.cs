using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace Emitters {
	//class MyFloatInputElement : FloatInputElement { }
	//[CustomModConfigItem( typeof( MyFloatInputElement ) )]




	public partial class EmittersConfig : ModConfig {
		public static EmittersConfig Instance => ModContent.GetInstance<EmittersConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		public bool DebugModeInfo { get; set; } = false;

		public bool DebugModeNetInfo { get; set; } = false;

		//

		[DefaultValue( true )]
		public bool EmitterRecipeEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool SoundEmitterRecipeEnabled { get; set; } = true;

		[DefaultValue( true )]
		public bool HologramRecipeEnabled { get; set; } = true;

		//
		
		[Range( 16 * 16, 16 * 1000 )]
		[DefaultValue( 16 * 96 )]
		public int DustEmitterMinimumRangeBeforeEmit { get; set; } = 16 * 96;

		[Range( 16 * 16, 16 * 1000 )]
		[DefaultValue( 16 * 192 )]
		public int SoundEmitterMinimumRangeBeforeEmit { get; set; } = 16 * 192;

		[Range( 16 * 16, 16 * 1000 )]
		[DefaultValue( 16 * 160 )]
		public int HologramMinimumRangeBeforeProject { get; set; } = 16 * 160;
	}
}
