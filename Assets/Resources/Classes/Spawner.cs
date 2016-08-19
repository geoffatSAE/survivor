using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    float[][] list = new float[15][];
    string[] enemyType = new string[5];

    float spawnTimer = 2;
    public GameObject enemy;

    private int wave;
    private bool lastWave;
    private bool waveFlow;
    private int enemiesRemaining;
    private int enemiesDestroyed;
    private int w;
    public RectTransform canvas;
    public GameObject wonGame;
    private int level = 1;

    void Start ()
    {
        w = 0;
        wave = 0;
        lastWave = false;
        waveFlow = false;
        enemiesRemaining = 0;
        enemiesDestroyed = 0;

        enemyType[0] = "Basic Enemy";
        enemyType[1] = "Boss";
        list[w++] = new float[]{0.5f, 15, 0, 5};
        list[w++] = new float[]{1f, 1, 1, 100};
        /*list[w++] = new float[]{0.5f, 3, 1, 10};
        list[w++] = new float[]{1, 3, 2, 10};
        list[w++] = new float[]{0.5f, 6, 1, 10};
        list[w++] = new float[]{0.5f, 8, 0, 2};
        list[w++] = new float[]{0.5f, 6, 1, 2};
        list[w++] = new float[]{1, 6, 2, 5};
        list[w++] = new float[]{0.5f, 12, 1, 10};
        list[w++] = new float[]{1, 6, 2, 10};
        list[w++] = new float[]{1, 1, 3, 15};
        list[w++] = new float[]{0.5f, 12, 0, 1};
        list[w++] = new float[]{0.5f, 12, 1, 100};
        list[w++] = new float[]{0, 0, 0, 100};*/
    }
	
	void Update ()
    {
        if (!lastWave && waveFlow && spawnTimer > 0 && enemiesRemaining <= 3)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnWaves();
            }
        }
	}

    private void SpawnWaves()
    {
        if (list[wave][1] > 0)
        {
            spawnTimer = list[wave][0];
            list[wave][1]--;

            enemy = (GameObject)Instantiate(Resources.Load("Prefabs/"+ enemyType[(int)list[wave][2]]), transform.position, transform.rotation);
            enemy.GetComponent<EnemyClass>().Difficulty((wave/5) + 1);
            enemiesRemaining++;
           // GameObject.Find("EnemiesRemaining").GetComponent<Text>().text = "Enemies Remaining \n" + enemiesRemaining.ToString();
        }
        else if(list[wave][3] < 100)
        {
            spawnTimer = list[wave][3];
            wave++;
            //GameObject.Find("WaveText").GetComponent<Text>().text = "Wave \n" + wave.ToString();
            //GameObject.Find("NextWave").GetComponent<Text>().text = "Next Wave \n" + enemyType[(int)list[wave][2]].ToString();
        }
        else{
            lastWave = true;
            Debug.Log("Last Wave");
        }
        //Debug.Log(wave);
    }

    public void SetWaveFlow(bool setFlow)
    {
        waveFlow = setFlow;
    }

    public void enemyDeathCount()
    {
        enemiesRemaining--;
        enemiesDestroyed++;
        //GameObject.Find("EnemiesRemaining").GetComponent<Text>().text = "Enemies Remaining \n" + enemiesRemaining.ToString();
        //GameObject.Find("EnemiesDestroyed").GetComponent<Text>().text = "Enemies Destroyed \n" + enemiesDestroyed.ToString();

        if(lastWave && enemiesRemaining == 0)
        {
            winGame();
            Time.timeScale = 0;
        }
    }

    public void removeEnemy()
    {
        enemiesRemaining--;
        //GameObject.Find("EnemiesRemaining").GetComponent<Text>().text = "Enemies Remaining \n" + enemiesRemaining.ToString();

        if (lastWave && enemiesRemaining == 0)
        {
            winGame();
            Time.timeScale = 0;
        }
    }

    public void winGame()
    {
        wonGame.SetActive(true);
    }

    public void ResetWaves()
    {
        level++;
        if(level == 2)
        {
            waveFlow = true;
            lastWave = false;
            wave = 0;
            w = 0;
            spawnTimer = 5;
            enemiesRemaining = 0;
            list[w++] = new float[] { 0.5f, 20, 0, 5 };
            list[w++] = new float[] { 1f, 2, 1, 100 };
        }else if(level == 3)
        {
            waveFlow = true;
            lastWave = false;
            wave = 0;
            w = 0;
            spawnTimer = 5;
            enemiesRemaining = 0;
            list[w++] = new float[] { 0.5f, 25, 0, 5 };
            list[w++] = new float[] { 1f, 3, 1, 100 };
        }
    }
}
