using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CacaoScript : MonoBehaviour
{
    public GameObject[] cacaoCoins;
    [SerializeField] TextMeshProUGUI cacaoText;
    public int cacaoTotal = 0;


    private void Update()
    {
        cacaoText.text = ": " + cacaoTotal.ToString();
    }

    public void SpawnCacao(Vector3 position, int total)
    {
        int listlength = cacaoCoins.Length;
        int count = total / 50;
        if(count != 0)
        {
            InstantitateCacao(position,listlength-1,count);
            total -= 50 * count;
        }
        if(total != 0)
        {
            count = total / 10;
            if (count != 0)
            {
                InstantitateCacao(position, listlength - 2, count);
                total -= 10 * count;
            }
        }
        if (total != 0)
        {
            count = total / 5;
            if (count != 0)
            {
                InstantitateCacao(position, listlength - 3, count);
                total -= 5 * count;
            }
        }
        if (total != 0)
        {
            count = total / 1;
            if (count != 0)
            {
                InstantitateCacao(position, listlength - 4, count);
                total -= 1 * count;
            }
        }


    }

    private void InstantitateCacao(Vector3 pos, int listpos, int forLimit)
    {
        for (int i = 0; i < forLimit; i++)
        {
            float randomNumx = Random.Range(-2, 2);
            float randomNumy = Random.Range(0, 2);
            Vector3 randomDir = new Vector3(randomNumx, randomNumy, 0f);
            Transform transform = Instantiate(cacaoCoins[listpos].transform, pos, Quaternion.identity);
            transform.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        }
    }
}
