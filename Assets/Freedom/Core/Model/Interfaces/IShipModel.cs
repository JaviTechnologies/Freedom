﻿namespace Freedom.Core.Model
{
    public interface IShipModel : ITickable
    {
        void Tick (float deltaTime);
    }
}