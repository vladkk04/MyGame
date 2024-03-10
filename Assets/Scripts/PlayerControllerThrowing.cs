using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerThrowing : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerInputHandler inputHandler;

    [SerializeField] Transform transformThrowing;
    [SerializeField] private GameObject throwingObject;
    [SerializeField] private GameObject isObjectOnPlayer;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;
    

    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Throw()
    {
        GameObject projectile = Instantiate(throwingObject, transformThrowing.position, throwingObject.transform.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 forceDirection = transform.forward;

        RaycastHit hit;

        Debug.DrawRay(transformThrowing.position, transformThrowing.forward * 30f, Color.red, 2f);

        if (Physics.Raycast(transformThrowing.position, transformThrowing.forward, out hit, 30f))
        {
            forceDirection = (hit.point - transformThrowing.position).normalized;
        }

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        rb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.ThrowingTriggered && isObjectOnPlayer.activeSelf)
        {
            Throw();
            isObjectOnPlayer.SetActive(false);
        } 
    }
}
