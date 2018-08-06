using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(SpawnManager))]
[CanEditMultipleObjects]
public class SpawnManagerEditor : Editor
{
    int size;
    List<SpawnableObject[]> spawnOb = new List<SpawnableObject[]>();
    int[] amountOfEnemiesPerWave = new int[0];
    bool[] foldouts = new bool[0];

    SerializedProperty spawnPoints;

    private void OnEnable()
    {
        //Tiedoston luku
        if (File.Exists(Application.dataPath + @"\Resources\Waves.txt"))
        {
            string[] waveData = File.ReadAllLines(Application.dataPath + @"\Resources\Waves.txt");
            size = waveData.Length;
            spawnOb = new List<SpawnableObject[]>();
            amountOfEnemiesPerWave = new int[size];
            foldouts = new bool[size];

            for (int i = 0; i < size; i++)
            {
                string[] enemyAndFoldoutData = waveData[i].Split(',');
                foldouts[i] = System.Convert.ToBoolean(enemyAndFoldoutData[1]);

                if (enemyAndFoldoutData[0].Contains(":"))
                {
                    string[] enemyData = enemyAndFoldoutData[0].Split(':');
                    amountOfEnemiesPerWave[i] = enemyData.Length;
                    SpawnableObject[] tempSpawnableObjects = new SpawnableObject[amountOfEnemiesPerWave[i]];

                    for (int io = 0; io < amountOfEnemiesPerWave[i]; io++)
                    {
                        string[] enemyAndAmout = enemyData[io].Split(' ');
                        GameObject enemy = (GameObject)Resources.Load(enemyAndAmout[0], typeof(GameObject));
                        tempSpawnableObjects[io] = new SpawnableObject(enemy, int.Parse(enemyAndAmout[1]));
                    }
                    spawnOb.Add(tempSpawnableObjects);
                }
                else
                {
                    string[] enemyData = enemyAndFoldoutData[0].Split(' ');
                    amountOfEnemiesPerWave[i] = 1;
                    SpawnableObject[] tempSpawnableObjects = new SpawnableObject[1];
                    GameObject enemy = (GameObject)Resources.Load(enemyData[0], typeof(GameObject));
                    tempSpawnableObjects[0] = new SpawnableObject(enemy, int.Parse(enemyData[1]));
                    spawnOb.Add(tempSpawnableObjects);
                }
            }
        }
        else
        {
            size = 0;
        }

        //Spawni paikat
        spawnPoints = serializedObject.FindProperty("spawnPoints");
    }

    public override void OnInspectorGUI()
    {
        //Spawni paikat
        serializedObject.Update();
        EditorGUILayout.PropertyField(spawnPoints, true);
        serializedObject.ApplyModifiedProperties();
       
        //Aallot
        size = Mathf.Max(0, EditorGUILayout.IntField("Amount of waves ", size));

        int[] newAmountOfEnemiesPerWave = CheckAmountOfEnemies();
        List<SpawnableObject[]> newSpawnOb = CheckSpawnableObjects(newAmountOfEnemiesPerWave);
        bool[] newFoldouts = CheckFoldouts();

        for (int i = 0; i < size; i++)
        {
            newFoldouts[i] = EditorGUILayout.Foldout(newFoldouts[i], "Wave " + (i + 1));
            if (newFoldouts[i])
            {
                for (int ia = 0; ia < newAmountOfEnemiesPerWave[i]; ia++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Enemy");
                    newSpawnOb[i][ia].enemy = (GameObject)EditorGUILayout.ObjectField(newSpawnOb[i][ia].enemy, typeof(GameObject), false);
                    EditorGUILayout.EndHorizontal();
                    newSpawnOb[i][ia].amount = Mathf.Max(1, EditorGUILayout.IntField("Spawn amount ", newSpawnOb[i][ia].amount));
                }
                int amountOfenemies = Mathf.Max(1, EditorGUILayout.IntField("Amount of enemies ", newAmountOfEnemiesPerWave[i]));
                newAmountOfEnemiesPerWave[i] = amountOfenemies;
                newSpawnOb = CheckSpawnableObjects(newAmountOfEnemiesPerWave);
            }
        }

        //Tiedostoon kirjoittaminen
        string waveSaveData = "";
        for (int i = 0; i < size; i++)
        {
            for (int ia = 0; ia < newAmountOfEnemiesPerWave[i]; ia++)
            {
                try
                {
                    waveSaveData += newSpawnOb[i][ia].enemy.name + " ";
                    waveSaveData += (newSpawnOb[i][ia].amount).ToString();
                    if (ia + 1 != newAmountOfEnemiesPerWave[i])
                    {
                        waveSaveData += ":";
                    }
                }
                catch (System.Exception)
                {
                    Debug.Log("Muista vaihtaa spawnerit pois, niitä ei löydy resoursseista...");
                }
            }
            waveSaveData += "," + newFoldouts[i].ToString();
            if (i + 1 != size)
            {
                waveSaveData += "\n";
            }
        }

        string path = Application.dataPath + @"\Resources\Waves.txt";
        if (!File.Exists(path))
        {
            File.CreateText(path);
        }
        File.WriteAllText(path, waveSaveData);

        amountOfEnemiesPerWave = newAmountOfEnemiesPerWave;
        spawnOb = newSpawnOb;
        foldouts = newFoldouts;
    }



    int[] CheckAmountOfEnemies()
    {
        int[] newAmountOfEnemiesPerWave = new int[size];
        for (int i = 0; i < size; i++)
        {
            if (i < amountOfEnemiesPerWave.Length)
            {
                newAmountOfEnemiesPerWave[i] = amountOfEnemiesPerWave[i];
            }
            else
            {
                newAmountOfEnemiesPerWave[i] = 1;
            }
        }
        return newAmountOfEnemiesPerWave;
    }

    List<SpawnableObject[]> CheckSpawnableObjects(int[] newAmountOfEnemiesPerWave)
    {
        List<SpawnableObject[]> newSpawnOb = new List<SpawnableObject[]>();
        for (int i = 0; i < size; i++)
        {
            if (i < spawnOb.Count)
            {
                SpawnableObject[] spawnableObjects = new SpawnableObject[newAmountOfEnemiesPerWave[i]];
                for (int io = 0; io < newAmountOfEnemiesPerWave[i]; io++)
                {
                    if (io < spawnOb[i].Length)
                    {
                        spawnableObjects[io] = spawnOb[i][io];
                    }
                    else
                    {
                        spawnableObjects[io] = new SpawnableObject(((SpawnManager)(target)).gameObject, 0);
                    }
                }
                newSpawnOb.Add(spawnableObjects);
            }
            else
            {
                SpawnableObject[] spawnableObjects = new SpawnableObject[newAmountOfEnemiesPerWave[i]];
                for (int io = 0; io < newAmountOfEnemiesPerWave[i]; io++)
                {
                    spawnableObjects[io] = new SpawnableObject(((SpawnManager)(target)).gameObject, 0);
                }
                newSpawnOb.Add(spawnableObjects);
            }
        }
        return newSpawnOb;
    }

    bool[] CheckFoldouts()
    {
        bool[] newFoldouts = new bool[size];
        for (int i = 0; i < size; i++)
        {
            if (i < foldouts.Length)
            {
                newFoldouts[i] = foldouts[i];
            }
            else
            {
                newFoldouts[i] = true;
            }
        }
        return newFoldouts;
    }
}
#endif