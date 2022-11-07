using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelAdjuster : MonoBehaviour
{

    private float timeBetweenWaves = 10f, waveCoolDown = 2f, levelCoolDown = 3f, waveAfterCoolDown = 4f;
    private int iCurrentRound = 0, m_additionalMonster = 0;


    public LevelScriptableObject m_levelData;
    public Image img_blackBg;
    private WaveManager m_waveManager;

    void Start()
    {
        img_blackBg.DOFade(0f, MainMenu.BLACK_SCREEN_ANIM_DURATION);
        Invoke(nameof(StartRound), waveAfterCoolDown);
        m_waveManager = GetComponent<WaveManager>();
    }

    public void StartRound()
    {
        FloatingTextController.CreateRoundText("Round - " + (iCurrentRound + 1)); // Bunlar index numaralari oldugu icin 
        Invoke(nameof(StartLevel), levelCoolDown);
    }

    public void StartLevel()
    {
        m_waveManager.CreateWave(m_levelData.level.rounds[iCurrentRound], m_additionalMonster);
        if (iCurrentRound == m_levelData.level.rounds.Length)
            InvokeRepeating(nameof(IsLevelDone), waveCoolDown + 2f, 1f);
        else
            Invoke(nameof(StartLevel), timeBetweenWaves);
    }

    public void IsLevelDone()
    {
        if (CommonData.enemiesPosition.Count == 0)
        {
            CancelInvoke(nameof(IsLevelDone));
            FloatingTextController.CreateRoundText("Round " + (iCurrentRound + 1) + " done");
            if (iCurrentRound < m_levelData.level.rounds.Length)
                NextRound();
            else
            {
                //Invoke(nameof(EndGame), 15f);
                iCurrentRound = 0;
                m_additionalMonster++;
            }
        }
    }

    public void NextRound()
    {
        iCurrentRound++;
        Invoke(nameof(StartRound), waveAfterCoolDown);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
