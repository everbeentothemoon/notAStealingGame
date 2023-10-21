using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    public float moveDistance = 5f; 
    public float moveSpeed = 2f; 
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector3 closedPosition;
    private bool isOpen = false;

    public Button openButton; 

    private void Start()
    {
        originalPosition = transform.position;
        closedPosition = originalPosition;

        float targetAngle = -35.955f;
        targetPosition = originalPosition + Quaternion.Euler(0f, 0f, targetAngle) * Vector3.down * moveDistance;

        openButton.onClick.AddListener(OpenDoorr);
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OpenDoorr()
    {
        isOpen = true;
    }
}
