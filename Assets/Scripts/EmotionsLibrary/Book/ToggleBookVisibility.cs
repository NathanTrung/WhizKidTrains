using UnityEngine;
using UnityEngine.UI;

public class ToggleBookVisibility : MonoBehaviour {
    //! Variables
    [SerializeField] private GameObject bookCanvas; // Reference to the book canvas
    [SerializeField] private Button showBookButton; // Reference to the show book button

    //! Methods
    //? Unity
    private void Start() { 
        if (bookCanvas != null) { bookCanvas.SetActive(false); } }

    //? Public
    public void ToggleBook() {
            bool isActive = bookCanvas.activeSelf;
            bookCanvas.SetActive(!isActive); } }
