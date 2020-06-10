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
	public partial class HologramItem : ModItem, IBaseEmitterItem {
		public static bool CanViewHolograms( Player plr, bool withWire ) {
			return (withWire && WiresUI.Settings.DrawWires) || (
					plr.HeldItem != null
					&& !plr.HeldItem.IsAir
					&& plr.HeldItem.type == ModContent.ItemType<HologramItem>() );
		}


		////////////////

		public static bool AttemptHologramPlacementForCurrentPlayer( HologramDefinition def ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();

			ushort tileX = (ushort)( Main.MouseWorld.X / 16 );
			ushort tileY = (ushort)( Main.MouseWorld.Y / 16 );
			if( myworld.GetHologram(tileX, tileY) != null ) {
				return false;
			}

			myworld.AddHologram( new HologramDefinition(def), tileX, tileY );

			Main.PlaySound( SoundID.Item108, Main.MouseWorld );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				HologramPlacementProtocol.BroadcastFromClient( def, tileX, tileY );
			}

			return true;
		}

		public static bool AttemptHologramToggle( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var tileX = (ushort)( worldPos.X / 16f );
			var tileY = (ushort)( worldPos.Y / 16f );

			HologramDefinition hologram = myworld.GetHologram( tileX, tileY );
			if( hologram == null ) {
				return false;
			}

			hologram.Activate( !hologram.IsActivated );

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				HologramActivateProtocol.BroadcastFromClient( hologram.IsActivated, tileX, tileY );
			}

			return true;
		}


		////////////////

		private static void AttemptHologramPickup( Vector2 worldPos ) {
			if( HologramItem.AttemptHologramRemove( worldPos ) ) {
				ItemHelpers.CreateItem( Main.LocalPlayer.position, ModContent.ItemType<HologramItem>(), 1, 16, 16 );
			}
		}

		////

		private static bool AttemptHologramRemove( Vector2 worldPos ) {
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			HologramDefinition hologram = myworld.GetHologram( tileX, tileY );
			if( hologram == null ) {
				return false;
			}

			if( !myworld.RemoveHologram( tileX, tileY ) ) {
				return false;
			}

			if( Main.netMode == NetmodeID.MultiplayerClient ) {
				HologramRemoveProtocol.BroadcastFromClient( tileX, tileY );
			} else if( Main.netMode == NetmodeID.Server ) {
				HologramRemoveProtocol.BroadcastFromServer( tileX, tileY );
			}

			return true;
		}



		////////////////

		/*public override bool CanRightClick() {
			return true;
		}

		public override bool ConsumeItem( Player player ) {
			HologramItem.OpenUI( this.item );

			return false;
		}*/


		////////////////

		public override bool UseItem( Player player ) {
			if( Main.netMode == NetmodeID.Server || player.whoAmI != Main.myPlayer ) {
				return base.UseItem( player );
			}

			string timerName = "HologramPlace_" + player.whoAmI;
			if( Timers.GetTimerTickDuration( timerName ) > 0 ) {
				return base.UseItem( player );
			}
			Timers.SetTimer( timerName, 15, false, () => false );

			if( this.Def == null ) {
				Main.NewText( "Hologram settings must be first specified (right-click item)." );
				return base.UseItem( player );
			}

			if( HologramItem.AttemptHologramPlacementForCurrentPlayer( this.Def ) ) {
				PlayerItemHelpers.RemoveInventoryItemQuantity( player, this.item.type, 1 );
			} else {
				//HologramItem.AttemptHologramToggle( Main.MouseWorld );
			}

			return base.UseItem( player );
		}


		////////////////

		public void OpenUI( Item hologramItem ) {
			var mymod = EmittersMod.Instance;
			mymod.HologramEditorDialog.Open();

			if( !mymod.HologramEditorDialog.SetItem( hologramItem ) ) {
				mymod.HologramEditorDialog.Close();
			}
		}
	}
}

