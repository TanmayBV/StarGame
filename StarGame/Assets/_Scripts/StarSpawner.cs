using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarSpawner : MonoBehaviour
{
    PlayerInputs playerInputs;
    public GameObject Prefab;
    public TextMeshProUGUI TextPrefab;

    [Header("Gameplay")]
    [SerializeField]
    private int starsToSpwan = 10;

    [Header("Game Stuff")]
    public Transform[] SpawnPoints;
    public int[] Clue;
    public TextMeshProUGUI[] ClueText;
    public List<int> StarList = new List<int>();
    GameObject[] obj;


    private void Awake()
    {
        playerInputs = GetComponent<PlayerInputs>();
        obj = new GameObject[starsToSpwan];
        
    }

    private void Start()
    {
        Initilize();
    }

    public void Initilize()
    {
        SpawnStar();
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            ClueText[i] = SpawnPoints[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        for (int i = 0; i < starsToSpwan; i++)
        {
            AddNumber(StarList[i]);
        }
        for (int i = 0; i <= 15; i++)
        {
            int rand = RandomNumberGeberater(ClueText.Length);
            if (!StarList.Contains(rand) && Clue[rand] > 0)
                ClueText[rand].enabled = true;
            else
                i--;
        }
    }

    void SpawnStar()
    {
        int j =0;
        for (int i = 0; i < starsToSpwan; i++)
        {
            int randomNumber = RandomNumberGeberater(SpawnPoints.Length);
            if (!StarList.Contains(randomNumber))
            {
                StarList.Add(randomNumber);
                GameObject var = Instantiate(Prefab, SpawnPoints[randomNumber].position, Quaternion.identity);
                var.transform.SetParent(SpawnPoints[randomNumber]);
                var.SetActive(false);
                obj[StarList.Count-1] = var;
                j++;
            }
            else
            {
                i--;
            }

        }
    }

    public void RevealStars()
    {
        foreach (GameObject i in obj)
        {
            i.SetActive(true);
        }
    }

    void AddNumber(int p)
    {
        //up
        if (CheckInBounds(p - 7))
        {
            int pos = p - 7;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //UpperLeft
        if (CheckInBounds(p - 7 - 1) && !Checkleft(p))
        {
            int pos = p - 7 - 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //UpperRight
        if (CheckInBounds(p - 7 + 1) && !CheckRight(p))
        {
            int pos = p - 7 + 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //Down
        if (CheckInBounds(p + 7))
        {
            int pos = p + 7 ;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //DownLeft
        if (CheckInBounds(p + 7 - 1) && !Checkleft(p))
        {
            int pos = p + 7 - 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //DownRight
        if (CheckInBounds(p + 7 + 1) && !CheckRight(p))
        {
            int pos = p + 7 + 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //Lef
        if (CheckInBounds(p - 1) && !Checkleft(p))
        {
            int pos = p - 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }
        //Right
        if (CheckInBounds(p + 1) && !CheckRight(p))
        {
            int pos = p + 1;
            Clue[pos] += 1;
            SpawnPoints[pos].GetComponentInChildren<TextMeshProUGUI>().text = "" + Clue[pos];
        }

    }

    bool Checkleft(int n)
    {
        if (n % 7 == 0)
            return true;
        else
            return false;
    }
    bool CheckRight(int n)
    {
        if (n % 7 == 6)
            return true;
        else
            return false;
    }
    bool CheckInBounds(int n)
    {
        if (n >= 0 && n < SpawnPoints.Length)
        {
            return true;
        }
        else
            return false;
    }

    int RandomNumberGeberater(int num)
    {
        int r = Random.Range(0, num);
        return r;
    }
}
