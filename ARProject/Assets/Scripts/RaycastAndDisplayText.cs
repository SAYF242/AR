using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;

public class RaycastAndDisplayText : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    public TextMeshPro currentText;
    private int RandomNumber;
    public  bool guessNumber;
    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        guessNumber = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !guessNumber)
        {
            TryDisplayTextOnPlane();
        }
    }

    private void TryDisplayTextOnPlane()
    {
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        
        if (arRaycastManager.Raycast(Input.touches[0].position, hits, TrackableType.PlaneWithinPolygon))
        {
            ARRaycastHit hit = hits[0];
            Vector3 hitPosition = hit.pose.position;

            currentText.gameObject.SetActive(true);
            currentText.text = GenerateRandomNumber().ToString();
           
            Vector3 cameraDirection = Camera.main.transform.forward;
            currentText.transform.position = hitPosition;
            currentText.transform.rotation = Quaternion.LookRotation(cameraDirection);
            guessNumber = true;
            // Update displayed Options
            FindObjectOfType<UIController>().DisplayOption(RandomNumber);

        }
    }


    private int GenerateRandomNumber()
    {
        RandomNumber = Random.Range(1, 11);
        return RandomNumber;
    }
    public void ResetGuessNumber()
    {
        guessNumber = false;
        currentText.gameObject.SetActive(false);
    }
}
