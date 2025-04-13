using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject vision;
    public int delay;
    public int flicker;
    public int numFlickers; 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(flickering());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator flickering(){
        while(true){
            yield return StartCoroutine(OnAndOff(0.3f));
            yield return StartCoroutine(OnAndOff(0.7f));
            yield return StartCoroutine(OnAndOff(1.4f));
        }
    }

    public IEnumerator OnAndOff(float stepDelay){
        yield return new WaitForSeconds(delay * stepDelay);
        vision.SetActive(false);
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().Play();
        for (int i = 0; i < numFlickers; i++){
            yield return new WaitForSeconds(.1f);
            vision.SetActive(true);
            yield return new WaitForSeconds(.1f);
            vision.SetActive(false);
        }
        if (GetComponent<AudioSource>())
            GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(delay);
        vision.SetActive(true);
        
    }
}
