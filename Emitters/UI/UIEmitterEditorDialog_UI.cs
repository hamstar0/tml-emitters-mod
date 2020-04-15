﻿using System;
using Terraria.ModLoader;
using HamstarHelpers.Classes.Errors;
using HamstarHelpers.Classes.UI.Elements;
using HamstarHelpers.Helpers.DotNET.Reflection;


namespace Emitters.UI {
	partial class UIEmitterEditorDialog : UIDialog {
		private bool IsSettingMode = false;



		////////////////

		public void SetGoreMode( bool isGoreMode ) {
			if( this.IsSettingMode ) { return; }
			this.IsSettingMode = true;

			this.ModeDustFlagElem.Selected = !isGoreMode;
			this.ModeGoreFlagElem.Selected = isGoreMode;

			if( !isGoreMode ) {
				int dustCount;
				if( !ReflectionHelpers.Get( typeof( ModDust ), null, "DustCount", out dustCount ) ) {
					throw new ModHelpersException( "Could not get dust count." );
				}

				this.TypeSliderElem.SetRange( 0, dustCount );
			} else {
				int goreCount;
				if( !ReflectionHelpers.Get( typeof( ModGore ), null, "GoreCount", out goreCount ) ) {
					throw new ModHelpersException( "Could not get gore count." );
				}

				this.TypeSliderElem.SetRange( 0, goreCount );
			}

			this.IsSettingMode = false;
		}
	}
}
