using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    [SerializeField] string[] instructions;
    [SerializeField] Text instructionText;

    InfiniteBlock infiniteBlock;

    Animator animator;

    private void Start()
    {
        infiniteBlock = FindObjectOfType<InfiniteBlock>();
        animator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        animator.SetTrigger("FadeIn");
    }

    public void CloseText()
    {
        animator.SetTrigger("FadeOut");
    }


    private void Update()
    {
        if(infiniteBlock.totalBlockCount == 0)
        {
            instructionText.text = instructions[0];
        }
        else if(infiniteBlock.totalBlockCount == 1)
        {
            instructionText.text = instructions[1];
        }
        else if(infiniteBlock.totalBlockCount == 2)
        {
            instructionText.text = instructions[2];
        }
        else if(infiniteBlock.totalBlockCount == 3)
        {
            CloseText();
        }
    }

}
