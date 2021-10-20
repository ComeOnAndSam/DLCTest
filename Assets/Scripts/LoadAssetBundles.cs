using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadAssetBundles : MonoBehaviour
{
    public void LoadAssets()
    {
        Debug.Log(Application.streamingAssetsPath);
        var bundleTest
            = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "bundletest"));
        var sceneBundle
            = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "scenebundle"));
        if (bundleTest == null)
        {
            Debug.Log("Failed to load bundletest!");
            return;
        }
        if (sceneBundle == null)
        {
            Debug.Log("Failed to load scenebundle!");
            return;
        }
    }
}
