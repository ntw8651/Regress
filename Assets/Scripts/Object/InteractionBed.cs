using UnityEngine;

public class InteractionBed : MonoBehaviour, IInteraction
{
    public string Name { get; private set; } = "Bed";
    public Animation bedAnimation;

    public void Interact(GameObject player)
    {
        player.GetComponent<SelectCamera>().ShowBedCamera();
        bedAnimation = player.GetComponent<Animation>();
        bedAnimation.Play("PlayerBed");
    }
}