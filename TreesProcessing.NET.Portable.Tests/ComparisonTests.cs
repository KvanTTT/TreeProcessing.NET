using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class ComparisonTests
    {
        [Test]
        public void Comparison_Static_Equals()
        {
            var tree1 = SampleTree.Init();
            var tree2 = SampleTree.Init();

            Assert.AreEqual(0, tree1.CompareTo(tree2));
        }

        [Test]
        public void Comparison_Dynamic_Equals()
        {
            var tree1 = SampleTree.Init();
            var tree2 = SampleTree.Init();

            Assert.AreEqual(0, tree1.Compare(tree2));
        }

        [Test]
        public void Comparison_NotEquals()
        {
            var tree1 = SampleTree.Init();
            var tree2 = SampleTree.Init();
            var visitor = new StaticVisitor();

            dynamic treeWithChangedInt = visitor.Visit(tree2);
            treeWithChangedInt.Statements[0].Expression.Right.Value = 0;

            Assert.AreEqual(1, tree1.CompareTo(treeWithChangedInt));
            Assert.AreEqual(1, tree1.Compare((Node)treeWithChangedInt));

            dynamic treeWithChangedListCount = visitor.Visit(tree2);
            treeWithChangedListCount.Statements = new List<Statement> { treeWithChangedListCount.Statements[0] };

            Assert.AreEqual(2, tree1.CompareTo(treeWithChangedListCount));
            Assert.AreEqual(2, tree1.Compare((Node)treeWithChangedListCount));
        }

        [Test]
        public void Descendants_Static()
        {
            TestDescendants(SampleTree.Init().Descendants);
        }

        [Test]
        public void Descendants_Dynamic()
        {
            TestDescendants(SampleTree.Init().GetDescendants());
        }

        private void TestDescendants(IEnumerable<Node> descendants)
        {
            var actualDescendants = descendants.Select(node => node.GetType().Name);

            var expectedDescendants = new List<string>()
            {
                nameof(ExpressionStatement),
                nameof(BinaryOperatorExpression),
                nameof(Identifier),
                nameof(IntegerLiteral),
                nameof(ExpressionStatement),
                nameof(InvocationExpression),
                nameof(MemberReferenceExpression),
                nameof(MemberReferenceExpression),
                nameof(Identifier),
                nameof(Identifier),
                nameof(Identifier),
                nameof(BooleanLiteral),
                nameof(StringLiteral),
                nameof(NullLiteral),
                nameof(UnaryOperatorExpression),
                nameof(FloatLiteral),
                nameof(ForStatement),
                nameof(ExpressionStatement),
                nameof(BinaryOperatorExpression),
                nameof(Identifier),
                nameof(IntegerLiteral),
                nameof(BinaryOperatorExpression),
                nameof(Identifier),
                nameof(IntegerLiteral),
                nameof(UnaryOperatorExpression),
                nameof(Identifier),
                nameof(IfElseStatement),
                nameof(BinaryOperatorExpression),
                nameof(Identifier),
                nameof(IntegerLiteral),
                nameof(ExpressionStatement),
                nameof(InvocationExpression),
                nameof(Identifier),
            };

            CollectionAssert.AreEqual(expectedDescendants, actualDescendants);
        }
    }
}