using UnityEngine;

public class LevelAdjuster : MonoBehaviour
{
    GameObject _levels, _willBeContinueScreen;


    float timeBetweenWaves = 10f, waveCoolDown = 2f, levelCoolDown = 3f, waveAfterCoolDown = 4f;
    int roundNumber = 0, waveNumber = 0;

    void Start()
    {
        _willBeContinueScreen = GameObject.Find("Canvas").transform.GetChild(9).gameObject;
        _levels = gameObject;
        Invoke(nameof(RoundStarter), waveAfterCoolDown);
    }


    public void RoundStarter()
    {
        FloatingTextController.CreateRoundText("Round - " + (roundNumber + 1)); // Bunlar index numaralari oldugu icin 
        Invoke(nameof(WaveStarter), levelCoolDown);
    }

    public void WaveStarter()
    {
        FloatingTextController.CreateRoundText("Wave - " + (waveNumber + 1));
        Invoke(nameof(LevelActivator), waveCoolDown);
    }

    public void LevelActivator()
    {
        _levels.transform.GetChild(roundNumber).GetChild(waveNumber).gameObject.SetActive(true);
        waveNumber++;
        if (waveNumber == 2)
        {
            InvokeRepeating(nameof(LevelDone), waveCoolDown + 2f, 1f);
        }
        else
        {
            Invoke(nameof(WaveStarter), timeBetweenWaves);
        }
    }


    public void LevelDone()
    {
        if (CommonData.enemiesPosition.Count == 0)
        {
            CancelInvoke(nameof(LevelDone));
            FloatingTextController.CreateRoundText("Round " + (roundNumber + 1) + " done");
            if (roundNumber < 4)// max 3 olucak.
                NextRound();
            else
            {
                _willBeContinueScreen.SetActive(true);
                Invoke(nameof(EndGame), 15f);
            }
        }
    }

    public void NextRound()
    {
        waveNumber = 0;
        roundNumber++;
        Invoke(nameof(RoundStarter), waveAfterCoolDown);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
