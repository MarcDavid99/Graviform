using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int levelCoins;
    public int coinCounter;
    public int atCheckPointCoins;
    public List<List<float>> coordsList;
    public List<List<float>> coordsListForGameRestart;
    public List<List<float>> test;
    public GameObject coinPrefab;
    public GameObject[] coins;

    private void Awake()
    {
        Events.OnPickUpCoin += UpdateText;
        Events.OnWin += onWin;
        Events.OnCheckpointReached += OnTouchCheckpoint;
        Events.OnResetCounter += ResetCounter;
        Events.OnTotalRestart += OnTotalRestart;

        coordsList = new List<List<float>>();
        coordsListForGameRestart = new List<List<float>>();
        
        Debug.Log("Awake");
        coins = GameObject.FindGameObjectsWithTag("Coin");

      

        for (int i = 0; i < coins.Length; i++)
        {
            List<float> subList = new List<float>();
            subList.Add(coins[i].transform.position.x);
            subList.Add(coins[i].transform.position.y);
            subList.Add(coins[i].transform.position.z);
            subList.Add(coins[i].transform.eulerAngles.z);
            coordsListForGameRestart.Add(subList);
        }

        atCheckPointCoins = 0;
    }

    private void OnDestroy()
    {
        Events.OnPickUpCoin -= UpdateText;
        Events.OnWin -= onWin;
        Events.OnResetCounter -= ResetCounter;
        Events.OnCheckpointReached -= OnTouchCheckpoint;
        Events.OnTotalRestart -= OnTotalRestart;
    }

    public void OnTouchCheckpoint(){
        atCheckPointCoins = coinCounter;

        coordsList.Clear();
        
        
    }

    //When you die, only spawns those coins that aren't picked up until current checkpoint
    public void ResetCounter(){
        
        for (int i = 0; i < coordsList.Count; i++)
        {
            Vector3 coinPosition = new Vector3(coordsList[i][0],coordsList[i][1],coordsList[i][2]);
            Instantiate(coinPrefab,coinPosition, Quaternion.Euler(0,0,coordsList[i][3]));
        }

        coordsList.Clear();

        coinCounter = atCheckPointCoins;
        coinText.text = coinCounter.ToString();
    }

    //When you pick up a coin
    public void UpdateText(float[] coords){
        List<float> subList = new List<float>();
        subList.AddRange(coords);
        coordsList.Add(subList);
        
        coinCounter = coinCounter + 1;
        
        coinText.text = coinCounter.ToString();
    }

    public void OnTotalRestart(){
        try
        {
            GameObject[] coinsToDelete = GameObject.FindGameObjectsWithTag("Coin");
            Debug.Log(coinsToDelete.Length);
            for (int i = 0; i < coinsToDelete.Length; i++)
            {
                Destroy(coinsToDelete[i]);
            }
        }
        catch (System.Exception)
        {
            
            Debug.Log("Exception");
           
        }
        
            Debug.Log(coordsListForGameRestart.Count);


        for (int i = 0; i < coordsListForGameRestart.Count; i++)
        {        
            Vector3 coinPosition = new Vector3(coordsListForGameRestart[i][0],coordsListForGameRestart[i][1],coordsListForGameRestart[i][2]);
            Instantiate(coinPrefab,coinPosition, Quaternion.Euler(0,0,coordsListForGameRestart[i][3]));
        }

        coordsList.Clear();
        
        coinCounter = 0;
        atCheckPointCoins = 0;
        coinText.text = coinCounter.ToString();
    }

    //When you win
    public void onWin(){
        
        coinText.text = coinCounter.ToString() + " / " + levelCoins.ToString();
        
        
    }
}
