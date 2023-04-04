using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveLoad : MonoBehaviour
{
    public void ButtonClick()
    {
        SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Additive);

    }
}
