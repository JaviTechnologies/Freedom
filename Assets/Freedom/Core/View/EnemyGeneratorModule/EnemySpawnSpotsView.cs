using UnityEngine;
using System.Collections;
using Freedom.Core.Controller.Utils;

namespace Freedom.Core.View.EnemyGeneratorModule
{
    public class EnemySpawnSpotsView : MonoBehaviour
    {
        public EnemySpawnGroupView[] spwanGroups;

        public Transform[] GetSpawnPoints ()
        {
            EnemySpawnGroupView spawnGroup = spwanGroups [Random.Range (0, spwanGroups.Length)];
            return spawnGroup.spots;
        }
    }
}