﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfApp.Entities
{
    public class RolePlayGame : BoardGame
    {
        public const string? Category = "Role Play Game";
        public override string ToString() => base.ToString() + "(Role Play Game)";
    }
}
