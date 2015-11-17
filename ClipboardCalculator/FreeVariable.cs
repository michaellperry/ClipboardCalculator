using System;
using System.Collections.Immutable;

namespace ClipboardCalculator
{
    class FreeVariable : Expression
    {
        private char _id;

        public FreeVariable(char id)
        {
            _id = id;
        }

        public override Expression Reduce()
        {
            return null;
        }

        public override Expression Replace(Symbol variable, Expression argument)
        {
            return this;
        }

        public override bool DependsUpon(Symbol variable)
        {
            return false;
        }

        public override ImmutableList<char> FreeVariableNames(ImmutableList<Symbol> boundVariables)
        {
            return new char[] { _id }.ToImmutableList();
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}