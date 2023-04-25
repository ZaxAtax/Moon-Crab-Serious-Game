using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button arButton; // Reference to the AR button
    public Button arboretumButton; // Reference to the Arboretum button
    public ARTrackedImageManager trackedImageManager; // Reference to the ARTrackedImageManager
    public SeedManager seedManager; // Reference to the SeedManager script
    public GameObject plot; // Reference to the plot prefab
    public GameObject arboretumCanvas; // Reference to the Arboretum canvas
    public GameObject arCanvas; // Reference to the AR canvas


    // Start is called before the first frame update
    void Start()
    {
        // Add click listeners to the AR and Arboretum buttons
        arButton.onClick.AddListener(OnARButtonClick);
        arboretumButton.onClick.AddListener(OnArboretumButtonClick);
        // Set the initial mode to AR mode
        SetARMode();
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

    void SetARMode()
    {
        // Activate AR canvas and ARTrackedImageManager
        arCanvas.SetActive(true);
        trackedImageManager.enabled = true;
        arboretumCanvas.SetActive(false); // Deactivate Arboretum canvas
        plot.SetActive(false); // Activate plot prefab
    }

void SetArboretumMode()
{
    // Activate Arboretum canvas and SeedManager
    arboretumCanvas.SetActive(true);
    trackedImageManager.enabled = false; // Disable ARTrackedImageManager in Arboretum mode
    arCanvas.SetActive(false); // Deactivate AR canvas

    // Set the plot prefab's parent to the camera's transform
    plot.transform.SetParent(Camera.main.transform, true);
    plot.SetActive(true); // Activate plot prefab
}

void Update()
{
    // Update the position of the plot prefab based on the camera's forward direction
    if (plot.activeSelf)
    {
        plot.transform.localPosition = Camera.main.transform.forward * 2;
    }
}


}
