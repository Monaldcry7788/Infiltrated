using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class List : ICommand
    {
        public string Command { get; } = "Infiltrated_list";

        public string[] Aliases { get; } = new[] { "IList", "infiltr_list" };

        public string Description { get; } = "Show all Infiltrated alives";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission("infiltrated.list"))
            {
                if (arguments.Count == 0)
                {

                    StringBuilder text = StringBuilderPool.Shared.Rent();
                    text.AppendLine().Append($"[INFILTRATED ALIVES ({Infiltrated.Instance.infiltrates.Count})]").AppendLine();
                    foreach (var infiltrated in Infiltrated.Instance.infiltrates)
                    {
                        text.AppendLine().Append($"Player: {infiltrated.Nickname} ({infiltrated.UserId})").AppendLine();
                    }
                    response = text.ToString();
                    return true;
                }


                else if (arguments.Count != 0)
                {
                    response = $"Usage: {Command}/IList";
                    return false;
                }
            }
            else
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            response = null;
            return false;
        }

    }
}
