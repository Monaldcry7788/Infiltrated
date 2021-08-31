using CommandSystem;
using System;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Main : ParentCommand
    {
        public Main()
        {
            LoadGeneratedCommands();
        }

        public override string Command { get; } = "infiltrated";
        public override string[] Aliases { get; } = new[] {"infi", "infiltr", "ifd", "if"};
        public override string Description { get; } = string.Empty;

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(InfoPlayer.Instance);
            RegisterCommand(Kill.Instance);
            RegisterCommand(List.Instance);
            RegisterCommand(RandomSpawn.Instance);
            RegisterCommand(Spawn.Instance);
            RegisterCommand(Help.Instance);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender,
            out string response)
        {
            response = string.Format("Please, specify a sub command: Spawn, Kill, RandomSpawn, InfoPlayer, List, help");
            return false;
        }
    }
}