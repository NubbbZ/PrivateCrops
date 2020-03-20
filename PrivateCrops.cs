using System.Collections.Generic;

namespace Oxide.Plugins
{
	[Info("Private Crops", "NubbbZ", "1.0.1")]
	[Description("Protects player's crops from being stolen!")]
	class PrivateCrops : CovalencePlugin
	{
		private const string messagebypass = "privatecrops.message.bypass";
		private const string protectionbypass = "privatecrops.protection.bypass";

		private void Init()
		{
			permission.RegisterPermission(messagebypass, this);
			permission.RegisterPermission(protectionbypass, this);
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
				if (plant.OwnerID != player.userID)
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
				if (plant.OwnerID != player.userID)
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
