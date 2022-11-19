namespace Lab2;

public class Automate
{
    private const string StartState = "q0";
    
    private readonly List<string> _states;
    private readonly HashSet<string> _finallyStates = new();
    private List<TransitionFunction> _transitionFunctions;

    public Automate(List<string> fileLines)
    {
        _states = new List<string>();
        _transitionFunctions = new List<TransitionFunction>();
        
        foreach(var fileLine in fileLines)
        {
            ExecuteDescriptionLine(fileLine);
        }
        _states = _states.Distinct().ToList();
        CheckFinallyStates();
    }


    private void AddTransitionFunc(string curState, char symbol, string nextState)
    {
        _transitionFunctions.Add(new TransitionFunction(curState, symbol, nextState));
    }

    private void ExecuteDescriptionLine(string line)
    {
        var i = 0;

        var curState = ReadState(line, ref i);
        _states.Add(curState);

        i += 1;
        var symbol = line[i];

        i += 2;

        var nextState = ReadState(line, ref i);
        _states.Add(nextState);
        AddTransitionFunc(curState, symbol, nextState);
    }

    public void PrintTransitionFunctions()
    {
        foreach(var func in _transitionFunctions)
            Console.WriteLine($"{func.CurrentState} : {func.Symbol} - {func.NextState}");
        Console.WriteLine();
    }

    public bool IsAutomateDeterministic()
    {
        return _transitionFunctions
            .GroupBy(tf => new { tf.CurrentState, tf.Symbol })
            .All(func => func.Count() <= 1);
    }

    public bool IsExecutableForInputLine(string inputLine)
    {
        var curState = StartState;
        
        foreach(var symbol in inputLine)
        {
            if(_transitionFunctions.Count(tf => tf.CurrentState.Equals(curState) && tf.Symbol.Equals(symbol)) != 1)
                return false;
            
            curState = _transitionFunctions.First(tf => tf.CurrentState.Equals(curState) && tf.Symbol.Equals(symbol)).NextState;
        }

        return _finallyStates.Contains(curState);
    }

    public void Determination()
    {

        while (!IsAutomateDeterministic())
        {
            List<List<string>> newStatesByPair = new();
            List<string> newStatesNames = new();
            foreach (var func in _transitionFunctions.GroupBy(tf => new { tf.CurrentState, tf.Symbol }))
            {
                if (func.Count() <= 1)
                    continue;
                
                var newStateName = "";
                var sortedNextStatsArray = func.OrderBy(n => n.NextState).ToArray();
                var backupStates = new List<string>();
                    
                foreach (var dest in sortedNextStatsArray)
                {
                    newStateName += dest.NextState;
                    backupStates.Add(dest.NextState);
                }

                newStatesByPair.Add(backupStates);
                newStatesNames.Add(newStateName);

                _transitionFunctions.RemoveAll(tf => tf.CurrentState == func.Key.CurrentState && tf.Symbol == func.Key.Symbol);
                _transitionFunctions.Add(new TransitionFunction(func.Key.CurrentState, func.Key.Symbol, newStateName));
                _states.Add(newStateName);
            }

            // СДЕЛАЙТЕ ВИД, ЧТО НЕ ВИДИТЕ ЭТО
            var newTransF = new List<TransitionFunction>();
            for (int i = 0; i < newStatesByPair.Count; i++)
            {
                foreach (var newStates in newStatesByPair[i])
                {
                    foreach (var f in _transitionFunctions)
                    {
                        if (newStates == f.CurrentState)
                        {
                            newTransF.Add(new TransitionFunction(newStatesNames[i], f.Symbol, f.NextState));
                        }
                    }
                }
            }

            foreach (var f in newTransF)
                _transitionFunctions.Add(f);

            _transitionFunctions = DeleteRepeatFunctions();

        }
        CheckFinallyStates();
        Console.WriteLine("Automate is Determizated!\n");
    }
    
    private static string ReadState(string analyzingLine, ref int index)
    {
        var state = "";
        while (char.IsDigit(analyzingLine[index]) || char.IsLetter(analyzingLine[index]))
        {
            state += analyzingLine[index];
            index++;
            if (analyzingLine.Length == index) break;
        }
        return state;
    }

    private List<TransitionFunction> DeleteRepeatFunctions()
    {
        var transitions = new List<TransitionFunction>();
        foreach (var item in _transitionFunctions)
        {
            var isEntry = false;
            foreach (var item2 in transitions)
            {
                if (item.Equals(item2))
                {
                    isEntry = true;
                }
            }
            if (!isEntry) transitions.Add(item);
        }
        return transitions;
    }

    private void CheckFinallyStates()
    {
        foreach (var state in _states.Where(state => state.Contains("f")))
            _finallyStates.Add(state);
    }
}

