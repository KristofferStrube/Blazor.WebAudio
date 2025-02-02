using KristofferStrube.Blazor.FormulaEditor;
using KristofferStrube.Blazor.FormulaEditor.BooleanExpressions;
using KristofferStrube.Blazor.FormulaEditor.Expressions;

namespace KristofferStrube.Blazor.WebAudio.WasmExample.CustomPeriodicWaves;

public static class ExpressionTemplates
{
    public static NumberReturningExpression SineWave(Identifier nIdentifier) => new CasesExpression()
    {
        Cases = [new() {
                Value = new NumericExpression() { Value = 1 },
                Condition = new EqualsOperator() { First = new IdentifierExpression() { Value = nIdentifier }, Second = new NumericExpression() { Value = 1 } }
            }],
        Otherwise = new NumericExpression() { Value = 0 },
    };

    public static NumberReturningExpression SquareWave(Identifier nIdentifier) => new MultiplicationOperator()
    {
        First = new FractionOperator()
        {
            Numerator = new NumericExpression() { Value = 2 },
            Denominator = new MultiplicationOperator()
            {
                First = new IdentifierExpression() { Value = nIdentifier },
                Second = new ConstantExpression() { Value = new Constant("π", Math.PI) },
                ExplicitOperator = false
            }
        },
        Second = new SubtractionOperator()
        {
            First = new NumericExpression() { Value = 1 },
            Second = new PowerOperator()
            {
                Value = new NumericExpression() { Value = -1, Parenthesis = true },
                Power = new IdentifierExpression() { Value = nIdentifier }
            },
            Parenthesis = true
        },
        ExplicitOperator = false
    };

    public static NumberReturningExpression SawtoothWave(Identifier nIdentifier) => new MultiplicationOperator()
    {
        First = new PowerOperator()
        {
            Value = new NumericExpression() { Value = -1, Parenthesis = true },
            Power = new AdditionOperator()
            {
                First = new IdentifierExpression() { Value = nIdentifier },
                Second = new NumericExpression() { Value = 1 },
                Parenthesis = true
            }
        },
        Second = new FractionOperator()
        {
            Numerator = new NumericExpression() { Value = 2 },
            Denominator = new MultiplicationOperator()
            {
                First = new IdentifierExpression() { Value = nIdentifier },
                Second = new ConstantExpression() { Value = new Constant("π", Math.PI) },
                ExplicitOperator = false
            }
        },
        ExplicitOperator = false
    };

    public static NumberReturningExpression TriangleWave(Identifier nIdentifier) => new FractionOperator()
    {
        Numerator = new MultiplicationOperator()
        {
            First = new NumericExpression() { Value = 8 },
            Second = new FunctionExpression()
            {
                Function = new Function("sin", v => Math.Sin(v)),
                Input = new FractionOperator()
                {
                    Numerator = new MultiplicationOperator()
                    {
                        First = new IdentifierExpression() { Value = nIdentifier },
                        Second = new ConstantExpression() { Value = new Constant("π", Math.PI) },
                        ExplicitOperator = false
                    },
                    Denominator = new NumericExpression() { Value = 2 }
                },
                Parenthesis = false
            },
            ExplicitOperator = false
        },
        Denominator = new PowerOperator()
        {
            Value = new MultiplicationOperator()
            {
                First = new ConstantExpression() { Value = new Constant("π", Math.PI) },
                Second = new IdentifierExpression() { Value = nIdentifier },
                ExplicitOperator = false,
                Parenthesis = true
            },
            Power = new NumericExpression() { Value = 2 }
        }
    };
}
