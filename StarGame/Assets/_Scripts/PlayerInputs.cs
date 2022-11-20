using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInputs: MonoBehaviour
{
    StarSpawner starSpawner;

    public GameObject curser;
    public int Chance = 10;
    public int Score;

    List<int> SelectedPoints = new List<int>(10);

     public Transform pointer;
    public int NavigationPosition = 0;

    bool isPlaying = true;

    Color selectedcolor;
    Color Unselectedcolor;
    private void Awake()
    {
        starSpawner = GetComponent<StarSpawner>();
        

        //Selected Objects color
        selectedcolor = Color.green;
        selectedcolor.a = .5f;

        //Unselected Objects color
        Unselectedcolor = Color.white;
        Unselectedcolor.a = 1f;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reset();
        if (Input.GetKeyDown(KeyCode.Escape))
            Exit();

        if (!isPlaying) return;

        Movement();
        Select();
        DeSelect();
    }
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            if ( CheckBounds(NavigationPosition - 7))
            {
                NavigationPosition -= 7;
                pointer = starSpawner.SpawnPoints[NavigationPosition];
                curser.transform.position = pointer.transform.position;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (CheckBounds(NavigationPosition + 7))
            {
                NavigationPosition +=7;
                pointer = starSpawner.SpawnPoints[NavigationPosition];
                curser.transform.position = pointer.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CheckBounds(NavigationPosition - 1))
            {
                NavigationPosition -= 1;
                pointer = starSpawner.SpawnPoints[NavigationPosition];
                curser.transform.position = pointer.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CheckBounds(NavigationPosition + 1))
            {
                NavigationPosition += 1;
                pointer = starSpawner.SpawnPoints[NavigationPosition];
                curser.transform.position = pointer.transform.position;
            }
        }
    }

    void Select()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (0 < Chance)
            {
                SelectedPoints.Add(NavigationPosition);
                starSpawner.SpawnPoints[NavigationPosition].gameObject.GetComponent<Image>().color = selectedcolor;                Chance--;
            }
            else
            {
                isPlaying = false;
                curser.SetActive(false);
                CheckScore();
                starSpawner.RevealStars();
                
            }
        }
    }
    
    void DeSelect()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            if(SelectedPoints.Contains(NavigationPosition))
            {
                Chance++;
                SelectedPoints.Remove(NavigationPosition);
                starSpawner.SpawnPoints[NavigationPosition].gameObject.GetComponent<Image>().color = Unselectedcolor;
            }
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        Application.Quit();
    }

    void CheckScore()
    {
        for(int i=0; i<SelectedPoints.Count;i++)
            if (starSpawner.StarList.Contains(SelectedPoints[i]))
            {
                Score++;
            }
        Chance = -1;
    }

    private bool CheckBounds(int i)
    {
        if (i >= 0 && i < 49)
            return true;
        else
            return false;
    }
}
