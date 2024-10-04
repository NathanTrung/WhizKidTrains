using UnityEngine;
using UnityEngine.UI;

namespace WhizKid.EmotionsLibrary.Book
{
    public class ImagePopout : MonoBehaviour
    {
        public bool canView = false;

        //* Assign in Inspector
        [Header("Emotion Card Elements")]
        [SerializeField] private GameObject popoutImage;
        [SerializeField] private Button imageButton;
        [SerializeField] private Button exitButton;


        private void Start()
        {
            popoutImage.SetActive(false);

            // Add listeners to the buttons
            imageButton.onClick.AddListener(OpenPopout);
            exitButton.onClick.AddListener(ClosePopout);
        }

        private void OpenPopout()
        {
            if (canView)
            {
                popoutImage.SetActive(true);
            }
        }

        private void ClosePopout()
        {
            popoutImage.SetActive(false);
        }
    }
}