﻿using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Weywey.Core.Services;

namespace Weywey.Core.Commands.Help;

public partial class HelpModule : ModuleBase<SocketCommandContext>
{
    [Name("Help")]
    [Command("help", RunMode = RunMode.Async)]
    [Summary("Shows the help page.")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    public async Task HelpCommand([Remainder][Summary("Command or module's name to get help")] string command = null)
    {
        var Commands = ProviderService.GetService<CommandService>();

        if (command == null)
        {
            var embed = new EmbedBuilder()
                .WithFooter(footer =>
                {
                    footer.Text = $"Requested by {Context.User}";
                    footer.IconUrl = Context.User.GetAvatarUrl();
                })
                .WithTitle($"{Context.Client.CurrentUser.Username}'s Help Page")
                .WithDescription($"Type `{ConfigurationService.Prefix}help <command|module>` to get help about a command or module. \n\nModules:\n {string.Join('\n', Commands.Modules.Select(x => $"`{x.Name}`"))}")
                .WithColor(Color.Teal)
                .WithCurrentTimestamp().Build();

            await ReplyAsync(embed: embed);
            return;
        }

        command = command.ToLower();

        var module = Commands.Modules.FirstOrDefault(x => x.Name.ToLower() == command);
        if (module != null)
        {
            var embed = new EmbedBuilder()
                .WithFooter(footer =>
                {
                    footer.Text = $"Requested by {Context.User}";
                    footer.IconUrl = Context.User.GetAvatarUrl();
                })
                .WithTitle($"{module.Name.SeperateFromCaps()}'s Help Page")
                .WithDescription(string.Join('\n', module.Commands.Select(x => $"`{x.Name}` >> {x.Summary}")))
                .WithColor(Color.Teal)
                .WithCurrentTimestamp().Build();

            await ReplyAsync(embed: embed);
            return;
        }

        var cmd = Commands.Commands.FirstOrDefault(x => x.Name.ToLower() == command || x.Aliases.Contains(command));
        if (cmd != null)
        {
            var embed = new EmbedBuilder()
                .WithFooter(footer =>
                {
                    footer.Text = $"Requested by {Context.User}";
                    footer.IconUrl = Context.User.GetAvatarUrl();
                })
                .WithTitle($"{cmd.Name}")
                .AddField("Summary", cmd.Summary, false)
                .AddField("Syntax", cmd.GetSyntax(), false)
                .AddField("Module", cmd.Module.Name.Replace("Module", ""), false)
                .AddField("Parameters", cmd.Parameters.Count > 0 ? string.Join('\n', cmd.Parameters.Select(x => $"`{x.Name}` -> {x.Summary} {(x.IsOptional ? "(Optional)" : "")}")) : "No parameters reqired.", false)
                .AddField("Permissions", (cmd.Preconditions.Where(x => x is RequireUserPermissionAttribute).Count() > 0) ? string.Join('\n', cmd.Preconditions.Where(x => x is RequireUserPermissionAttribute).Select(x => (((x as RequireUserPermissionAttribute) is var p && p.GuildPermission.HasValue) ? p.GuildPermission.ToString() : p.ChannelPermission.ToString()).SeperateFromCaps())) : "No permission required.", false)
                .WithColor(Color.Teal).Build();

            await ReplyAsync(embed: embed);
            return;
        }

        await ReplyAsync($"Command or module not found with `{command}`.");
    }
}
