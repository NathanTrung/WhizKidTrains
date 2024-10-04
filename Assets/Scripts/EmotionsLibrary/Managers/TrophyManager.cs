using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using WhizKid.EmotionsLibrary.Book;

namespace WhizKid.EmotionsLibrary
{
    public class TrophyManager : MonoBehaviour
    {
        //! Variables
        #region Serialized Private Fields

        [Header("Achievement UI")]
        [SerializeField] private GameObject achievementPanel;
        [SerializeField] private GameObject achievementIcon;
        [SerializeField] private GameObject achievementName;
        [SerializeField] private TextMeshProUGUI achievementCounterText;  // Added counter text

        [Header("Achievement Trigger Effects")]
        [SerializeField] private GameObject achievementBookPanel;
        [SerializeField] private AudioSource achievement_sfx;

        [Header("Achievements")]
        [SerializeField] private Trophy[] _trophies;

        #endregion
        #region Private Fields

        private int totalAchievements;
        private int achievementsUnlocked;

        #endregion

        //! Methods
        //? Unity
        private void Start()
        {
            if (achievementPanel != null)
            {
                achievementPanel.SetActive(false);
            }

            totalAchievements = _trophies.Length;  // Set the total number of achievements
            achievementsUnlocked = 0;  // Initialize unlocked achievements counter

            InitTrophies();
            UpdateAchievementCounter();  // Initial update of the counter
        }

        //? Public
        public void TriggerAchievement(int id)
        {
            if (id >= 0 && id < _trophies.Length)
            {
                StartCoroutine(_TriggerTrophy(id));  // Trigger the specific achievement
            }
        }

        #region Private Methods
        IEnumerator _TriggerTrophy(int id)
        {
            if (_trophies == null || _trophies.Length <= id || _trophies[id] == null)
            {
                Debug.LogError("Trophy data is missing or out of bounds!");
                yield break;
            }

            if (_trophies[id].Achieved)
            {
                yield break;  // If already achieved, skip
            }
            else
            {
                // Set achievement pop-up data
                _trophies[id].Achieved = true;
                achievementIcon.GetComponent<RawImage>().texture = _trophies[id].unlockedTexture;
                achievementName.GetComponent<TextMeshProUGUI>().text = _trophies[id].Name;

                // Check if achievementPage is valid and has ImagePopout component
                if (_trophies[id].achievementPage != null && _trophies[id].achievementPage.GetComponent<ImagePopout>() != null)
                {
                    _trophies[id].achievementPage.GetComponent<ImagePopout>().canView = true;
                }
                else
                {
                    Debug.LogError($"Trophy {id}'s achievement page is null or missing ImagePopout component.");
                }

                if (_trophies[id].bookTrophySprite != null)
                {
                    _trophies[id].bookTrophySprite.GetComponent<Image>().sprite = _trophies[id].unlockedSprite;
                }
                else
                {
                    Debug.LogError($"Trophy {id} is missing bookTrophySprite.");
                }

                achievementPanel.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                achievementPanel.SetActive(false);

                achievementsUnlocked++;  // Increment unlocked achievements counter
                UpdateAchievementCounter();  // Update the counter display
            }
        }


        private void InitTrophies()
        {
            foreach (Trophy trophy in _trophies)
            {
                trophy.InitTrophy();

                if (trophy.Achieved)
                {
                    trophy.bookTrophySprite.GetComponent<Image>().sprite = trophy.unlockedSprite;
                    trophy.achievementPage.GetComponent<ImagePopout>().canView = true;
                    achievementsUnlocked++;  // Increment unlocked achievements counter if already unlocked
                }
                else
                {
                    trophy.bookTrophySprite.GetComponent<Image>().sprite = trophy.lockedSprite;
                }
            }

            UpdateAchievementCounter();  // Update the counter after initializing trophies
        }

        //* Method to update the achievement counter display
        private void UpdateAchievementCounter()
        {
            if (achievementCounterText != null)
            {
                achievementCounterText.text = $"{achievementsUnlocked}/{totalAchievements}";
            }
        }

        //* Good for achievements dependencies
        private bool _CheckTrophyUnlocked(int id)
        {
            if (_trophies == null) { return false; }
            return _trophies[id].Achieved;
        }

        private bool _CheckAllUnlocked()
        {
            if (_trophies == null) { return false; }

            foreach (Trophy trophy in _trophies)
            {
                if (!trophy.Achieved) { return false; }
            }

            return true;
        }

        #endregion
    }
}

