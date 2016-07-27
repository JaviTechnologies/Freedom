using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.EnemySpawnModule
{
    public class EnemySpawnSpotsView : MonoBehaviour
    {
        public EnemySpawnGroupView[] spwanGroups;

        public Transform[] GetSpawnPoints ()
        {
            EnemySpawnGroupView spawnGroup = spwanGroups [Random.Range (0, spwanGroups.Length - 1)];
            return spawnGroup.spots;
        }
    }
}