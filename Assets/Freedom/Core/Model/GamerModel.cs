﻿using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.Model
{
    public class GamerModel : IGamerModel
    {
        public ILevelModel CurrentLevel { get; set; }
    }
}