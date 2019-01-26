using UnityEngine;
using System.Collections;
using System;

namespace QuasarGames
{
    /// <summary>
    /// Class is charge to set and get PlayerPrefs values
    /// </summary>
    public class PrefsCenter : MonoBehaviour
    {
        public enum RateUsStatus {NotShownYet, Later, BannedToShow, Rated};

        private static readonly string _rateUsStatus = "RateUsStatus";

        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }



        /// <summary>
        /// Prefs, which shows: Is sound on?
        /// </summary>
        public static bool IsSoundOn
        {
            get { return PlayerPrefsX.GetBool("IsSoundOn"); }
            set { PlayerPrefsX.SetBool("IsSoundOn", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows: Is music muted?
        /// </summary>
        public static string MuteMusic
        {
            get { return PlayerPrefs.GetString("MuteMusic"); }
            set { PlayerPrefs.SetString("MuteMusic", value); PlayerPrefs.Save(); }
        }
        
        /// <summary>
        /// Prefs, which shows: Is SFX muted?
        /// </summary>
        public static string MuteSFX
        {
            get { return PlayerPrefs.GetString("MuteSFX"); }
            set { PlayerPrefs.SetString("MuteSFX", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows language
        /// </summary>
        public static int Language
        {
            get { return PlayerPrefs.GetInt("Language"); }
            set { PlayerPrefs.SetInt("Language", value); PlayerPrefs.Save(); }
        }


        /// <summary>
        /// Prefs, which shows GamesCompleteQTY
        /// </summary>
        public static int GamesCompleteQTY
        {
            get { return PlayerPrefs.GetInt("GamesCompleteQTY"); }
            set { PlayerPrefs.SetInt("GamesCompleteQTY", value); PlayerPrefs.Save(); }
        }
        
        
        /// <summary>
        /// Prefs, which shows: Should we give user coins for login?
        /// </summary>
        public static bool VKLoggedIn
        {
            get { return PlayerPrefsX.GetBool("VKLoggedIn"); }
            set { PlayerPrefsX.SetBool("VKLoggedIn", value); PlayerPrefs.Save(); }
        }


        /// <summary>
        /// Prefs, which shows Coins
        /// </summary>
        public static int Coins
        {
            get { return PlayerPrefs.GetInt("Coins"); }
            set { PlayerPrefs.SetInt("Coins", value); PlayerPrefs.Save(); }
        }



        /// <summary>
        /// Prefs, which shows count of game started
        /// </summary>
        public static int GameStartCount
        {
            get { return PlayerPrefs.GetInt("GameStartCount"); }
            set { PlayerPrefs.SetInt("GameStartCount", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows count of app launched
        /// </summary>
        public static int AppStartCount
        {
            get { return PlayerPrefs.GetInt("AppStartCount"); }
            set { PlayerPrefs.SetInt("AppStartCount", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs, which shows on witch Game Enter was selected 'Rate Later'
        /// </summary>
        public static int LaterRateGameStartSaved
        {
            get { return PlayerPrefs.GetInt("LaterRateGameStart"); }
            set { PlayerPrefs.SetInt("LaterRateGameStart", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Prefs,which show: Is rateUs showed
        /// </summary>
        public static RateUsStatus RateUs
        {
            get
            {
                string strEnum = PlayerPrefs.GetString(_rateUsStatus);
                return string.IsNullOrEmpty(strEnum) ?
                    RateUsStatus.NotShownYet : ParseEnum<RateUsStatus>(strEnum);
            }
        }
       
        /// <summary>
        /// Set 'RateUs' Prefs data
        /// </summary>
        /// <param name="status">Status of rateUs</param>
        public static void SetRateUs(RateUsStatus status)
        {
            PlayerPrefs.SetString(_rateUsStatus, status.ToString());
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Parse from string to enum
        /// </summary>
        /// <param name="value">parsed string</param>
        private static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }


        /// <summary>
        /// Prefs, which shows LeaderboardAuth state
        /// </summary>
        public static bool LeaderboardAutoAuth
        {
            get { return PlayerPrefsX.GetBool("LeaderboardAutoAuth"); }
            set { PlayerPrefsX.SetBool("LeaderboardAutoAuth", value); PlayerPrefs.Save(); }
        }

        /// <summary>
        /// Page Locked/Unlocked state
        /// </summary>
        public static bool GetIsPageUnlocked(string pageID)
        {
            return PlayerPrefsX.GetBool("PageUnlocked_" + pageID);
        }

        public static void SetIsPageUnlocked(string pageID, bool value)
        {
            PlayerPrefsX.SetBool("PageUnlocked_" + pageID, value);
            PlayerPrefs.Save();
        }


        /// <summary>
        /// Level Locked/Unlocked state
        /// </summary>
        public static bool GetIsLevelUnlocked(string levelID)
        {
            return PlayerPrefsX.GetBool("LevelUnlocked_" + levelID);
        }

        public static void SetIsLevelUnlocked(string levelID, bool value)
        {
            PlayerPrefsX.SetBool("LevelUnlocked_" + levelID, value);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Level Best Time (sec)
        /// </summary>
        public static float GetLevelBestTime(string levelID)
        {
            return PlayerPrefs.GetFloat("LevelBestTime_" + levelID);
        }

        public static void SetLevelBestTime(string levelID, float value)
        {
            PlayerPrefs.SetFloat("LevelBestTime_" + levelID, value);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Level Stars Count
        /// </summary>
        public static int GetLevelStars(string levelID)
        {
            return PlayerPrefs.GetInt("LevelStars_" + levelID);
        }

        public static void SetLevelStars(string levelID, int value)
        {
            PlayerPrefs.SetInt("LevelStars_" + levelID, value);
            PlayerPrefs.Save();
        }




        public static bool GetIsItemUnlocked(string ballType)
        {
            return PlayerPrefsX.GetBool(ballType + "Unlocked");
        }

        public static void SetIsItemUnlocked(string ballType, bool value)
        {
            PlayerPrefsX.SetBool(ballType + "Unlocked", value);
            PlayerPrefs.Save();
        }

        public static bool GetIsItemInUse(string ballType)
        {
            return PlayerPrefsX.GetBool(ballType + "InUse");
        }

        public static void SetIsItemInUse(string ballType, bool value)
        {
            PlayerPrefsX.SetBool(ballType + "InUse", value);
            PlayerPrefs.Save();
        }


        /// <summary>
        /// Achievement Unlocked Info
        /// </summary>
        /// <param name="id">Achievement ID</param>
        /// <returns></returns>
        public static bool GetIsAchievUnlocked(string id)
        {
            return PlayerPrefsX.GetBool(id);
        }

        public static void SetIsAchievUnlocked(string id)
        {
            PlayerPrefsX.SetBool(id, true);
            PlayerPrefs.Save();
        }




        // IN APP PURCHASES

        /// <summary>
        /// Prefs, which shows is purchased No Ads
        /// </summary>
        public static bool IsPurchased_NoAds
        {
            get { return PlayerPrefsX.GetBool("IsPurchased_NoAds"); }
            set { PlayerPrefsX.SetBool("IsPurchased_NoAds", value); PlayerPrefs.Save(); }
        }




    }
}