namespace ClipboardCalculator
{
    class Symbol
    {
        private readonly char _id;

        public Symbol(char id)
        {
            _id = id;
        }

        public char Default
        {
            get { return _id; }
        }
    }
}