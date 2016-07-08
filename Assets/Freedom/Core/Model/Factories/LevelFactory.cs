using UnityEngine;
using System.Collections;

namespace Freedom.Core.Model {
    public class LevelFactory {
        public static ILevelModel CreateLevelModel (int id) {
            ILevelModel level = new LevelModel ();

            // asign first level by default
            level.id = id;

            return level;
        }
    }
}