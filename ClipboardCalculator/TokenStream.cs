using System;

namespace ClipboardCalculator
{
    internal class TokenStream
    {
        private readonly string _text;
        private int _position = 0;

        public TokenStream(string text)
        {
            _text = text;
        }

        public int Position
        {
            get { return _position; }
        }

        public char Peek()
        {
            return _text[_position];
        }

        public bool Done()
        {
            return _position == _text.Length;
        }

        public void Consume(char c)
        {
            if (Peek() != c)
                throw new ParserException(String.Format("Unexpected token \"{0}\" in position {1}", c, _position));
            _position++;
            while (!Done() && Peek() == ' ')
                _position++;
        }
    }
}