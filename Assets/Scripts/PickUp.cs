using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject objectThrowing;
    [SerializeField] private GameObject text;
    private PlayerInputHandler inputHandler;

    void Start()
    {
        objectThrowing.SetActive(false);
        text.SetActive(false);
        inputHandler = PlayerInputHandler.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(true);
            if (inputHandler.PickUpTriggered)
            {
                this.gameObject.SetActive(false);
                text.SetActive(false);
                objectThrowing.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text.SetActive(false);
    }
}
