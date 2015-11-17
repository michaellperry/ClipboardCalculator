using System;
using System.Collections.Immutable;

namespace ClipboardCalculator
{
    class BoundVariable : Expression
    {
        private Symbol _variable;

        public BoundVariable(Symbol variable)
        {
            _variable = variable;
        }

        public override Expression Reduce()
        {
            return null;
        }

        public override Expression Replace(Symbol variable, Expression argument)
        {
            if (_variable == variable)
                return argument;
            return this;
        }

        public override bool DependsUpon(Symbol variable)
        {
            return _variable == variable;
        }

        public override ImmutableList<char> FreeVariableNames(ImmutableList<Symbol> boundVariables)
        {
            if (boundVariables.Contains(_variable))
                return ImmutableList<char>.Empty;
            else
                return new char[] { _variable.Default }.ToImmutableList();
        }

        public override string ToString()
        {
            return _variable.Default.ToString();
        }
    }
}