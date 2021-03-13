using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevelController : MonoBehaviour
{
    [SerializeField]
    private PlayView view;

    [SerializeField]
    private PlayerMovementController playerMovement;
    [SerializeField]
    private PlayerShootingController playerShooting;
    [SerializeField]
    private GameObject asteroidPrefab;

    public int timeToWin;
    public int timeBeforeReturn;
    // Start is called before the first frame update
    void Start()
    {

        
        playerMovement.CalculateSpeed(LevelModel.speedDeduction);
        playerShooting.SetFireRate(LevelModel.fireRate);
        InvokeRepeating("SpawnAsteroids", 0, LevelModel.spawnRate/1000f);
        Invoke("WinGame", timeToWin);

        view.UpdateView(LevelModel.health.Value);
        view.SetTimer(timeToWin);
    }

    private void SpawnAsteroids()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-9, 9), 0, 20);
        Instantiate(asteroidPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
    }

    // Update is called once per frame
    public void Hit()
    {
        LevelModel.health.Value--;
        view.UpdateView(LevelModel.health.Value);

        if (LevelModel.health.Value == 0)
        {
            view.ShowLoseText();
            FinishGame();
        }
    }

    private void WinGame()
    {
        view.ShowVictoryText();
        FinishGame();
        LevelModel.LevelPassed = true;
    }

    private void FinishGame()
    {
        CancelInvoke();
        playerMovement.Stop();
        playerShooting.Stop();
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject asteroid in asteroids)
        {
            Destroy(asteroid);
        }
        Invoke("ReturnToMap", timeBeforeReturn);
    }
    private void ReturnToMap()
    {
        SceneManager.LoadScene("Map");
    }

}
