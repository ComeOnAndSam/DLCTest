using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class LoadAdressables : MonoBehaviour
{
    public AssetReference scene;

    public void LoadScene()
    {
        Addressables.LoadSceneAsync(scene, LoadSceneMode.Single).Completed += SceneLoadComplete;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SceneLoadComplete(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        { // Set our reference to the AsyncOperationHandle (see next section)
            Debug.Log(obj.Result.Scene.name + " successfully loaded");
        }
    }
}
