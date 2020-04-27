using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using HamstarHelpers.Services.Configs;


namespace Emitters {
	//class MyFloatInputElement : FloatInputElement { }
	//[CustomModConfigItem( typeof( MyFloatInputElement ) )]




	public partial class EmittersConfig : StackableModConfig {
		public static EmittersConfig Instance => ModConfigStack.GetMergedConfigs<EmittersConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ServerSide;



		////////////////

		[Range( 16 * 16, 16 * 1000 )]
		[DefaultValue( 16 * 96 )]
		public int DustEmitterMinimumRangeBeforeEmit { get; set; } = 16 * 96;
	}
}
