using UnityEngine;
using EmotionsLibrary.Player;

public class portal : MonoBehaviour {
    //! Variables
    //? Private
    [SerializeField] protected Transform destination;


    //! Methods
    //? Unity
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && other.TryGetComponent<Player>(out var player)) {
            player.Teleport(destination.position, destination.rotation); } }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction); } }
