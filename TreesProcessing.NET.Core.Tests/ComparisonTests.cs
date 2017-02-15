using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace TreesProcessing.NET.Tests
{
    [TestFixture]
    public class ComparisonTests
    {
        [TestCase(TestHelper.Platform)]
        public void Comparison_Static_Equals(string platform)
        {
            var tree1 = SampleTree.Init();
            var tree2 = SampleTree.Init();

            Assert.AreEqual(0, tree1.CompareTo(tree2));
        }

        [TestCase(TestHelper.Platform)]
        public void Comparison_Dynamic_Equals(string platform)
        {
            var tree1 = SampleTree.Init();
            var tree2 = SampleTree.Init();

            Assert.AreEqual(0, tree1.Compare(tree2));
        }

        [TestCase(TestHelper.Platform)]
        public void Comparison_NotEquals(string platform)
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

        [TestCase(TestHelper.Platform)]
        public void Descendants_Static(string platform)
        {
            TestDescendants(SampleTree.Init().AllDescendants);
        }

        [TestCase(TestHelper.Platform)]
        public void Descendants_Dynamic(string platform)
        {
            TestDescendants(SampleTree.Init().GetAllDescendants());
        }

        [TestCase(TestHelper.Platform)]
        public void Children_Static(string platform)
        {
            TestChildren(SampleTree.Init().Children);
        }

        [TestCase(TestHelper.Platform)]
        public void Children_Dynamic(string platform)
        {
            TestChildren(SampleTree.Init().GetChildren());
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

        private void TestChildren(IEnumerable<Node> children)
        {
            var actualChildren = children.Select(node => node.GetType().Name);

            var expectedChildren = new List<string>()
            {
                nameof(ExpressionStatement),
                nameof(ExpressionStatement),
                nameof(ForStatement),
            };

            CollectionAssert.AreEqual(expectedChildren, actualChildren);
        }
    }
}