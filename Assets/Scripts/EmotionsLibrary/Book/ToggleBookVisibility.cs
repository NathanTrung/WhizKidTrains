using UnityEngine;
using UnityEngine.UI;

namespace WhizKid.EmotionsLibrary.Book
{
    public class ToggleBookVisibility : MonoBehaviour
    {
        [SerializeField] private GameObject bookCanvas; // Reference to the book canvas
        [SerializeField] private Button showBookButton; // Reference to the show book button

        private void Start()
        {
            if (bookCanvas != null) { bookCanvas.SetActive(false); }
        }

        public void ToggleBook()
        {
            bool isActive = bookCanvas.activeSelf;
            bookCanvas.SetActive(!isActive);
        }
    }
}
