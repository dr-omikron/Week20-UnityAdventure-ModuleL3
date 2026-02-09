using System;
using System.Collections.Generic;
using System.Text;
using _Project.Develop.Runtime.UI;
using Random = UnityEngine.Random;

namespace _Project.Develop.Runtime.Gameplay.Features
{
    public class SymbolsSequenceGenerator : IStringChanged
    {
        public event Action<string> Changed;

        public string Generate(List<char> symbols, int sequenceLenght)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < sequenceLenght; i++)
            {
                char symbol = symbols[Random.Range(0, symbols.Count)];

                if(symbol == ' ')
                    throw new ArgumentException($"Blank symbol in {nameof(symbols)}");

                result.Append(symbol);
            }

            string resultString = result.ToString();

            Changed?.Invoke(resultString);
            return resultString;
        }

    }
}
