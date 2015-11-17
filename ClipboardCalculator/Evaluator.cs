using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardCalculator
{
    public class Evaluator
    {
        public static string Evaluate(string text)
        {
            var tokenStream = new TokenStream(text);
            Expression exp = ParseExp(tokenStream, new SymbolTable());
            if (!tokenStream.Done())
                throw new ParserException(String.Format("Extra characters after position {0}", tokenStream.Position));
            var resolved = exp.Reduce();
            if (resolved == null)
                return "";
            return resolved.ToString();
        }

        private static Expression ParseExp(TokenStream tokenStream, SymbolTable symbols)
        {
            var head = ParseTerm(tokenStream, symbols);
            while (true)
            {
                if (tokenStream.Done() || tokenStream.Peek() == ')')
                    return head;
                var next = ParseTerm(tokenStream, symbols);
                head = new Apply(head, next);
            }
        }

        private static Expression ParseTerm(TokenStream tokenStream, SymbolTable symbols)
        {
            if (tokenStream.Peek() == '(')
            {
                tokenStream.Consume('(');
                var inner = ParseExp(tokenStream, symbols);
                tokenStream.Consume(')');
                return inner;
            }
            if (tokenStream.Peek() == 'λ')
            {
                tokenStream.Consume('λ');
                char id = tokenStream.Peek();
                if (!Char.IsLetter(id))
                    throw new ParserException(String.Format("Expected identifier at position {0}", tokenStream.Position));
                tokenStream.Consume(id);
                tokenStream.Consume('.');
                var variable = new Symbol(id);
                var body = ParseExp(tokenStream, symbols.Plus(id, variable));
                return new Lambda(variable, body);
            }
            if (Char.IsLetter(tokenStream.Peek()))
            {
                char id = tokenStream.Peek();
                tokenStream.Consume(id);
                var variable = symbols.Lookup(id);
                if (variable != null)
                    return new BoundVariable(variable);
                else
                    return new FreeVariable(id);
            }
            throw new ParserException(String.Format("Unexpected character \"{0}\" at position {1}", tokenStream.Peek(), tokenStream.Position));
        }
    }
}
