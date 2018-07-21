using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TreeProcessing.NET
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

        public static Node DtoToModelViaReflection(NodeDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            Type dtoType = dto.GetType();
            Type modelType = Type.GetType(dtoType.FullName.Replace("Dto", ""));
            if (modelType == null)
            {
                throw new NotImplementedException($"Model destination type has not been found for source {dtoType.FullName} type");
            }
            var modelObject = (Node)Activator.CreateInstance(modelType);

            PropertyInfo[] dtoProperties = ReflectionCache.GetClassProperties(dtoType);
            Dictionary<string, PropertyInfo> modelProperties = ReflectionCache.GetClassProperties(modelType)
                .ToDictionary(propInfo => propInfo.Name);
            foreach (PropertyInfo dtoProp in dtoProperties)
            {
                object dtoPropValue = dtoProp.GetValue(dto);
                if (dtoPropValue != null)
                {
                    Type dtoPropValueType = dtoPropValue.GetType();
                    TypeInfo dtoTypeInfo = dtoPropValueType.GetTypeInfo();
                    PropertyInfo modelProperty;
                    if (!modelProperties.TryGetValue(dtoProp.Name, out modelProperty))
                    {
                        throw new NotImplementedException($"Property {dtoProp.Name} has not been found in model destination object");
                    }

                    if (dtoTypeInfo.IsValueType || dtoPropValueType == typeof(string))
                    {
                        modelProperty.SetValue(modelObject, dtoPropValue);
                    }
                    else if (dtoTypeInfo.IsSubclassOf(typeof(NodeDto)) || dtoPropValueType == typeof(NodeDto))
                    {
                        modelProperty.SetValue(modelObject, DtoToModelViaReflection((NodeDto)dtoPropValue));
                    }
                    else if (dtoTypeInfo.ImplementedInterfaces.Contains(typeof(IList)))
                    {
                        var children = (IList)dtoPropValue;
                        var destChildren = (IList)Activator.CreateInstance(modelProperty.PropertyType);
                        foreach (var child in children)
                        {
                            destChildren.Add(DtoToModelViaReflection((NodeDto)child));
                        }
                        modelProperty.SetValue(modelObject, destChildren);
                    }
                    else
                    {
                        throw new NotImplementedException($"Type \"{dtoPropValueType}\" mapping is not implemented via reflection");
                    }
                }
            }

            return modelObject;
        }

        public static NodeDto ModelToDtoDynamicViaReflection(Node model)
        {
            if (model == null)
            {
                return null;
            }
            
            Type type = model.GetType();
            Type dtoType = Type.GetType(type.FullName + "Dto");
            if (dtoType == null)
            {
                throw new NotImplementedException($"Dto destination type has not been found for source {type.FullName} type");
            }
            var dtoObject = (NodeDto)Activator.CreateInstance(dtoType);

            PropertyInfo[] properties = ReflectionCache.GetClassProperties(type);
            Dictionary<string, PropertyInfo> dtoProperties = ReflectionCache.GetClassProperties(dtoType)
                .ToDictionary(propInfo => propInfo.Name);
            foreach (PropertyInfo prop in properties)
            {
                object propValue = prop.GetValue(model);
                if (propValue != null)
                {
                    Type propValueType = propValue.GetType();
                    string propName = prop.Name;
                    TypeInfo typeInfo = propValueType.GetTypeInfo();
                    PropertyInfo dtoProperty;
                    if (!dtoProperties.TryGetValue(propName, out dtoProperty))
                    {
                        throw new NotImplementedException($"Property {propName} has not been found in dto destination object");
                    }

                    if (typeInfo.IsValueType || propValueType == typeof(string))
                    {
                        dtoProperty.SetValue(dtoObject, propValue);
                    }
                    else if (typeInfo.IsSubclassOf(typeof(Node)) || propValueType == typeof(Node))
                    {
                        dtoProperty.SetValue(dtoObject, ModelToDtoDynamicViaReflection((Node)propValue));
                    }
                    else if (typeInfo.ImplementedInterfaces.Contains(typeof(IList)))
                    {
                        var children = (IList)propValue;
                        var destChildren = (IList)Activator.CreateInstance(dtoProperty.PropertyType);
                        foreach (var child in children)
                        {
                            destChildren.Add(ModelToDtoDynamicViaReflection((Node)child));
                        }
                        dtoProperty.SetValue(dtoObject, destChildren);
                    }
                    else
                    {
                        throw new NotImplementedException($"Type \"{propValueType}\" mapping is not implemented via reflection");
                    }
                }
            }

            return dtoObject;
        }
    }
}
