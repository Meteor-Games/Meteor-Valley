using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Netcode;

public class ServerController : MonoBehaviour
{
    public string sceneName;
    public string type;
    private Scene _parentScene;

    private void Start()
    {
        _parentScene = SceneManager.GetActiveScene();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode == LoadSceneMode.Additive && scene.name == sceneName)
        {
            SceneManager.UnloadSceneAsync(_parentScene);
        }

        GameObject networkManagerObject = GameObject.FindGameObjectWithTag("NetworkManager");
        if (networkManagerObject != null)
        {
            NetworkManager networkManager = networkManagerObject.GetComponent<NetworkManager>();
            if (networkManager != null)
            {
                switch (type)
                {
                    case "client":
                        networkManager.StartClient();
                        break;
                    case "host":
                        networkManager.StartHost();
                        break;
                }
            }
        }
    }

    public void ChangeSceneHost()
    {
        type = "host";
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneName, parameters);
    }

    public void ChangeSceneClient()
    {
        type = "client";
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneName, parameters);
    }
}
