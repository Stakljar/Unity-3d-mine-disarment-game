using TMPro;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    const int maxMines = 30;
    const float yOffset = 0.0441f;
    const float xStart = 450.07f;
    const float zStart = 458f;
    const float xEnd = 550.12f;
    const float zEnd = 532.61f;

    [SerializeField]
    GameObject mine;

    [SerializeField]
    TMP_Text minesDisarmedText;

    Terrain terrain;

    int minesDisarmed = 0;
    bool didAnyMineExplode = false;

    public bool DidAnyMineExplode
    {
        get => didAnyMineExplode;
        set => didAnyMineExplode = value;
    }

    void Start()
    {
        minesDisarmedText.text = "Mines disarmed: " + minesDisarmed + " / " + maxMines;
        terrain = Terrain.activeTerrain;

        for (int i = 0; i < maxMines; i++)
        {
            SpawnMine();
        }
    }

    void Update()
    {
       if(minesDisarmed == maxMines)
       {
            FindObjectOfType<Movement>().IsMovable = false;
            FindObjectOfType<Rotation>().enabled = false;
            FindObjectOfType<GameManager>().EndGame("You disarmed all mines");
       } 
    }

    public void UpdateNumberOfMines()
    {
        minesDisarmed++;
        minesDisarmedText.text = "Mines disarmed: " + minesDisarmed + " / " + maxMines;
    }

    void SpawnMine()
    {
        GameObject spawnedMine;
        Vector3 randomMinePosition = GenerateRandomPosition();
        Vector3 normal = terrain.terrainData.
            GetInterpolatedNormal(randomMinePosition.x / terrain.terrainData.size.x, randomMinePosition.z / terrain.terrainData.size.z);
        Quaternion mineRotation = Quaternion.FromToRotation(Vector3.up, normal);
        spawnedMine = Instantiate(mine, randomMinePosition, mineRotation);
        spawnedMine.gameObject.SetActive(true);
    }

    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(terrain.transform.position.x + xStart, terrain.transform.position.x + xEnd);
        float z = Random.Range(terrain.transform.position.z + zStart, terrain.transform.position.z + zEnd);

        Vector3 randomPosition = new Vector3(x, 0f, z);

        randomPosition.y = terrain.SampleHeight(randomPosition) + yOffset;

        return randomPosition;
    }
}
