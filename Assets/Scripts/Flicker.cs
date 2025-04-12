using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject vision;
    public int delay;
    public int flicker;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnAndOff());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator OnAndOff(){
        while(true){
            yield return new WaitForSeconds(delay);
            vision.SetActive(false);

            yield return new WaitForSeconds(.1f);
            vision.SetActive(true);
            yield return new WaitForSeconds(.1f);
            vision.SetActive(false);
            yield return new WaitForSeconds(delay);
            vision.SetActive(true);
        }
        
    }
}
