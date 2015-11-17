using System;
using System.Collections.Immutable;
using System.Linq;

namespace ClipboardCalculator
{
    class Apply : Expression
    {
        private readonly Expression _head;
        private readonly Expression _tail;

        public Apply(Expression head, Expression tail)
        {
            _head = head;
            _tail = tail;
        }

        public Expression Head
        {
            get { return _head; }
        }

        public Expression Tail
        {
            get { return _tail; }
        }

        public override Expression Reduce()
        {
            // β-reduction
            if (_head is Lambda)
            {
                var function = (Lambda)_head;
                return function.Apply(_tail);
            }

            var newHead = _head.Reduce();
            if (newHead != null)
                return new Apply(newHead, _tail);
            var newTail = _tail.Reduce();
            if (newTail != null)
                return new Apply(_head, newTail);
            return null;
        }

        public override Expression Replace(Symbol variable, Expression argument)
        {
            var newHead = _head.Replace(variable, argument);
            var newTail = _tail.Replace(variable, argument);
            if (newHead == _head && newTail == _tail)
                return this;
            return new Apply(newHead, newTail);
        }

        public override bool DependsUpon(Symbol variable)
        {
            return _head.DependsUpon(variable) || _tail.DependsUpon(variable);
        }

        public override ImmutableList<char> FreeVariableNames(ImmutableList<Symbol> boundVariables)
        {
            return _head.FreeVariableNames(boundVariables)
                .Union(_tail.FreeVariableNames(boundVariables))
                .ToImmutableList();
        }

        public override string ToString()
        {
            string head;
            if (_head is Lambda)
                head = String.Format("({0})", _head);
            else
                head = String.Format("{0}", _head);
            string tail;
            if (_tail is Lambda || _tail is Apply)
                tail = String.Format("({0})", _tail);
            else
                tail = String.Format("{0}", _tail);
            return String.Format("{0} {1}", head, tail);
        }
    }
}