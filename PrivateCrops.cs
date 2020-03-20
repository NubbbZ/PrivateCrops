using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
	[Info("Private Crops", "NubbbZ", "1.0.0")]
	[Description("Protects player's crops from being stolen!")]
	class PrivateCrops : CovalencePlugin
	{
		private void Init()
		{
			permission.RegisterPermission("privatecrops.message.bypass", this);
			permission.RegisterPermission("privatecrops.protection.bypass", this);
		}

		protected override void LoadDefaultMessages()
		{
			lang.RegisterMessages(new Dictionary<string, string>
			{
				["message"] = "That crop is not yours! Do not steal other players crops!",
			}, this);
		}

		private object OnCropGather(PlantEntity plant, Item item, BasePlayer player)
		{
			if (player.IPlayer.HasPermission("privatecrops.protection.bypass") == false)
			{
				if (plant.OwnerID.ToString() != player.IPlayer.Id)
				{
					if (player.IPlayer.HasPermission("privatecrops.message.bypass") == false)
					{
						player.IPlayer.Message(lang.GetMessage("message", this, player.IPlayer.Id));
					}
					return true;
				}
			}
			return null;
		}
		private object CanTakeCutting(BasePlayer player, PlantEntity plant)
		{
			if (player.IPlayer.HasPermission("privatecrops.protection.bypass") == false)
			{
				if (plant.OwnerID.ToString() != player.IPlayer.Id)
				{
					if (player.IPlayer.HasPermission("privatecrops.message.bypass") == false)
					{
						player.IPlayer.Message(lang.GetMessage("message", this, player.IPlayer.Id));
					}
					return true;
				}
			}
			return null;
		}
	}
}
