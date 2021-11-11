using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    [SerializeField]
    GameObject[] DialogueList;

    public void PlayDialogue_1()
    {
        StartCoroutine(Dialogue_1());
    }
    public void PlayDialogue_2()
    {
        StartCoroutine(Dialogue_2());
    }
    public void PlayDialogue_3()
    {
        StartCoroutine(Dialogue_3());
    }
    public void PlayDialogue_4()
    {
        StartCoroutine(Dialogue_4());
    }
    public void PlayDialogue_5()
    {
        StartCoroutine(Dialogue_5());
    }
    public void PlayDialogue_6()
    {
        StartCoroutine(Dialogue_6());
    }

    public void PlayDialogue_4_5()
    {
        StartCoroutine(Dialogue_4_5());
    }

    IEnumerator Dialogue_1() //¡°700m to the destination, put on your¡­¡±
    {
        yield return new WaitForSeconds(11f);
        DialogueList[0].SetActive(true);
        yield return new WaitForSeconds(4f);
        DialogueList[0].SetActive(false);
    }

    IEnumerator Dialogue_2() //¡°Sir, we attacked by [crow monster](give it a name), we can¡¯t hold it¡±
    {
        DialogueList[1].SetActive(true);
        yield return new WaitForSeconds(5f);
        DialogueList[1].SetActive(false);
    }
    IEnumerator Dialogue_3() //¡°Go, go, go everybody jump¡±
    {
        DialogueList[2].SetActive(true);
        yield return new WaitForSeconds(5f);
        DialogueList[2].SetActive(false);
    }
    IEnumerator Dialogue_4() //¡°Halbert is calling, please respond after hearing me ¡±
    {
        DialogueList[3].SetActive(true);
        yield return new WaitForSeconds(3f);
        DialogueList[3].SetActive(false);
    }
    IEnumerator Dialogue_5() //¡°Zzz, zzz¡±(lost signal SFX)
    {
        DialogueList[4].SetActive(true);
        AudioManager.instance.Play("LostSignal", "SFX");
        yield return new WaitForSeconds(3f);
        DialogueList[4].SetActive(false);
    }

    IEnumerator Dialogue_4_5()
    {
        StartCoroutine(Dialogue_4());
        yield return new WaitForSeconds(3f);
        StartCoroutine(Dialogue_5());
    }

    IEnumerator Dialogue_6() //¡°Gain, Jean, Noah, Liam, James, is anybody here?¡±
    {
        DialogueList[5].SetActive(true);
        yield return new WaitForSeconds(3f);
        DialogueList[5].SetActive(false);
    }
}
