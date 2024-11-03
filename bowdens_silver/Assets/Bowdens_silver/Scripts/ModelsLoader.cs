using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class ModelsLoader : MonoBehaviour
{
    // Define Addressable references for pre-mining models
    [SerializeField] private List<AssetReference> preMiningModelReferences;
    [SerializeField] private List<AssetReference> miningModelReferences;

    public Button toggleButton;
  
    private List<GameObject> preMiningModels = new List<GameObject>();
    private List<GameObject> miningModels = new List<GameObject>();
    public float scaleDuration = 1.0f;

    private bool isPreMiningActive = true;
    private TextMeshProUGUI buttonText;

    private void Start()
    {
        LoadModels(preMiningModelReferences, preMiningModels, true);
        LoadModels(miningModelReferences, miningModels, false);   
     
        buttonText = toggleButton.GetComponentInChildren<TextMeshProUGUI>(); 
        buttonText.text = "Mining";
        Debug.Log("buttonText: " + buttonText);
        toggleButton.onClick.AddListener(ToggleModels);
    }

    public void ToggleModels()
    {
        if (isPreMiningActive)
        {
            ShowModels(miningModels, preMiningModels, this.scaleDuration);
            buttonText.text = "PreMining";
        }
        else
        {
            ShowModels(preMiningModels, miningModels, this.scaleDuration);
           buttonText.text = "Mining";
        }

        isPreMiningActive = !isPreMiningActive;
    }


    private IEnumerator ScaleModel(GameObject model, float duration)
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one; 
        float elapsedTime = 0f;

        model.transform.localScale = initialScale; 

        while (elapsedTime < duration)
        {
            model.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        model.transform.localScale = targetScale; 
    }


    private void LoadModels(List<AssetReference> references, List<GameObject> modelPool, bool show)
    {
        foreach (var reference in references)
        {
            reference.LoadAssetAsync<GameObject>().Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var model = Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                    Debug.Log("Model loaded: " + model);
                    if(show)
                    {
                        model.SetActive(true);
                    }
                    else
                    {
                        model.SetActive(false);
                    }
                    modelPool.Add(model);
                }
                else
                {
                    Debug.LogError("Failed to load model.");
                }
            };
        }
    }

    private void ShowModels(List<GameObject> modelsToShow, List<GameObject> modelsToHide, float scaleDuration = 1f)
    {
        foreach (var model in modelsToHide)
        {
            if (model != null)
                model.SetActive(false);
        }
        
        foreach (var model in modelsToShow)
        {
            if (model != null)
            {
                model.SetActive(true);
                StartCoroutine(ScaleModel(model, scaleDuration));
            }
        }
    }

    private void OnDestroy()
    {
        // Release addressables to clean up memory when the script is destroyed
        ReleaseModels(preMiningModelReferences, preMiningModels);
        ReleaseModels(miningModelReferences, miningModels);
    }

    private void ReleaseModels(List<AssetReference> references, List<GameObject> modelPool)
    {
        foreach (var model in modelPool)
        {
            if (model != null)
                Destroy(model);
        }

        foreach (var reference in references)
        {
            reference.ReleaseAsset();
        }
    }
}
