using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextDelay : MonoBehaviour
{
    public TextMeshProUGUI textHolder;
    public string[] sentences;
    int index;
    public float textSpeed;

    public GameObject continueButton;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        if(textHolder.text == sentences[index]){
            continueButton.SetActive(true);
        }
        else{
            continueButton.SetActive(false);
        }
    }

    public IEnumerator Delay(){
        foreach (char letter in sentences[index].ToCharArray()){
            textHolder.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        source.Stop();
    }

    public void NextSentence(){
        source.Play();
        if(index < sentences.Length - 1){
            index++;
            textHolder.text = "";
            StartCoroutine(Delay());
        }
        else{
            textHolder.text = "";
            continueButton.SetActive(false);
        }
    }
}
