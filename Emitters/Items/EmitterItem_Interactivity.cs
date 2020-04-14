using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Items;
using Emitters.NetProtocols;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem {
		public static void OpenUI( Item emitterItem ) {
			var mymod = EmittersMod.Instance;

			mymod.EmitterEditorDialog.SetItem( emitterItem );
			mymod.EmitterEditorDialog.Open();
		}



		////////////////

		public static bool CanViewEmitters( Player plr ) {
			return !plr.HeldItem.IsAir && plr.HeldItem.type == ModContent.ItemType<EmitterItem>();
		}


		////

		public static void AttemptEmitterPlacementForCurrentPlayer( EmitterDefinition def ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			ushort tileX = (ushort)Main.MouseWorld.X;
			ushort tileY = (ushort)Main.MouseWorld.Y;

			myworld.AddEmitter( def, tileX, tileY );

			Main.PlaySound( SoundID.Item108, Main.MouseWorld );

			if( Main.netMode == 1 ) {
				EmitterPlacementProtocol.BroadcastFromClient( def, tileX, tileY );
			}
		}


		////////////////

		private void AttemptEmitterPickup( Vector2 worldPos ) {
			if( this.AttemptEmitterRemove(worldPos) ) {
				ItemHelpers.CreateItem( Main.LocalPlayer.position, ModContent.ItemType<EmitterItem>(), 1, 16, 16 );
			}
		}

		////

		private bool AttemptEmitterRemove( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			EmitterDefinition emitter = myworld.GetEmitter( tileX, tileY );
			if( emitter == null ) {
				return false;
			}

			return myworld.RemoveEmitter( tileX, tileY );
		}


		////////////////

		public override bool CanRightClick() {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			EmitterItem.OpenUI( this.item );

			return false;
		}


		////////////////

		public override bool UseItem( Player player ) {
			if( Main.netMode == 2 || player.whoAmI != Main.myPlayer ) {
				return base.UseItem( player );
			}

			string timerName = "EmitterPlace_" + player.whoAmI;

			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return base.UseItem( player );
			}
			Timers.SetTimer( timerName, 4, false, () => false );

			EmitterItem.AttemptEmitterPlacementForCurrentPlayer( this.Def );

			return base.UseItem( player );
		}
	}
}