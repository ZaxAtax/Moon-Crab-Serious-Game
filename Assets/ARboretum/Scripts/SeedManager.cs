using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeedManager : MonoBehaviour
{
    public int oakSeeds = 0; // Initial oak seed count
    public int pineSeeds = 0; // Initial pine seed count
    public GameObject oakTreePrefab; // Reference to the oak tree prefab to be planted
    public GameObject pineTreePrefab; // Reference to the pine tree prefab to be planted
    public LayerMask dirtLayer; // Layer mask for dirt plots
    public Button oakSeedButton; // Reference to the oak seed button text
    public Button pineSeedButton; // Reference to the pine seed button text
    public Text oakText;
    public Text pineText;
    public Text seedGainText; // Reference to the seed gain text
    public float seedGainDuration = 3f; // Duration for which seed gain text is visible

    private GameObject currentTreePrefab;

    private void Start()
    {
        // Hide the seed gain text
        seedGainText.gameObject.SetActive(false);
        oakSeedButton.gameObject.SetActive(false);
        pineSeedButton.gameObject.SetActive(false);
        oakSeedButton.onClick.AddListener(PlantOakTree);
        pineSeedButton.onClick.AddListener(PlantPineTree);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if the touch hit a dirt plot and has enough seeds to plant a tree
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if ((oakSeeds + pineSeeds) > 0 && Physics.Raycast(ray, out hit, Mathf.Infinity, dirtLayer))
            {
                // Check if the dirt plot is empty
                if (hit.transform.childCount == 0)
                {
                    // Show the seed buttons
                    oakText.text = "Oak Seeds: " + oakSeeds;
                    pineText.text = "Pine Seeds: " + pineSeeds;
                    oakSeedButton.gameObject.SetActive(true);
                    pineSeedButton.gameObject.SetActive(true);
                    // Store the current dirt plot
                    currentTreePrefab = hit.transform.gameObject;
                }
            }
        }
    }

public void PlantOakTree()
{
    // Check if the player has at least one oak seed and the current dirt plot is empty
    if (oakSeeds > 0 && currentTreePrefab != null && currentTreePrefab.transform.childCount == 0)
    {
        // Scale down the oak tree prefab
        Vector3 treeScale = new Vector3(0.03f, 0.05f, 0.03f);
        // Calculate the position of the oak tree prefab in the center of the top surface of the dirt plot
        Vector3 treePosition = currentTreePrefab.transform.position + new Vector3(0, 0, 1) * (currentTreePrefab.transform.localScale.y * 0.01f);
        // Instantiate an oak tree prefab at the current dirt plot with the scaled down size and centered position
        Instantiate(oakTreePrefab, treePosition, Quaternion.identity).transform.localScale = treeScale;
        // Subtract -1 from the player's oak seed count
        oakSeeds--;
    }
    //Hide buttons
    oakSeedButton.gameObject.SetActive(false);
    pineSeedButton.gameObject.SetActive(false);
}



    public void PlantPineTree()
    {
        // Check if the player has at least one pine seed and the current dirt plot is empty
        if (pineSeeds > 0 && currentTreePrefab != null && currentTreePrefab.transform.childCount == 0)
        {
            // Scaledown the pine tree prefab
            Vector3 treeScale = new Vector3(0.03f, 0.05f, 0.03f);
            // Calculate the position of the pine tree prefab in the center of the dirt plot
            Vector3 treePosition = currentTreePrefab.transform.position + new Vector3(0, 0, 1) * (currentTreePrefab.transform.localScale.y * 0.01f);
            // Instantiate a pine tree prefab at the current dirt plot with the scaled down size and centered position
            Instantiate(pineTreePrefab, treePosition, Quaternion.identity).transform.localScale = treeScale;
            // Subtract -1 from the player's pine seed count
            pineSeeds--;
            }
            // Hide the seed buttons
            oakSeedButton.gameObject.SetActive(false);
            pineSeedButton.gameObject.SetActive(false);
    }

    public void CollectSeed(string treeType)
    {
        // Update the seed gain text with the gained seed and the tree type
        seedGainText.text = "+" + 1 + " " + treeType + " Seed";
        // Show the seed gain text
        seedGainText.gameObject.SetActive(true);
        if (treeType == "oak")
        {
          oakSeeds++;
        }
        else if (treeType == "pine")
        {
          pineSeeds++;
        }
        // Start the coroutine to hide the seed gain text after seedGainDuration seconds
        StartCoroutine(HideSeedGainText());
    }

    private IEnumerator HideSeedGainText()
    {
        // Wait for seedGainDuration seconds
        yield return new WaitForSeconds(seedGainDuration);
        // Hide the seed gain text
        seedGainText.gameObject.SetActive(false);
    }
}
