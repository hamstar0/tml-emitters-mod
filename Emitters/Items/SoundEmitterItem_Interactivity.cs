using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Timers;
using Emitters.NetProtocols;
using Emitters.Definitions;


namespace Emitters.Items {
	public partial class SoundEmitterItem : ModItem, IBaseEmitterItem<SoundEmitterDefinition> {
		public static bool CanViewSoundEmitters( Player plr, bool withWire ) {
			return (withWire && WiresUI.Settings.DrawWires) || (
					plr.HeldItem != null
					&& !plr.HeldItem.IsAir
					&& plr.HeldItem.type == ModContent.ItemType<SoundEmitterItem>() );
		}


		////////////////

		public static bool AttemptSoundEmitterPlacementForCurrentPlayer( SoundEmitterDefinition def ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			ushort tileX = (ushort)( Main.MouseWorld.X / 16 );
			ushort tileY = (ushort)( Main.MouseWorld.Y / 16 );
			if( myworld.GetSoundEmitter( tileX, tileY ) != null ) {
				return false;
			}

			myworld.AddSoundEmitter( new SoundEmitterDefinition(def), tileX, tileY );

			Main.PlaySound( SoundID.Item108, Main.MouseWorld );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				SoundEmitterPlacementProtocol.BroadcastFromClient( def, tileX, tileY );
			}

			return true;
		}

		public static bool AttemptSoundEmitterToggle( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var tileX = (ushort)( worldPos.X / 16f );
			var tileY = (ushort)( worldPos.Y / 16f );

			SoundEmitterDefinition sndEmitter = myworld.GetSoundEmitter( tileX, tileY );
			if( sndEmitter == null ) {
				return false;
			}

			sndEmitter.Activate( !sndEmitter.IsActivated );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				SoundEmitterPlacementProtocol.BroadcastFromClient( sndEmitter, tileX, tileY );
			}

			return true;
		}


		////////////////

		private static void AttemptSoundEmitterPickup( Vector2 worldPos ) {
			if( SoundEmitterItem.AttemptSoundEmitterRemove( worldPos ) ) {
				ItemHelpers.CreateItem( Main.LocalPlayer.position, ModContent.ItemType<SoundEmitterItem>(), 1, 16, 16 );
			}
		}

		////

		private static bool AttemptSoundEmitterRemove( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			SoundEmitterDefinition soundEmitter = myworld.GetSoundEmitter( tileX, tileY );
			if( soundEmitter == null ) {
				return false;
			}

			if( !myworld.RemoveSoundEmitter( tileX, tileY ) ) {
				return false;
			}

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				SoundEmitterRemoveProtocol.BroadcastFromClient( tileX, tileY );
			} else if( Main.netMode == NetmodeID.Server ) {
				SoundEmitterRemoveProtocol.BroadcastFromServer( tileX, tileY );
			}

			return true;
		}



		////////////////

		/*public override bool CanRightClick() {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			SoundEmitterItem.OpenUI( this.item );

			return false;
		}*/


		////////////////

		public override bool UseItem( Player player ) {
			if( Main.netMode == NetmodeID.Server || player.whoAmI != Main.myPlayer ) {
				return base.UseItem( player );
			}

			string timerName = "SoundEmitterPlace_" + player.whoAmI;
			if( Timers.GetTimerTickDuration( timerName ) > 0 ) {
				return base.UseItem( player );
			}
			Timers.SetTimer( timerName, 15, false, () => false );

			if( this.Def == null ) {
				Main.NewText( "Sound Emitter settings must be first specified (click item's button)." );
				return base.UseItem( player );
			}

			if( SoundEmitterItem.AttemptSoundEmitterPlacementForCurrentPlayer( this.Def ) ) {
				PlayerItemHelpers.RemoveInventoryItemQuantity( player, this.item.type, 1 );
			} else {
				//SoundEmitterItem.AttemptSoundEmitterToggle( Main.MouseWorld );
			}

			return base.UseItem( player );
		}


		////////////////

		public void OpenUI( Item soundEmitterItem ) {
			var mymod = EmittersMod.Instance;
			mymod.SoundEmitterEditorDialog.Open();
			mymod.SoundEmitterEditorDialog.SetItem( soundEmitterItem );
		}
	}
}