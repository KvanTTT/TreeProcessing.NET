using NUnit.Framework;
using System.Collections.Generic;
using TreesProcessing.NET;
using TreesProcessing.NET.Tests;

namespace TreesProcessing.NET.Core.Tests
{
    [TestFixture]
    public class HashTests
    {
        [TestCase(TestHelper.Platform)]
        public void Hash_EqualNodes(string platform)
        {
            Node tree1 = SampleTree.Init();
            Node tree2 = SampleTree.Init();

            Assert.AreEqual(tree1.GetHashCode(), tree2.GetHashCode());
        }

        [TestCase(TestHelper.Platform)]
        public void Hash_MerkelizedAst(string platform)
        {
            Node tree = SampleTree.Init();
            List<Statement> statements = ((BlockStatement)tree).Statements;
            var expressionStatement = (ExpressionStatement)statements[0];
            var forStatement = (ForStatement)statements[2];

            int treeHashBefore = tree.GetHashCode();
            int expressionStatementHashBefore = expressionStatement.GetHashCode();
            int forStatementHashBefore = forStatement.GetHashCode();

            ((BinaryOperatorExpression)expressionStatement.Expression).Operator = "!=";

            int treeHashAfter = tree.GetHashCode();
            int expressionStatementHashAfter = expressionStatement.GetHashCode();
            int forStatementHashAfter = forStatement.GetHashCode();

            Assert.AreNotEqual(treeHashBefore, treeHashAfter);
            Assert.AreNotEqual(expressionStatementHashBefore, expressionStatementHashAfter);
            Assert.AreEqual(forStatementHashBefore, forStatementHashAfter);
        }
    }
}
