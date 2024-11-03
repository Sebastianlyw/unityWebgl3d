using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MenuSceneLoader : MonoBehaviour
{
    [SerializeField] private AssetReference menuReference;

    private GameObject loadedMenu;
    private void Start()
    {
        menuReference.LoadAssetAsync<GameObject>().Completed += OnCompleted;
    
    }
    private void Update()
    {
        if (menuReference != null) { 
        }
    }
    private void OnCompleted(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            loadedMenu = Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Failed to load menu.");
        }
    }

    private void OnDestroy()
    {
        if (loadedMenu != null)
        {
            Destroy(loadedMenu);
            menuReference.ReleaseAsset();
        }
    }

}
