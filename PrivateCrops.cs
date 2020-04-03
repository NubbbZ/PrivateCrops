using System;
using System.Collections.Generic;

namespace Oxide.Plugins
{
	[Info("Private Crops", "NubbbZ", "1.1.1")]
	[Description("Protects player's crops from being stolen!")]
	class PrivateCrops : CovalencePlugin
	{
		#region Variables
		private const string messagebypass = "privatecrops.message.bypass";
		private const string protectionbypass = "privatecrops.protection.bypass";
		#endregion

		#region Setup
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

		protected override void LoadDefaultConfig()
		{
			LogWarning("Creating a new configuration file");
			Config["ToolCupboardArea"] = true;
		}
		#endregion

		#region Hooks
		private object OnCropGather(GrowableEntity plant, Item item, BasePlayer player)
		{
			try
			{
				protection(player, plant);
			}
			catch (Exception ex)
			{
				if (ex.Message == "null")
				{
					return null;
				}
				else
				{
					return Convert.ToBoolean(ex.Message);
				}
			}
			return null;
		}
		private object CanTakeCutting(BasePlayer player, GrowableEntity plant)
		{
			try
			{
				protection(player, plant);
			}
			catch (Exception ex)
			{
				if (ex.Message == "null")
				{
					return null;
				}
				else
				{
					return Convert.ToBoolean(ex.Message);
				}
			}
			return null;
		}
		#endregion

		#region Helpers
		public void protection(BasePlayer player, GrowableEntity plant)
		{
			BuildingPrivlidge TC = player.GetBuildingPrivilege();

			if ((bool)Config["ToolCupboardArea"] == true)
			{
				if (player.IPlayer.HasPermission("privatecrops.protection.bypass") == false)
				{
					if (TC == null)
					{
						throw new Exception("null");
					}
					else
					{
						if (TC.IsAuthed(player) == true)
						{
							throw new Exception("null");
						}
						else
						{
							warning(player);
							throw new Exception("true");
						}
					}
				}
			}
			else
			{
				if (player.IPlayer.HasPermission("privatecrops.protection.bypass") == false)
				{
					if (plant.OwnerID != player.userID)
					{
						warning(player);
						throw new Exception("true");
					}
				}
			}
		}

		public void warning(BasePlayer player)
		{
			if (player.IPlayer.HasPermission("privatecrops.message.bypass") == false)
			{
				player.IPlayer.Message(lang.GetMessage("message", this, player.IPlayer.Id));
			}
		}
		#endregion
	}
}
