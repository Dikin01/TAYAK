using lab5.Interpreter;

namespace Lab5.Expressions;

public class ProgramExpression : AbstractExpression
{
    private StatementExpression _statement;

    public ProgramExpression(StatementExpression statement)
    {
        _statement = statement;
    }

    public override object Interpret(Context context)
    {
        return _statement.Interpret(context);
    }
}