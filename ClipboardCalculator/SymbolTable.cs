using System;
using System.Collections.Immutable;

namespace ClipboardCalculator
{
    class SymbolTable
    {
        private readonly ImmutableDictionary<char, Symbol> _symbols;

        public SymbolTable()
        {
            _symbols = ImmutableDictionary<char, Symbol>.Empty;
        }

        private SymbolTable(ImmutableDictionary<char, Symbol> symbols)
        {
            _symbols = symbols;
        }

        public SymbolTable Plus(char id, Symbol variable)
        {
            var symbols = _symbols.ContainsKey(id)
                ? _symbols.Remove(id)
                : _symbols;
            symbols = symbols.Add(id, variable);
            return new SymbolTable(symbols);
        }

        public Symbol Lookup(char id)
        {
            Symbol variable;
            if (_symbols.TryGetValue(id, out variable))
                return variable;
            else
                return null;
        }
    }
}