using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class PlayerData
    {
        public int lastScore;
        public int bestScore;

        public PlayerData()
        {
            lastScore = 0;
            bestScore = 0;
        }
    }
 
    public class SaveSystem
    {
        private PlayerData loadedData;
        public PlayerData LoadedData => loadedData;


        public void Load()
        {
            if(PlayerPrefs.HasKey("SAVE_KEY"))
            {
                var json = PlayerPrefs.GetString("SAVE_KEY");
                loadedData = JsonUtility.FromJson<PlayerData>(json);
            }
            else
            {
                loadedData = new PlayerData();
                Save();
            }
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(loadedData);
            PlayerPrefs.SetString("SAVE_KEY", json);
        }
    }
}