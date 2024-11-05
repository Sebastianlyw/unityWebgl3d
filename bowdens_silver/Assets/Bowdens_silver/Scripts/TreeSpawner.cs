using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private AssetReference treePrefabReferences;
    public TreeSpawnerConfig preMining_config;
    public TreeSpawnerConfig mining_config;
    public GlobalConfig globalConfig;
    public Vector3 sceneCenter = new Vector3(-850,0,-650); 
    public Mesh terrainMesh;
    public Mesh terrainMeshOuter;
    private TreeSpawnerConfig config;

    private List<GameObject> preMiningTrees = new List<GameObject>();
    private List<GameObject> miningTrees = new List<GameObject>();

    //We should load the trees after terrain is loaded, as we need the terrin height to place the trees. 
    // private void Start()
    // {
    //     SpawnTrees();
    // }

    private void OnTreePrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if(globalConfig.isPreMining)
            {
                config = preMining_config;
            }
            else
            {
                config = mining_config;
            }
            PopulateTreePool(preMiningTrees, handle.Result, config);
            PopulateTreePool(miningTrees, handle.Result, config);
            ToggleTreeVisibility();
           
        }
        else
        {
            Debug.LogError("Failed to load menu.");
        }
    }

    public void TogglePreMiningTrees(bool b)
    {
        foreach (var tree in preMiningTrees)
        {
            tree.SetActive(b);
        }
    }

    public void ToggleMiningTrees(bool b)
    {
        foreach (var tree in miningTrees)
        {
            tree.SetActive(b);
        }
    }

    public void ToggleTreeVisibility()
    {
        bool isPreMining = globalConfig.isPreMining;

        foreach (var tree in preMiningTrees)
        {
            tree.SetActive(isPreMining);
        }

        foreach (var tree in miningTrees)
        {
            tree.SetActive(!isPreMining);
        }
    }


    private void PopulateTreePool(List<GameObject> treePool, GameObject treePrefab, TreeSpawnerConfig config)
    {
        for (int i = 0; i < config.density; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * config.radius;
            Vector3 spawnPosition = new Vector3(randomPos.x, 600, randomPos.y) + sceneCenter;

            spawnPosition.y = GetTerrainHeight(spawnPosition);

            GameObject newTree = Instantiate(treePrefab, spawnPosition, Quaternion.identity);
            for (int j = 0; j < newTree.transform.childCount; j++)
            {
                GameObject treeModel = newTree.transform.GetChild(j).gameObject;

                // Add the TreeCuller component to each tree model
                TreeCuller treeCuller = treeModel.AddComponent<TreeCuller>();
                treeCuller.Initialize(Camera.main.transform, config.cullingDistance);
            }

            newTree.AddComponent<TreeCuller>().Initialize(Camera.main.transform, config.cullingDistance);
            float randomYRotation = Random.Range(0, config.maxYRotation);
            newTree.transform.Rotate(0, randomYRotation, 0);

            float randomScale = Random.Range(config.minScale, config.maxScale);
            newTree.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Make newTree a child of this spawner for easy cleanup
            newTree.transform.parent = transform;
            newTree.SetActive(false); // Initially set to inactive
            treePool.Add(newTree);
        }
    }
    

    public void SpawnTrees()
    {
        if(preMiningTrees.Count == 0 && miningTrees.Count == 0)
        {
          treePrefabReferences.LoadAssetAsync<GameObject>().Completed += OnTreePrefabLoaded;
        }
    }

    float GetTerrainHeight(Vector3 position)
    {
        // Use terrain mesh to find the height at the X and Z position
        Ray ray = new Ray(new Vector3(position.x, 1000f, position.z), Vector3.down); // Cast downwards from above
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
           // Debug.Log("hit the terrian y:" + hit.point.y);
            return hit.point.y;
            
        }
        return position.y; // Default if terrain height isn�t found
    }

}
