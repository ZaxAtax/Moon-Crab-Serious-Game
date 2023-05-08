using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class TreeDetectionController : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public SeedManager seedManager; // Reference to SeedManager script

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Check if the tracked image has a specific name, e.g. "Tree"
            if (trackedImage.referenceImage.name == "oak tree")
            {
                // Call the CollectSeed() method on the SeedManager script
                seedManager.CollectSeed("oak");
            }
            else if (trackedImage.referenceImage.name == "pine tree")
            {
                seedManager.CollectSeed("pine");
            }
        }
    }
}
