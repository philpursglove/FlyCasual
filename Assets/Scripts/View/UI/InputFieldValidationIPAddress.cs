using System;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldValidationIPAddress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<InputField>().onValidateInput += IsValidInput;
    }

    private void OnDestroy()
    {
        this.gameObject.GetComponent<InputField>().onValidateInput -= IsValidInput;
    }

    public char IsValidInput(string text, int charIndex, char addedChar)
    {
        if (IsDigit(addedChar) || IsValidPunctuation(addedChar)) return addedChar;

        return '\0';
    }

    private bool IsDigit(char ch)
    {
        return Char.IsNumber(ch);
    }

    private bool IsValidPunctuation(char ch)
    {
        return ch.Equals('.');
    }
}
