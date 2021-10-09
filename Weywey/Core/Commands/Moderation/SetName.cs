﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Weywey.Core.Modules.Moderation
{
    public partial class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [Name("Set Name")]
        [Command("setname", RunMode = RunMode.Async)]
        [Summary("Sets a user nickname in guild.")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        public async Task SetNicknameCommand([Summary("That the name will be set.")] SocketGuildUser user, [Remainder] [Summary("The name to set.")] string nickname = "")
        {
            await user.ModifyAsync(x => x.Nickname = nickname);
            await ReplyAsync($"{user}'s nickname set to {nickname}");
        }
    }
}