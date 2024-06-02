using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryManagerBoss : MonoBehaviour
{
    [SerializeField] private List<Sprite> storyImages;
    [SerializeField] private Image displayImage;
    [SerializeField] private ImageFadersBoss imageFader;
    [SerializeField] private Image background;
    private int currentIndex = 0;

    Color cerah;
    Color cerahBG;

    void Start()
    {
        // Time.timeScale = 0f;
        cerah = displayImage.color;
        cerah.a = 1f;
        displayImage.color = cerah;

        cerahBG = background.color;
        cerahBG.a = 1f;
        background.color = cerahBG;

        if (storyImages.Count > 0)
        {
            displayImage.sprite = storyImages[currentIndex];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextImage();
        }
    }

    void NextImage()
    {
        currentIndex++;
        if (currentIndex < storyImages.Count)
        {
            StartCoroutine(SwitchImage());
        }
        else
        {
            StartCoroutine(HideImage());
            cerahBG.a = 0f;
            background.color = cerahBG;
            
            Debug.Log("Cerita selesai");
        }
    }

    private IEnumerator SwitchImage()
    {
        imageFader.FadeOut(displayImage);
        yield return new WaitForSeconds(imageFader.fadeDuration);
        displayImage.sprite = storyImages[currentIndex];
        imageFader.FadeIn(displayImage);
        yield return null;
    }

    private IEnumerator HideImage()
    {
        imageFader.FadeOut(displayImage);
        yield return new WaitForSeconds(imageFader.fadeDuration);
        // displayImage.gameObject.SetActive(false); // Menonaktifkan UI Image
        // Time.timeScale = 1f;
        Destroy(gameObject);
    }
}