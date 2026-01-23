using System.Collections;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI TextUI;
    public float charDelay = 0.03f;

    [Header("Sound")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _typeSounds;
    [SerializeField] private float minPitch = 0.95f;
    [SerializeField] private float maxPitch = 1.05f;

    private string _fullText;
    private Coroutine _typingCoroutine;

    public bool IsTyping { get; private set; }

    public void Play(string fullText)
    {
        _fullText = fullText;
        if (_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);

        TextUI.text = fullText;
        TextUI.ForceMeshUpdate();
        TextUI.maxVisibleCharacters = 0;

        _typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        int totalChars = TextUI.textInfo.characterCount;

        for (int i = 0; i < totalChars; i++)
        {
            TextUI.maxVisibleCharacters = i;
            PlayTypeSound(i);
            yield return new WaitForSeconds(charDelay);
        }

        IsTyping = false;
    }

    private void PlayTypeSound(int charIndex)
    {
        if (_typeSounds.Length == 0 || _audioSource == null)
            return;

        char currentChar = _fullText[charIndex];

        if (char.IsWhiteSpace(currentChar))
            return;

        _audioSource.pitch = Random.Range(minPitch, maxPitch);
        _audioSource.PlayOneShot(_typeSounds[Random.Range(0, _typeSounds.Length)]);
    }

    public void Skip()
    {
        TextUI.maxVisibleCharacters = TextUI.textInfo.characterCount;
        IsTyping = false;
    }
}
