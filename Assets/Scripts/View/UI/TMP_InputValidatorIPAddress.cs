using System;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Input Field Validator", menuName = "Input Field Validator")]
public class TMP_InputValidatorIPAddress : TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
        if (IsDigit(ch) || IsValidPunctuation(ch)) return ch;

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
