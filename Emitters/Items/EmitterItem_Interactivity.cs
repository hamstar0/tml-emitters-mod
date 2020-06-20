using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using HamstarHelpers.Services.Timers;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
using Emitters.NetProtocols;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class EmitterItem : ModItem, IBaseEmitterItem<EmitterDefinition> {
		public static bool CanViewEmitters( Player plr, bool withWire ) {
			return (withWire && WiresUI.Settings.DrawWires) || (
					plr.HeldItem != null
					&& !plr.HeldItem.IsAir
					&& plr.HeldItem.type == ModContent.ItemType<EmitterItem>() );
		}


		////////////////

		public static bool AttemptEmitterPlacementForCurrentPlayer( EmitterDefinition def ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			ushort tileX = (ushort)(Main.MouseWorld.X / 16);
			ushort tileY = (ushort)(Main.MouseWorld.Y / 16);
			if( myworld.GetEmitter(tileX, tileY) != null ) {
				return false;
			}

			myworld.AddEmitter( new EmitterDefinition(def), tileX, tileY );

			Main.PlaySound( SoundID.Item108, Main.MouseWorld );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				EmitterPlacementProtocol.BroadcastFromClient( def, tileX, tileY );
			}

			return true;
		}

		public static bool AttemptEmitterToggle( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var tileX = (ushort)( worldPos.X / 16f );
			var tileY = (ushort)( worldPos.Y / 16f );

			EmitterDefinition emitter = myworld.GetEmitter( tileX, tileY );
			if( emitter == null ) {
				return false;
			}

			emitter.Activate( !emitter.IsActivated );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				EmitterActivateProtocol.BroadcastFromClient( emitter.IsActivated, tileX, tileY );
			}

			return true;
		}


		////////////////

		private static void AttemptEmitterPickup( Vector2 worldPos ) {
			if( EmitterItem.AttemptEmitterRemove(worldPos) ) {
				ItemHelpers.CreateItem( Main.LocalPlayer.position, ModContent.ItemType<EmitterItem>(), 1, 16, 16 );
			}
		}

		////

		private static bool AttemptEmitterRemove( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			EmitterDefinition emitter = myworld.GetEmitter( tileX, tileY );
			if( emitter == null ) {
				return false;
			}

			if( !myworld.RemoveEmitter( tileX, tileY ) ) {
				return false;
			}

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				EmitterRemoveProtocol.BroadcastFromClient( tileX, tileY );
			} else if( Main.netMode == NetmodeID.Server ) {
				EmitterRemoveProtocol.BroadcastFromServer( tileX, tileY );
			}

			return true;
		}



		////////////////

		/*public override bool CanRightClick() {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			EmitterItem.OpenUI( this.item );

			return false;
		}*/


		////////////////

		public override bool UseItem( Player player ) {
			if( Main.netMode == NetmodeID.Server || player.whoAmI != Main.myPlayer ) {
				return base.UseItem( player );
			}

			string timerName = "EmitterPlace_" + player.whoAmI;
			if( Timers.GetTimerTickDuration(timerName) > 0 ) {
				return base.UseItem( player );
			}
			Timers.SetTimer( timerName, 15, false, () => false );

			if( this.Def == null ) {
				Main.NewText( "Emitter settings must be first specified (click item's button)." );
				return base.UseItem( player );
			}

			if( EmitterItem.AttemptEmitterPlacementForCurrentPlayer(this.Def) ) {
				PlayerItemHelpers.RemoveInventoryItemQuantity( player, this.item.type, 1 );
			} else {
				//EmitterItem.AttemptEmitterToggle( Main.MouseWorld );
			}

			return base.UseItem( player );
		}


		////////////////

		public void OpenUI( Item emitterItem ) {
			var mymod = EmittersMod.Instance;

			mymod.EmitterEditorDialog.Open();
			mymod.EmitterEditorDialog.SetItem( emitterItem );
		}
	}
}