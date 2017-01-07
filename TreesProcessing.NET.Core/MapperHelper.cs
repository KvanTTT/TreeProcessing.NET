using System;
using System.Linq;

namespace TreesProcessing.NET
{
    public static class MapperHelper
    {
        public static Node DtoToModel(NodeDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            Node result;
            switch (dto.NodeType)
            {
                case NodeType.BinaryOperatorExpression:
                    var binaryOperatorExpressionDto = dto as BinaryOperatorExpressionDto;
                    result = new BinaryOperatorExpression(
                        (Expression)DtoToModel(binaryOperatorExpressionDto.Left),
                        binaryOperatorExpressionDto.Operator,
                        (Expression)DtoToModel(binaryOperatorExpressionDto.Right));
                    break;
                case NodeType.BlockStatement:
                    var blockStatementDto = dto as BlockStatementDto;
                    result = new BlockStatement(
                        blockStatementDto.Statements.Select(s => (Statement)DtoToModel(s)).ToList());
                    break;
                case NodeType.BooleanLiteral:
                    var booleanLiteralDto = dto as BooleanLiteralDto;
                    result = new BooleanLiteral(booleanLiteralDto.Value);
                    break;
                case NodeType.ExpressionStatement:
                    var expressionStatementDto = dto as ExpressionStatementDto;
                    result = new ExpressionStatement((Expression)DtoToModel(expressionStatementDto.Expression));
                    break;
                case NodeType.FloatLiteral:
                    var floatLiteralDto = dto as FloatLiteralDto;
                    result = new FloatLiteral(floatLiteralDto.Value);
                    break;
                case NodeType.ForStatement:
                    var forStatementDto = dto as ForStatementDto;
                    result = new ForStatement(forStatementDto.Initializers.Select(i => (Statement)DtoToModel(i)).ToList(),
                        (Expression)DtoToModel(forStatementDto.Condition),
                        forStatementDto.Iterators.Select(i => (Expression)DtoToModel(i)).ToList(),
                        (Statement)DtoToModel(forStatementDto.Statement));
                    break;
                case NodeType.Identifier:
                    var identifierDto = dto as IdentifierDto;
                    result = new Identifier(identifierDto.Id);
                    break;
                case NodeType.IfElseStatement:
                    var ifElseStatementDto = dto as IfElseStatementDto;
                    result = new IfElseStatement(
                        (Expression)DtoToModel(ifElseStatementDto.Condition),
                        (Statement)DtoToModel(ifElseStatementDto.TrueStatement),
                        (Statement)DtoToModel(ifElseStatementDto.FalseStatement));
                    break;
                case NodeType.IntegerLiteral:
                    var integerLiteralDto = dto as IntegerLiteralDto;
                    result = new IntegerLiteral(integerLiteralDto.Value);
                    break;
                case NodeType.InvocationExpression:
                    var invocationExpressionDto = dto as InvocationExpressionDto;
                    result = new InvocationExpression(
                        (Expression)DtoToModel(invocationExpressionDto.Target),
                        invocationExpressionDto.Args.Select(a => (Expression)DtoToModel(a)).ToList());
                    break;
                case NodeType.MemberReferenceExpression:
                    var memberReferenceExpressionDto = dto as MemberReferenceExpressionDto;
                    result = new MemberReferenceExpression(
                        (Expression)DtoToModel(memberReferenceExpressionDto.Target),
                        (Identifier)DtoToModel(memberReferenceExpressionDto.Name));
                    break;
                case NodeType.NullLiteral:
                    result = new NullLiteral();
                    break;
                case NodeType.StringLiteral:
                    var stringLiteralDto = dto as StringLiteralDto;
                    result = new StringLiteral(stringLiteralDto.Value);
                    break;
                case NodeType.UnaryOperatorExpression:
                    var unaryOperatorExpressionDto = dto as UnaryOperatorExpressionDto;
                    result = new UnaryOperatorExpression(
                        unaryOperatorExpressionDto.Operator, (Expression)DtoToModel(unaryOperatorExpressionDto.Expression));
                    break;
                default:
                    throw new NotImplementedException($"{nameof(dto.NodeType)} mapping is not implemented");
            }
            return result;
        }

        public static NodeDto ModelToDto(Node model)
        {
            if (model == null)
            {
                return null;
            }
            NodeDto result;
            switch (model.NodeType)
            {
                case NodeType.BinaryOperatorExpression:
                    var binaryOperatorExpression = model as BinaryOperatorExpression;
                    result = new BinaryOperatorExpressionDto(
                        (ExpressionDto)ModelToDto(binaryOperatorExpression.Left),
                        binaryOperatorExpression.Operator,
                        (ExpressionDto)ModelToDto(binaryOperatorExpression.Right));
                    break;
                case NodeType.BlockStatement:
                    var blockStatement = model as BlockStatement;
                    result = new BlockStatementDto(
                        blockStatement.Statements.Select(s => (StatementDto)ModelToDto(s)).ToList());
                    break;
                case NodeType.BooleanLiteral:
                    var booleanLiteral = model as BooleanLiteral;
                    result = new BooleanLiteralDto(booleanLiteral.Value);
                    break;
                case NodeType.ExpressionStatement:
                    var expressionStatement = model as ExpressionStatement;
                    result = new ExpressionStatementDto((ExpressionDto)ModelToDto(expressionStatement.Expression));
                    break;
                case NodeType.FloatLiteral:
                    var floatLiteral = model as FloatLiteral;
                    result = new FloatLiteralDto(floatLiteral.Value);
                    break;
                case NodeType.ForStatement:
                    var forStatement = model as ForStatement;
                    result = new ForStatementDto(forStatement.Initializers.Select(i => (StatementDto)ModelToDto(i)).ToList(),
                        (ExpressionDto)ModelToDto(forStatement.Condition),
                        forStatement.Iterators.Select(i => (ExpressionDto)ModelToDto(i)).ToList(),
                        (StatementDto)ModelToDto(forStatement.Statement));
                    break;
                case NodeType.Identifier:
                    var identifier = model as Identifier;
                    result = new IdentifierDto(identifier.Id);
                    break;
                case NodeType.IfElseStatement:
                    var ifElseStatement = model as IfElseStatement;
                    result = new IfElseStatementDto(
                        (ExpressionDto)ModelToDto(ifElseStatement.Condition),
                        (StatementDto)ModelToDto(ifElseStatement.TrueStatement),
                        (StatementDto)ModelToDto(ifElseStatement.FalseStatement));
                    break;
                case NodeType.IntegerLiteral:
                    var integerLiteral = model as IntegerLiteral;
                    result = new IntegerLiteralDto(integerLiteral.Value);
                    break;
                case NodeType.InvocationExpression:
                    var invocationExpression = model as InvocationExpression;
                    result = new InvocationExpressionDto(
                        (ExpressionDto)ModelToDto(invocationExpression.Target),
                        invocationExpression.Args.Select(a => (ExpressionDto)ModelToDto(a)).ToList());
                    break;
                case NodeType.MemberReferenceExpression:
                    var memberReferenceExpression = model as MemberReferenceExpression;
                    result = new MemberReferenceExpressionDto(
                        (ExpressionDto)ModelToDto(memberReferenceExpression.Target),
                        (IdentifierDto)ModelToDto(memberReferenceExpression.Name));
                    break;
                case NodeType.NullLiteral:
                    result = new NullLiteralDto();
                    break;
                case NodeType.StringLiteral:
                    var stringLiteral = model as StringLiteral;
                    result = new StringLiteralDto(stringLiteral.Value);
                    break;
                case NodeType.UnaryOperatorExpression:
                    var unaryOperatorExpression = model as UnaryOperatorExpression;
                    result = new UnaryOperatorExpressionDto(
                        unaryOperatorExpression.Operator, (ExpressionDto)ModelToDto(unaryOperatorExpression.Expression));
                    break;
                default:
                    throw new NotImplementedException($"{nameof(model.NodeType)} mapping is not implemented");
            }
            return result;
        }
    }
}
