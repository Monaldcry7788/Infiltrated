using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class List : ICommand
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
                    text.AppendLine().Append($"[INFILTRATED ALIVES ({Infiltrated.Singleton.infiltrates.Count})]").AppendLine();
                    foreach (var infiltrated in Infiltrated.Singleton.infiltrates)
                    {
                        text.AppendLine().Append($"[Player: {infiltrated.Nickname} ({infiltrated.UserId})]").AppendLine()
                        .Append($"[Player Health: {infiltrated.Health}HP]").AppendLine()
                        .Append($"[Player Role: {infiltrated.Role}]").AppendLine();
                    }
                    response = StringBuilderPool.Shared.ToStringReturn(text);
                    return true;
                }
                    response = "Usage: Infiltrated_list/IList";
                    return false;
            }
            response = "<color=red>You can't do this command!</color>";
            return false;
        }
    }
}
