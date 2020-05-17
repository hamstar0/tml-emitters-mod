using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace Emitters.Items
{
	public partial class SoundEmitterItem : ModItem
	{
		public SoundEmitterDefinition Def { get; private set; } = null;


		////////////////

		public override bool CloneNewInstances => false;



		////////////////

		public override ModItem Clone(Item item)
		{
			var myclone = (SoundEmitterItem)base.Clone(item);
			myclone.Def = this.Def;

			return myclone;
		}

		public override ModItem Clone()
		{
			var myclone = (SoundEmitterItem)base.Clone();
			myclone.Def = this.Def;

			return myclone;
		}


		////////////////

		public override void SetStaticDefaults()
		{
			this.DisplayName.SetDefault("Sound Emitter");
			this.Tooltip.SetDefault("Spews particles."
				+ "\n" + "Place on a tile to apply effect"
				+ "\n" + "Emitters may be wire controlled"
			);
		}

		public override void SetDefaults()
		{
			this.item.width = 12;
			this.item.height = 12;
			this.item.maxStack = 999;
			this.item.useTurn = true;
			this.item.autoReuse = true;
			this.item.useAnimation = 15;
			this.item.useTime = 10;
			this.item.useStyle = 1;
			this.item.consumable = true;
			//this.item.createTile = ModContent.TileType<EmitterTile>();
		}


		////////////////

		public override void Load(TagCompound tag)
		{
			if (!tag.ContainsKey("EmitterMode"))
			{
				return;
			}

			try
			{
				this.Def = new SoundEmitterDefinition(
					type: tag.GetInt("EmitterType"),
					style: tag.GetInt("EmitterStyle"),
					volume: tag.GetFloat("EmitterVolume"),
					pitch: tag.GetFloat("EmitterPitch"),
					delay: tag.GetInt("EmitterDelay"),
					isActivated: tag.GetBool("EmitterIsActivated")
				);
			}
			catch { }
		}

		public override TagCompound Save()
		{
			if (this.Def == null)
			{
				return new TagCompound();
			}

			return new TagCompound {
				{ "EmitterType", (int)this.Def.Type },
				{ "EmitterStyle", (float)this.Def.Style },
				{ "EmitterVolume", (float)this.Def.Volume },
				{ "EmitterPitch", (float)this.Def.Pitch },
				{ "EmitterDelay", (int)this.Def.Delay },
				{ "EmitterIsActivated", (bool)this.Def.IsActivated },
			};
		}


		////////////////

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Insert(0, new TooltipLine(this.mod, "EmitterUI", "[c/00FF00:Right-click in inventory to adjust settings]"));
			tooltips.Insert(1, new TooltipLine(this.mod, "EmitterToggle", "[c/00FF00:Left-click in world to toggle activation]"));
			tooltips.Insert(2, new TooltipLine(this.mod, "EmitterRemove", "[c/00FF00:Right-click in world to remove]"));

			if (this.Def == null)
			{
				return;
			}

			var typeTip = new TooltipLine(this.mod, "EmitterType", " Type: " + this.Def?.RenderType());
			var VolumeTip = new TooltipLine(this.mod, "EmitterVolume", " Volume: " + this.Def?.RenderVolume());
			var delayTip = new TooltipLine(this.mod, "EmitterDelay", " Delay: " + this.Def?.RenderDelay());

			var color = Color.White * 0.75f;
			typeTip.overrideColor = color;
			VolumeTip.overrideColor = color;
			delayTip.overrideColor = color;

			tooltips.Add(typeTip);
			tooltips.Add(VolumeTip);
			tooltips.Add(delayTip);
		}


		////////////////

		public void SetSoundEmitterDefinition(SoundEmitterDefinition def)
		{
			//Main.NewText( def.ToString() );
			this.Def = def;
		}


		////////////////

		public override void UpdateInventory(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				this.UpdateForCurrentPlayer();
			}
		}

		private void UpdateForCurrentPlayer()
		{
			if (SoundEmitterItem.CanViewSoundEmitters(Main.LocalPlayer))
			{
				this.UpdateInterface();
			}
		}

		////

		private void UpdateInterface()
		{
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				//	this.AttemptEmitterToggle( Main.MouseWorld );
			}
			else if (Main.mouseRight && Main.mouseRightRelease)
			{
				SoundEmitterItem.AttemptSoundEmitterPickup(Main.MouseWorld);
			}
		}
	}
}