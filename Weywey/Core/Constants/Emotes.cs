﻿using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weywey.Core.Constants
{
    public static class Emotes
    {
        public static List<IEmote> DirectionEmotes = new List<IEmote>() { new Emoji("⏪"), new Emoji("◀️"), new Emoji("⏹"), new Emoji("▶️"), new Emoji("⏩"), new Emoji("🔢") };
        public static List<IEmote> Numbers = new List<IEmote>() { new Emoji("0️⃣"), new Emoji("1️⃣"), new Emoji("2⃣"), new Emoji("3⃣"), new Emoji("4⃣"), new Emoji("5⃣"), new Emoji("6⃣"), new Emoji("7⃣"), new Emoji("8⃣"), new Emoji("9⃣"), new Emoji("🔟") };
    }
}
