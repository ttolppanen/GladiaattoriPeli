using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public List<SpawnableObject[]> waves = new List<SpawnableObject[]>();
    int currentWave;

    private void Awake()
    {
        currentWave = 0;
        if (File.Exists(Application.dataPath + @"\Waves.txt"))
        {
            print("asdasdasadsd");
            string[] waveData = File.ReadAllLines(Application.dataPath + @"\Waves.txt");
            int size = waveData.Length;

            for (int i = 0; i < size; i++)
            {
                string[] enemyAndFoldoutData = waveData[i].Split(',');
   
                if (enemyAndFoldoutData[0].Contains(":"))
                {
                    string[] enemyData = enemyAndFoldoutData[0].Split(':');
                    SpawnableObject[] tempSpawnableObjects = new SpawnableObject[enemyData.Length];

                    for (int io = 0; io < enemyData.Length; io++)
                    {
                        string[] enemyAndAmout = enemyData[io].Split(' ');
                        GameObject enemy = (GameObject)Resources.Load(enemyAndAmout[0], typeof(GameObject));
                        tempSpawnableObjects[io] = new SpawnableObject(enemy, int.Parse(enemyAndAmout[1]));
                    }
                    waves.Add(tempSpawnableObjects);
                }
                else
                {
                    string[] enemyData = enemyAndFoldoutData[0].Split(' ');
                    SpawnableObject[] tempSpawnableObjects = new SpawnableObject[1];
                    GameObject enemy = (GameObject)Resources.Load(enemyData[0], typeof(GameObject));
                    tempSpawnableObjects[0] = new SpawnableObject(enemy, int.Parse(enemyData[1]));
                    waves.Add(tempSpawnableObjects);
                }
            }
        }
        else
        {
            print("JOSSAIN VÄÄRIN APUA");
        }
    }

    void Update ()
    {
        if (transform.childCount <= 0)
        {
            foreach (SpawnableObject spawnOb in waves[currentWave])
            {
                print(spawnOb.amount);
                for (int i = spawnOb.amount; i > 0; i--)
                {
                    print("ad");
                    Instantiate(spawnOb.enemy, transform);
                }
            }
            currentWave += 1;
        }
	}
}