using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClipboardCalculator.Test
{
    [TestClass]
    public class LambdaTest
    {
        [TestMethod]
        public void CanApplyAFunction()
        {
            string result = Evaluator.Evaluate("(λx.x) A");
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void CanApplyAFunctionToALambda()
        {
            string result = Evaluator.Evaluate("(λx.x) (λy.y)");
            Assert.AreEqual("λy.y", result);
        }

        [TestMethod]
        public void CanApplyAFunctionToAnApplication()
        {
            string result = Evaluator.Evaluate("(λx.x) (F N)");
            Assert.AreEqual("F N", result);
        }

        [TestMethod]
        public void PreservesOrderOfOperations()
        {
            string result = Evaluator.Evaluate("(λx.x) (F (G N))");
            Assert.AreEqual("F (G N)", result);
        }

        [TestMethod]
        public void EvaluatesInNormalOrder1()
        {
            string result = Evaluator.Evaluate("(λx.λy.x) A ((λs.s) (λs.s))");
            Assert.AreEqual("(λy.A) ((λs.s) (λs.s))", result);
        }

        [TestMethod]
        public void EvaluatesInNormalOrder2()
        {
            string result = Evaluator.Evaluate("(λy.A) ((λs.s) (λs.s))");
            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void WillPerformEtaReduction()
        {
            string result = Evaluator.Evaluate("λf.λx.f x");
            Assert.AreEqual("λf.f", result);
        }

        [TestMethod]
        public void WillPerformAlphaConversionWhenNecessary()
        {
            string result = Evaluator.Evaluate("(λa.λx.a x a) x");
            Assert.AreEqual("λy.x y x", result);
        }
    }
}
