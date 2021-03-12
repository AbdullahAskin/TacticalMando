using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject _loadingScreen;
    public Slider _progressSlider;
    public TextMeshProUGUI _progressText;

    private void Start()
    {
        _loadingScreen = transform.GetChild(4).gameObject;
        _progressText = _loadingScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _progressSlider = _loadingScreen.transform.GetChild(2).GetComponent<Slider>();
    }

    public void PlayGame(int sceneIndex)
    {
        StartCoroutine("LoadAsynchronously", sceneIndex);
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.BetaMode.tacticalmando");
    }


    // Oy verme ve hakkimizda kismini da ekle.
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progress *= progress;
            _progressSlider.value = progress;
            int progressTemp = (int)Mathf.Round(progress);
            _progressText.text = progressTemp.ToString() + " %";
            yield return null;
        }
    }

}
