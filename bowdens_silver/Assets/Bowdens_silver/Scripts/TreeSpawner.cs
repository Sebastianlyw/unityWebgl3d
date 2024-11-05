using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private List<AssetReference> treesReferences;
    public TreeSpawnerConfig preMining_config;
    public TreeSpawnerConfig mining_config;
    public GlobalConfig globalConfig;
    public Vector3 sceneCenter = new Vector3(-850, 0, -650);
 

    private List<GameObject> preMiningTrees = new List<GameObject>();
    private List<GameObject> miningTrees = new List<GameObject>();
    private List<GameObject> treePrefabs = new List<GameObject>();
    private GameObject miningTreeParent;
    private GameObject preMiningTreeParent;

    private void Start()
    {
        miningTreeParent = new GameObject("MiningTrees");
        preMiningTreeParent = new GameObject("PreMiningTrees");

        miningTreeParent.transform.parent = transform;
        preMiningTreeParent.transform.parent = transform;
    }

    private void OnTreePrefabsLoaded()
    {
        GameObject treeParent = new GameObject("TreesGroup");

        foreach (var treePrefab in treePrefabs)
        {
            treePrefab.transform.parent = treeParent.transform;
        }
        PopulateTreePool(preMiningTrees, treeParent, preMining_config, true);
        PopulateTreePool(miningTrees, treeParent, mining_config, false);

        ToggleTreeVisibility();
    }

    public void TogglePreMiningTrees(bool b)
    {
        preMiningTreeParent.SetActive(b);
    }

    public void ToggleMiningTrees(bool b)
    {
        miningTreeParent.SetActive(b);
    }

    public void ToggleTreeVisibility()
    {
        bool isPreMining = globalConfig.isPreMining;
        preMiningTreeParent.SetActive(isPreMining);
        miningTreeParent.SetActive(!isPreMining);
    }

    private void PopulateTreePool(List<GameObject> treePool, GameObject treeParent, TreeSpawnerConfig config, bool isPreMining)
    {
        for (int i = 0; i < config.density; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * config.radius;
            Vector3 spawnPosition = new Vector3(randomPos.x, 600, randomPos.y) + sceneCenter;

            spawnPosition.y = GetTerrainHeight(spawnPosition);

            GameObject newTree = Instantiate(treeParent, spawnPosition, Quaternion.identity);

            // Add the TreeCuller component to each tree model
            // Iterate through the children of newTree to add the TreeCuller component
            foreach (Transform child in newTree.transform)
            {
                TreeCuller treeCuller = child.gameObject.AddComponent<TreeCuller>();
                treeCuller.Initialize(Camera.main.transform, config.cullingDistance);
            }

            float randomYRotation = Random.Range(0, config.maxYRotation);
            newTree.transform.Rotate(0, randomYRotation, 0);

            float randomScale = Random.Range(config.minScale, config.maxScale);
            newTree.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            if (isPreMining)
            {
                newTree.transform.parent = preMiningTreeParent.transform;
            }
            else
            {
                newTree.transform.parent = miningTreeParent.transform;
            }

            treePool.Add(newTree);
        }
        Debug.Log("trePool.Count: " + treePool.Count);
    }

    public void SpawnTrees()
    {
        if (preMiningTrees.Count == 0 && miningTrees.Count == 0)
        {
            StartCoroutine(LoadTreePrefabs());
        }
    }

    private IEnumerator LoadTreePrefabs()
    {
        List<AsyncOperationHandle<GameObject>> loadOperations = new List<AsyncOperationHandle<GameObject>>();

        foreach (var reference in treesReferences)
        {
            var handle = reference.LoadAssetAsync<GameObject>();
            loadOperations.Add(handle);
        }

        foreach (var handle in loadOperations)
        {
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                treePrefabs.Add(handle.Result);
            }
            else
            {
                Debug.LogError("Failed to load tree prefab.");
            }
        }

        OnTreePrefabsLoaded();
    }

    float GetTerrainHeight(Vector3 position)
    {
        // Use terrain mesh to find the height at the X and Z position
        Ray ray = new Ray(new Vector3(position.x, 1000f, position.z), Vector3.down); // Cast downwards from above
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }

        return position.y; // Default if terrain height isn’t found
    }
}