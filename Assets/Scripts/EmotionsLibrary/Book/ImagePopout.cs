using UnityEngine;
using UnityEngine.UI;

public class ImagePopout : MonoBehaviour {
    //! Variables
    //? Private
    public bool canView = false;

    //* Assign in Inspector
    [Header("Emotion Card Elements")]
    [SerializeField] private GameObject popoutImage; 
    [SerializeField] private Button imageButton;     
    [SerializeField] private Button exitButton;      


    //! Methods
    //? Unity
    private void Start() {
        popoutImage.SetActive(false);

        // Add listeners to the buttons
        imageButton.onClick.AddListener(OpenPopout);
        exitButton.onClick.AddListener(ClosePopout); }

    //? Private
    private void OpenPopout() {
        if(canView) {
            popoutImage.SetActive(true); } }

    private void ClosePopout() {
        popoutImage.SetActive(false); } }