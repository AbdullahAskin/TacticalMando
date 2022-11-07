using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Loading Materials")]
    public Image img_blackBg;
    public static float BLACK_SCREEN_ANIM_DURATION = 0.5f;

    public void LoadScene(int sceneIndex)
    {
        img_blackBg.raycastTarget = true;
        img_blackBg.DOFade(1f, BLACK_SCREEN_ANIM_DURATION).OnComplete(() =>
        {
            SceneManager.LoadSceneAsync(sceneIndex);
        });
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.BetaMode.tacticalmando");
    }

}
