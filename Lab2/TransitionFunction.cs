namespace Lab2;

public class TransitionFunction
{
    public string CurrentState { get; }
    public char Symbol { get; }
    public string NextState { get; }

    public TransitionFunction(string curState, char symbol, string nextState)
    {
        CurrentState = curState;
        Symbol = symbol;
        NextState = nextState;
    }

    public bool Equals(TransitionFunction transitionFunction2)
    {
        return CurrentState == transitionFunction2.CurrentState &&
               Symbol == transitionFunction2.Symbol &&
               NextState == transitionFunction2.NextState;
    }
}