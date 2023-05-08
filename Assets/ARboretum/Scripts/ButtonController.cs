using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    public Button arButton; // Reference to the AR button
    public Button arboretumButton; // Reference to the Arboretum button
    public Button quitButton; // Reference to the Quit button
    public ARTrackedImageManager trackedImageManager; // Reference to the ARTrackedImageManager
    public SeedManager seedManager; // Reference to the SeedManager script
    public GameObject plotPrefab; // Reference to the plot prefab
    public GameObject arboretumCanvas; // Reference to the Arboretum canvas
    public GameObject arCanvas; // Reference to the AR canvas
    public ARPlaneManager planeManager; // Reference to the ARPlaneManager
    public ARRaycastManager arRaycastManager;
    public GameObject plane;
    private bool isArboretumMode = false; // Flag to keep track of Arboretum mode
    public int moves;
    private GameObject plotInstance; // Reference to the plot instance

    // Start is called before the first frame update
    void Start()
    {
        // Add click listeners to the AR, Arboretum, and Quit buttons
        arButton.onClick.AddListener(OnARButtonClick);
        arboretumButton.onClick.AddListener(OnArboretumButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        plotInstance = Instantiate(plotPrefab);
        // Set the initial mode to AR mode
        SetARMode();
    }

void Update()
{
    if (isArboretumMode)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Vector3 hitPosition = hitPose.position;
                Quaternion hitRotation = hitPose.rotation;
                if (moves <= 0)
                {
                    plotInstance.SetActive(true);
                    plotInstance.transform.position = hitPosition;
                    plotInstance.transform.rotation = hitRotation;
                    moves++;
                    planeManager.enabled = false;
                    plane.SetActive(false);
                }
            }
        }
        if (moves == 1)
        {
          ARPlane[] planes = FindObjectsOfType<ARPlane>();
          foreach (ARPlane p in planes)
          {
            if (p.gameObject.activeSelf)
            {
              Destroy(p.gameObject);
            }
          }
        }
    }
}





    void OnARButtonClick()
    {
        // Set the mode to AR mode
        SetARMode();
    }

    void OnArboretumButtonClick()
    {
        // Set the mode to Arboretum mode
        SetArboretumMode();
    }

    void OnQuitButtonClick()
    {
        Application.Quit();
    }

    void SetARMode()
    {
        moves = 0;
        // Activate AR canvas and ARTrackedImageManager
        arCanvas.SetActive(true);
        trackedImageManager.enabled = true;
        arboretumCanvas.SetActive(false); // Deactivate Arboretum canvas
        if (plotInstance != null)
        {
            plotInstance.SetActive(false); // Deactivate plot instance
        }
        planeManager.enabled = false;
        isArboretumMode = false;
    }

    void SetArboretumMode()
    {
        arboretumCanvas.SetActive(true);
        trackedImageManager.enabled = false; // Disable ARTrackedImage
        arCanvas.SetActive(false);
        planeManager.enabled = true;
        plane.SetActive(true);
        isArboretumMode = true;
    }
}