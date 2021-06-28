using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;
    public TextMeshProUGUI promptText;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;
    bool keyPressed = false;

    public virtual void Interact()
    {
        //This method is meant to be overwritten
        Debug.Log("Interacting with" + transform.name);
    }
    private void Start()
    {
        promptText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                if (keyPressed == true)
                {
                    Interact();
                    hasInteracted = true;
                    promptText.gameObject.SetActive(false);
                }
                else
                {
                    promptText.gameObject.SetActive(true);
                }

            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    public void OnFocused(Transform playerTransform, bool isPressed)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
        keyPressed = isPressed;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
