using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
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

		public static bool AttemptEmitterPlacementForCurrentPlayer( EmitterDefinition def ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			ushort tileX = (ushort)(Main.MouseWorld.X / 16);
			ushort tileY = (ushort)(Main.MouseWorld.Y / 16);
			if( myworld.GetEmitter(tileX, tileY) != null ) {
				return false;
			}

			myworld.AddEmitter( new EmitterDefinition(def), tileX, tileY );

			Main.PlaySound( SoundID.Item108, Main.MouseWorld );

			if( Main.netMode == 1 ) {
				EmitterPlacementProtocol.BroadcastFromClient( def, tileX, tileY );
			}

			return true;
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

		public void AttemptEmitterToggle( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			EmitterDefinition emitter = myworld.GetEmitter( tileX, tileY );
			if( emitter == null ) {
				return;
			}

			emitter.Activate( !emitter.IsActivated );

			if( Main.netMode == 1 ) {
				EmitterActivateProtocol.BroadcastFromClient( emitter.IsActivated, tileX, tileY );
			}
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
			Timers.SetTimer( timerName, 15, false, () => false );

			if( this.Def == null ) {
				Main.NewText( "Emitter settings must be first specified (right-click item)." );
				return base.UseItem( player );
			}

			if( EmitterItem.AttemptEmitterPlacementForCurrentPlayer(this.Def) ) {
				PlayerItemHelpers.RemoveInventoryItemQuantity( player, this.item.type, 1 );
			} else {
				this.AttemptEmitterToggle( Main.MouseWorld );
			}

			return base.UseItem( player );
		}
	}
}