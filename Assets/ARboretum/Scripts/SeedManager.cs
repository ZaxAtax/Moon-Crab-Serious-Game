using UnityEngine;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{
    public int seeds = 1; // Initial seed count
    public GameObject treePrefab; // Reference to the tree prefab to be planted
    public LayerMask dirtLayer; // Layer mask for dirt plots
    public Text seedText; // Reference to the seed text

    private void Update()
    {
        // Update the seed text with the current seed count
        seedText.text = "Seeds: " + seeds.ToString();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if the touch hit a dirt plot and has enough seeds to plant a tree
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, dirtLayer) && seeds > 0)
            {
                // Check if the dirt plot is empty
                if (hit.transform.childCount == 0)
                {
                    // Instantiate a tree prefab at the tapped dirt plot
                    Instantiate(treePrefab, hit.point, Quaternion.identity, hit.transform);
                    // Subtract -1 from the player's seed count
                    seeds--;
                }
            }
        }
    }

    public void CollectSeed()
    {
        // Add +1 to the player's seed count
        seeds++;
    }
}
