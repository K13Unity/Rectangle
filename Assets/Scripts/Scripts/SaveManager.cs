using UnityEngine;

namespace Scripts
{
    public class SaveManager 
    {
        internal readonly string _levelKey = "Level";

        public int GetLevelIndex()
        {
            return PlayerPrefs.GetInt(_levelKey, 0);
        }

        public void SaveLevelIndex(int levelIndex)
        {
            PlayerPrefs.SetInt(_levelKey, levelIndex);
        }

        public void ResetProgress()
        {
            PlayerPrefs.DeleteKey(_levelKey);
        }
    }
}
