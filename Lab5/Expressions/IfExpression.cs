namespace Lab5.Expressions;

public class IfExpression : AbstractExpression
{
    private BoolExpression _bool;

    public IfExpression(BoolExpression boolExpression)
    {
        _bool = boolExpression;
    }
    public override object Interpret(Context context)
    {
        return _bool.Interpret(context);
    }
}