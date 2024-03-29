using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ScriptManagement : MonoBehaviour
{
    public static BuahInfo buahInfo;
    public GameObject[] prefebs;
    public GameObject AnkorSpawn;
    public GameObject player;
    private SpawnFruit spawnScript;
    private Player playerScript;
    private bool stayFruit = false;

    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        private set { score = value; }
    }


    private int index;

    private GameObject fruitNext;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Start()
    {
        cleanStaticVar();
        spawnScript = AnkorSpawn.gameObject.GetComponent<SpawnFruit>();
        spawnBuah();
        scoreText.text = "0";
        score = 0;
        Debug.Log(scoreText.text);
    }

    private void Update()
    {
        refraseScoreDisplay();
        checkFruitInPlayer();
        gabunginBuah();
    }

    private void gabunginBuah()
    {
        if (buahInfo != null && buahInfo!.ubahBuah)
        {
            buahInfo.ubahBuah = false;
            int index = nextBuahIndex();
            if (index > 0)
            {
                Instantiate(prefebs[index], buahInfo.lokasi, Quaternion.identity);
            }
        }
    }

    private int nextBuahIndex()
    {
        switch (buahInfo.namaBuah)
        {
            case JenisBuah.cerry:
                score += 5;
                return 1;
            case JenisBuah.stawberry:
                score += 10;
                return 2;
            case JenisBuah.anggur:
                score += 15;
                return 3;
            case JenisBuah.jeruk:
                score += 20;
                return 4;
            case JenisBuah.apel:
                score += 25;
                return 5;
            case JenisBuah.kelapa:
                score += 30;
                return 6;
            case JenisBuah.nanas:
                score += 35;
                return 7;
            case JenisBuah.melon:
                score += 40;
                return 8;
            case JenisBuah.semangka:
                score += 50;
                return 0;
            default:
                return 0;
        }
    }

    public void refraseScoreDisplay()
    {
        scoreText.text = score.ToString();
    }

    private void spawnBuah()
    {
        index = Random.Range(0, 4);
        fruitNext = spawnScript.setSpawn(prefebs[index], null);
    }

    private void checkFruitInPlayer()
    {
        if (!playerScript.isFruitExist)
        {
            playerScript.isFruitExist = true;
            fruitNextDestroy();
            GameObject buahInPlayer = spawnScript.setSpawn(prefebs[index], player.transform);
            buahInPlayer.gameObject.GetComponent<Buah>().addSubcribe();
            buahInPlayer.gameObject.GetComponent<Buah>().followPlayer = true;
            spawnBuah();
        }
    }

    private void fruitNextDestroy()
    {
        Destroy(fruitNext);
    }

    private void cleanStaticVar()
    {
        buahInfo = null;
    }
}


