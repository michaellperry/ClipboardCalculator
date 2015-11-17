using System;
using System.Collections.Immutable;

namespace ClipboardCalculator
{
    class Lambda : Expression
    {
        private readonly Symbol _variable;
        private readonly Expression _body;

        public Lambda(Symbol variable, Expression body)
        {
            _variable = variable;
            _body = body;
        }

        public override Expression Reduce()
        {
            // η-reduction
            if (_body is Apply)
            {
                var apply = (Apply)_body;
                if (apply.Tail is BoundVariable &&
                    apply.Tail.DependsUpon(_variable) &&
                    !apply.Head.DependsUpon(_variable))
                {
                    return apply.Head;
                }
            }

            var newBody = _body.Reduce();
            if (newBody != null)
                return new Lambda(_variable, newBody);
            return null;
        }

        public Expression Apply(Expression argument)
        {
            return _body.Replace(_variable, argument);
        }

        public override Expression Replace(Symbol variable, Expression argument)
        {
            var newBody = _body.Replace(variable, argument);
            if (newBody == _body)
                return this;
            ImmutableList<char> taken = newBody.FreeVariableNames(new Symbol[] { _variable }.ToImmutableList());
            if (taken.Contains(_variable.Default))
            {
                int ordinal = (int)(char.ToLower(_variable.Default) - 'a') + 1 % 26;
                while (taken.Contains((char)('a' + ordinal)))
                    ordinal++;
                var newVariable = new Symbol((char)('a' + ordinal));
                newBody = newBody.Replace(_variable, new BoundVariable(newVariable));
                return new Lambda(newVariable, newBody);
            }
            return new Lambda(_variable, newBody);
        }

        public override bool DependsUpon(Symbol variable)
        {
            return _body.DependsUpon(variable);
        }

        public override ImmutableList<char> FreeVariableNames(ImmutableList<Symbol> boundVariables)
        {
            return _body.FreeVariableNames(boundVariables.Add(_variable));
        }

        public override string ToString()
        {
            return String.Format("λ{0}.{1}", _variable.Default, _body);
        }
    }
}