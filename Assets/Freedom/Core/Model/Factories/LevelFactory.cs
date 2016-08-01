using UnityEngine;
using System.Collections;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.Model.Factories
{
    public class LevelFactory
    {
        public static ILevelModel CreateLevelModel (int id)
        {
            ILevelModel level = new LevelModel ();

            // asign first level by default
            level.id = id;

            return level;
        }
    }
}