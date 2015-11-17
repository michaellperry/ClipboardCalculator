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
            return new SymbolTable(_symbols.Add(id, variable));
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