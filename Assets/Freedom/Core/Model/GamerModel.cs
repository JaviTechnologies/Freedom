using Freedom.Core.Model.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using Freedom.Core.Controller;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.Model
{
    public class GamerModel : IGamerModel
    {
        public ILevelModel CurrentLevel { get; set; }

        public int MaxScore { get; set; }

        public void Save ()
        {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Create (Application.persistentDataPath + GameConstants.USER_SAVE_DATA_FILE);
            bf.Serialize (file, GetGamerSerializableData ());
            file.Close ();
        }

        public bool Load ()
        {
            if (File.Exists (Application.persistentDataPath + GameConstants.USER_SAVE_DATA_FILE)) {
                BinaryFormatter bf = new BinaryFormatter ();
                FileStream file = File.Open (Application.persistentDataPath + GameConstants.USER_SAVE_DATA_FILE, FileMode.Open);
                GamerSerializableData data = (GamerSerializableData)bf.Deserialize (file);
                file.Close ();

                if (data != null) {
                    CurrentLevel = LevelFactory.CreateLevelModel (data.levelId);
                    MaxScore = data.maxScore;

                    return true;
                }
            }

            return false;
        }

        private GamerSerializableData GetGamerSerializableData ()
        {
            GamerSerializableData data = new GamerSerializableData ();

            data.levelId = CurrentLevel.id;
            data.maxScore = MaxScore;

            return data;
        }

        [System.Serializable]
        public class GamerSerializableData
        {
            public int levelId;
            public int maxScore;
        }
    }
}