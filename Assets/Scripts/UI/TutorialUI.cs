using UnityEngine;
using System.Collections;
using GameCore;


public class TutorialUI : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TutorialRoutine());
    }

    private IEnumerator TutorialRoutine()
    {
        UIManager.Instance.ShowTutorial("¡Mueve al guardián con WASD y ataca con click!");
        yield return new WaitForSeconds(7f);
        UIManager.Instance.ShowTutorial("");
        yield return new WaitUntil(() => 
            ResourceManager.Instance.Gold >= 50);
        UIManager.Instance.ShowTutorial("Coloca una torre sobre una zona marcada.");
        yield return new WaitForSeconds(6f);
        UIManager.Instance.ShowTutorial("");
    }
}