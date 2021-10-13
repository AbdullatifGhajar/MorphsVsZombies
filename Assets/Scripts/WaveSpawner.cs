
using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;
    public Wave[] waves;
    public AudioClip[] audioClips;
    public float timeBetweenWaves = 5f;
    private float countdown;

    public GameManager gameManager;

    private int waveIndex;

    void Start()
    {
        EnemiesAlive = 0;
        waveIndex = 0;
        countdown = 2f;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
            return;

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown = Mathf.Clamp(countdown - Time.deltaTime, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];

        AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().PlayOneShot(randomClip);

        EnemiesAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }

}
