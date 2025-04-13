using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

public class TextDelay : MonoBehaviour
{
    public TextMeshProUGUI textHolder;
    public string[] sentences;
    int index;
    public float textSpeed;

    public GameObject continueButton;
    public AudioClip ding;
    AudioSource typing;
    // Start is called before the first frame update
    void Start()
    {
       typing =  GetComponent<AudioSource>();
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
        typing.Stop();
        SoundFXManager.Instance.PlayAudioClip(ding, transform, 1, 1);
    }

    public void NextSentence(){
        typing.Play();
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
