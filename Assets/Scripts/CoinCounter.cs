using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int levelCoins;
    public int coinCounter;
    public List<List<float>> coordsList;
    public GameObject coinPrefab;

    private void Awake()
    {
        Events.OnPickUpCoin += UpdateText;
        Events.OnWin += onWin;
        
        Events.OnResetCounter += ResetCounter;

        coordsList = new List<List<float>>();
    }

    private void OnDestroy()
    {
        Events.OnPickUpCoin -= UpdateText;
        Events.OnWin -= onWin;
        Events.OnResetCounter -= ResetCounter;
    }

    //When level is either restarted or you die
    public void ResetCounter(){
        
        for (int i = 0; i < coordsList.Count; i++)
        {
            Vector3 coinPosition = new Vector3(coordsList[i][0],coordsList[i][1],coordsList[i][2]);
            Instantiate(coinPrefab,coinPosition, Quaternion.Euler(0,0,coordsList[i][3]));
        }

        coordsList.Clear();

        coinCounter = 0;
        coinText.text = coinCounter.ToString();
    }

    //When you pick up a coin
    public void UpdateText(float[] coords){
        List<float> subList = new List<float>();
        subList.AddRange(coords);
        coordsList.Add(subList);
        Debug.Log("Before " + coinCounter);
        coinCounter = coinCounter + 1;
        Debug.Log("After " + coinCounter);
        coinText.text = coinCounter.ToString();
    }

    //When you win
    public void onWin(){
        
        coinText.text = coinCounter.ToString() + " / " + levelCoins.ToString();
        
        
    }
}
