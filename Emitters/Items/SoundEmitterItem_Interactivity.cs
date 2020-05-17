using Emitters.NetProtocols;
using HamstarHelpers.Helpers.Items;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Services.Timers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emitters.Items
{
	public partial class SoundEmitterItem : ModItem
	{
		public static void OpenUI(Item SoundEmitterItem)
		{
			var mymod = EmittersMod.Instance;
			var myitem = SoundEmitterItem.modItem as SoundEmitterItem;
			mymod.SoundEmitterEditorDialog.Open();
			mymod.SoundEmitterEditorDialog.SetItem(SoundEmitterItem);

			while (mymod.SoundEmitterEditorDialog.IsOpen)
			{
				if (myitem.Def == null)
				{
					return;
				}

				if (!myitem.Def.IsActivated)
				{
					return;
				}

				if (myitem.Def.Timer++ < myitem.Def.Delay)
				{
					return;
				}
				myitem.Def.Timer = 0;
				Main.PlaySound(myitem.Def.Type, -1, -1, myitem.Def.Style, myitem.Def.Volume, myitem.Def.Pitch);
			}

		}


		////////////////

		public static bool CanViewSoundEmitters(Player plr)
		{
			return !plr.HeldItem.IsAir && plr.HeldItem.type == ModContent.ItemType<SoundEmitterItem>();
		}


		////////////////

		public static bool AttemptSoundEmitterPlacementForCurrentPlayer(SoundEmitterDefinition def)
		{
			var myworld = ModContent.GetInstance<EmittersWorld>();

			ushort tileX = (ushort)(Main.MouseWorld.X / 16);
			ushort tileY = (ushort)(Main.MouseWorld.Y / 16);
			if (myworld.GetSoundEmitter(tileX, tileY) != null)
			{
				return false;
			}

			myworld.AddSoundEmitter(new SoundEmitterDefinition(def), tileX, tileY);

			Main.PlaySound(SoundID.Item108, Main.MouseWorld);

			if (Main.netMode == 1)
			{
				SoundEmitterPlacementProtocol.BroadcastFromClient(def, tileX, tileY);
			}

			return true;
		}

		public static bool AttemptSoundEmitterToggle(Vector2 worldPos)
		{
			var myworld = ModContent.GetInstance<EmittersWorld>();
			var tileX = (ushort)(worldPos.X / 16f);
			var tileY = (ushort)(worldPos.Y / 16f);

			SoundEmitterDefinition SoundEmitter = myworld.GetSoundEmitter(tileX, tileY);
			if (SoundEmitter == null)
			{
				return false;
			}

			SoundEmitter.Activate(!SoundEmitter.IsActivated);

			if (Main.netMode == 1)
			{
				EmitterActivateProtocol.BroadcastFromClient(SoundEmitter.IsActivated, tileX, tileY);
			}

			return true;
		}


		////////////////

		private static void AttemptSoundEmitterPickup(Vector2 worldPos)
		{
			if (SoundEmitterItem.AttemptSoundEmitterRemove(worldPos))
			{
				ItemHelpers.CreateItem(Main.LocalPlayer.position, ModContent.ItemType<SoundEmitterItem>(), 1, 16, 16);
			}
		}

		////

		private static bool AttemptSoundEmitterRemove(Vector2 worldPos)
		{
			var myworld = ModContent.GetInstance<EmittersWorld>();
			Vector2 tilePos = worldPos / 16f;
			var tileX = (ushort)tilePos.X;
			var tileY = (ushort)tilePos.Y;

			SoundEmitterDefinition SoundEmitter = myworld.GetSoundEmitter(tileX, tileY);
			if (SoundEmitter == null)
			{
				return false;
			}

			if (!myworld.RemoveSoundEmitter(tileX, tileY))
			{
				return false;
			}

			if (Main.netMode == 1)
			{
				EmitterRemoveProtocol.BroadcastFromClient(tileX, tileY);
			}
			else if (Main.netMode == 2)
			{
				EmitterRemoveProtocol.BroadcastFromServer(tileX, tileY);
			}

			return true;
		}



		////////////////

		public override bool CanRightClick()
		{
			return true;
		}

		public override bool ConsumeItem(Player player)
		{
			SoundEmitterItem.OpenUI(this.item);

			return false;
		}


		////////////////

		public override bool UseItem(Player player)
		{
			if (Main.netMode == 2 || player.whoAmI != Main.myPlayer)
			{
				return base.UseItem(player);
			}

			string timerName = "SoundEmitterPlace_" + player.whoAmI;
			if (Timers.GetTimerTickDuration(timerName) > 0)
			{
				return base.UseItem(player);
			}
			Timers.SetTimer(timerName, 15, false, () => false);

			if (this.Def == null)
			{
				Main.NewText("SoundEmitter settings must be first specified (right-click item).");
				return base.UseItem(player);
			}

			if (SoundEmitterItem.AttemptSoundEmitterPlacementForCurrentPlayer(this.Def))
			{
				PlayerItemHelpers.RemoveInventoryItemQuantity(player, this.item.type, 1);
			}
			else
			{
				SoundEmitterItem.AttemptSoundEmitterToggle(Main.MouseWorld);
			}

			return base.UseItem(player);
		}
	}
}