using AutoMapper;

namespace TreeProcessing.NET
{
    public class AutoMapperHelper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Node, NodeDto>()
                    .Include<Statement, StatementDto>()
                    .Include<Expression, ExpressionDto>()
                    .Include<Terminal, TerminalDto>()
                    .ReverseMap();

                cfg.CreateMap<Statement, StatementDto>()
                    .Include<BlockStatement, BlockStatementDto>()
                    .Include<ExpressionStatement, ExpressionStatementDto>()
                    .Include<ForStatement, ForStatementDto>()
                    .Include<IfElseStatement, IfElseStatementDto>()
                    .ReverseMap();

                cfg.CreateMap<Expression, ExpressionDto>()
                    .Include<BinaryOperatorExpression, BinaryOperatorExpressionDto>()
                    .Include<InvocationExpression, InvocationExpressionDto>()
                    .Include<MemberReferenceExpression, MemberReferenceExpressionDto>()
                    .Include<UnaryOperatorExpression, UnaryOperatorExpressionDto>()
                    .Include<Terminal, TerminalDto>()
                    .ReverseMap();

                cfg.CreateMap<Terminal, TerminalDto>()
                    .Include<BooleanLiteral, BooleanLiteralDto>()
                    .Include<FloatLiteral, FloatLiteralDto>()
                    .Include<Identifier, IdentifierDto>()
                    .Include<IntegerLiteral, IntegerLiteralDto>()
                    .Include<NullLiteral, NullLiteralDto>()
                    .Include<StringLiteral, StringLiteralDto>()
                    .ReverseMap();

                cfg.CreateMap<BlockStatement, BlockStatementDto>().ReverseMap();
                cfg.CreateMap<ExpressionStatement, ExpressionStatementDto>().ReverseMap();
                cfg.CreateMap<ForStatement, ForStatementDto>().ReverseMap();
                cfg.CreateMap<IfElseStatement, IfElseStatementDto>().ReverseMap();

                cfg.CreateMap<BinaryOperatorExpression, BinaryOperatorExpressionDto>().ReverseMap();
                cfg.CreateMap<InvocationExpression, InvocationExpressionDto>().ReverseMap();
                cfg.CreateMap<MemberReferenceExpression, MemberReferenceExpressionDto>().ReverseMap();
                cfg.CreateMap<UnaryOperatorExpression, UnaryOperatorExpressionDto>().ReverseMap();

                cfg.CreateMap<BooleanLiteral, BooleanLiteralDto>().ReverseMap();
                cfg.CreateMap<FloatLiteral, FloatLiteralDto>().ReverseMap();
                cfg.CreateMap<Identifier, IdentifierDto>().ReverseMap();
                cfg.CreateMap<IntegerLiteral, IntegerLiteralDto>().ReverseMap();
                cfg.CreateMap<NullLiteral, NullLiteralDto>().ReverseMap();
                cfg.CreateMap<StringLiteral, StringLiteralDto>().ReverseMap();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}
