using System;
using System.Collections.Immutable;

namespace ClipboardCalculator
{
    abstract class Expression
    {
        public abstract Expression Reduce();
        public abstract Expression Replace(Symbol variable, Expression argument);
        public abstract bool DependsUpon(Symbol variable);
        public abstract ImmutableList<char> FreeVariableNames(ImmutableList<Symbol> boundVariables);
    }
}