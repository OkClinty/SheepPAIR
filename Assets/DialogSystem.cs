using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogChunk
{
    [TextArea]
    public string text;
    public string speakerName;
    public Sprite portrait;
    public bool showChoices;
    public List<string> choices;
    public List<int> choiceMaps;
    public List<char> stat;
    public int s1;
    public int l1;
    public double m1;
    public int k1;
    public int s2;
    public int l2;
    public double m2;
    public int k2;
}

public class DialogSystem : MonoBehaviour
{
    [Header("UI Elements")]
    public Image dialogBox;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI speakerNameText;
    public Image portraitImage;
    public Image portraitFrame;
    public Image dark;
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;
    public Image background;
    public Sprite swap;

    [Header("Typing Settings")]
    public float typingSpeed = 0.03f;

    [Header("Dialog Data")]
    public List<DialogChunk> dialogChunks;
    public List<int> paths;

    private int currentChunkIndex = 0;
    private bool isTyping = false;
    private bool skipTyping = false;

    void Start()
    {
        dialogBox.canvasRenderer.SetAlpha(0);
        portraitFrame.canvasRenderer.SetAlpha(0);
        switch (MainSystem.scenePos + (MainSystem.day - 3) * 4)
        {
            case 0:
                ShowDialogBox(0);
                break;
            case 1:
                ShowDialogBox(0);
                break;
            case 2:
                ShowDialogBox(0);
                break;
            case 3:
                ShowDialogBox(0);
                break;
            case 4:
                ShowDialogBox(3);
                break;
            case 5:
                ShowDialogBox(5);
                break;
            case 6:
                ShowDialogBox(8);
                break;
            case 7:
                ShowDialogBox(20);
                break;
            case 8:
                ShowDialogBox(8);
                break;
            case 9:
                ShowDialogBox(6);
                break;
            case 10:
                ShowDialogBox(11);
                break;
            case 11:
                ShowDialogBox(33);
                break;
            case 12:
                ShowDialogBox(0);
                break;
            case 13:
                ShowDialogBox(8);
                break;
            case 14:
                ShowDialogBox(14);
                break;
            case 15:
                ShowDialogBox(46);
                break;
            case 16:
                ShowDialogBox(0);
                break;
            case 17:
                ShowDialogBox(9);
                break;
            case 18:
                ShowDialogBox(17);
                break;
            case 19:
                ShowDialogBox(59);
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                skipTyping = true;
            }
            else if (!dialogChunks[currentChunkIndex].showChoices)
            {
                AdvanceDialog();
            }
        }
    }

    void AdvanceDialog()
    {
        currentChunkIndex = paths[currentChunkIndex];
        if (currentChunkIndex != -1)
        {
            ShowChunk(dialogChunks[currentChunkIndex]);
        }
        else
        {
            ShowDialogBox(-1);
        }
    }

    void ShowChunk(DialogChunk chunk)
    {
        speakerNameText.text = chunk.speakerName;
        portraitImage.sprite = chunk.portrait;
        portraitImage.enabled = chunk.portrait != null;

        if (chunk.showChoices)
        {
            DisplayChoices(chunk.choices);
        }
        else
        {
            choice1.SetActive(false);
            choice2.SetActive(false);
            choice3.SetActive(false);
            StartCoroutine(TypeText(chunk.text));
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        skipTyping = false;
        dialogText.text = "";

        foreach (char c in text)
        {
            if (skipTyping)
            {
                dialogText.text = text;
                break;
            }

            dialogText.text += c;

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void DisplayChoices(List<string> choices)
    {
        choice1.SetActive(true);
        choice1.GetComponentInChildren<TMP_Text>().text = choices[0];

        if (choices.Count > 1)
        {
            choice2.SetActive(true);
            choice2.GetComponentInChildren<TMP_Text>().text = choices[1];
        }

        if (choices.Count > 2)
        {
            choice3.SetActive(true);
            choice3.GetComponentInChildren<TMP_Text>().text = choices[2];
        }
    }

    public void ShowDialogBox(int start)
    {
        if (start != -1)
        {
            dialogBox.CrossFadeAlpha(1f, 0.5f, false);
            portraitFrame.CrossFadeAlpha(1f, 0.5f, false);
            portraitImage.CrossFadeAlpha(1f, 0.5f, false);
            currentChunkIndex = start;
            ShowChunk(dialogChunks[currentChunkIndex]);
        }
        else
        {
            dialogBox.CrossFadeAlpha(0f, 0.5f, false);
            portraitFrame.CrossFadeAlpha(0f, 0.5f, false);
            portraitImage.CrossFadeAlpha(0f, 0.5f, false);
            dark.CrossFadeAlpha(0f, 0.5f, false);
            dialogText.text = "";
            speakerNameText.text = "";

            Vector3 spawnPoint;
            if (MainSystem.scenePos == 3 || MainSystem.scenePos == 0)
            {
                spawnPoint = new Vector3(0, -3, 0);
            } else
            {
                spawnPoint = new Vector3(0, 0, 0);
            }
            CharacterController.Spawn(spawnPoint);

            background.sprite = swap;
        }
    }

    public void Choose1()
    {
        if (MainSystem.scenePos == 2)
        {
            if (dialogChunks[currentChunkIndex].stat[0] == 'r')
            {
                MainSystem.rizz += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[0] == 'a')
            {
                MainSystem.aura += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[0] == 'h')
            {
                MainSystem.hax += 1;
            }
        }
        else if (MainSystem.scenePos == 3)
        {
            MainSystem.NoonChoice(dialogChunks[currentChunkIndex].s1, dialogChunks[currentChunkIndex].l1, dialogChunks[currentChunkIndex].m1, dialogChunks[currentChunkIndex].k1);
        }
        currentChunkIndex = dialogChunks[currentChunkIndex].choiceMaps[0];
        ShowChunk(dialogChunks[currentChunkIndex]);
    }

    public void Choose2()
    {
        if (MainSystem.scenePos == 2)
        {
            if (dialogChunks[currentChunkIndex].stat[1] == 'r')
            {
                MainSystem.rizz += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[1] == 'a')
            {
                MainSystem.aura += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[1] == 'h')
            {
                MainSystem.hax += 1;
            }
        } 
        else if (MainSystem.scenePos == 3)
        {
            MainSystem.NoonChoice(dialogChunks[currentChunkIndex].s2, dialogChunks[currentChunkIndex].l2, dialogChunks[currentChunkIndex].m2, dialogChunks[currentChunkIndex].k2);
        }
        currentChunkIndex = dialogChunks[currentChunkIndex].choiceMaps[1];
        ShowChunk(dialogChunks[currentChunkIndex]);
    }

    public void Choose3()
    {
        if (MainSystem.scenePos == 2)
        {
            if (dialogChunks[currentChunkIndex].stat[2] == 'r')
            {
                MainSystem.rizz += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[2] == 'a')
            {
                MainSystem.aura += 1;
            }
            else if (dialogChunks[currentChunkIndex].stat[2] == 'h')
            {
                MainSystem.hax += 1;
            }
        }
        currentChunkIndex = dialogChunks[currentChunkIndex].choiceMaps[2];
        ShowChunk(dialogChunks[currentChunkIndex]);
    }
}