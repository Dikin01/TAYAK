namespace Lab3;

public class PushDownAutomate
{
    private readonly HashSet<string> _states = new();
    private readonly HashSet<string> _alphabet = new();
    private readonly HashSet<string> _stackAlphabet = new();
    private readonly Stack<string> _stack = new();
    private const string StartState = "s0";
    private readonly HashSet<string> _finalStates = new() { "s0" };
    private readonly HashSet<TransitionFunction> _transitionFunctions = new();
    private const string StackButton = "h0";

    public PushDownAutomate(List<string> fileLines, List<string> alphabet)
    {
        _states.Add(StartState);

        _alphabet = alphabet.ToHashSet();
        _stackAlphabet = alphabet.ToHashSet();
        _stackAlphabet.Add(StackButton);

        foreach (var line in fileLines)
            ExecuteDescriptionLine(line);

        AddTerminalTransFunctions();
        AddFinalStateTransFunction();


        PrintTransactionFunctions();
    }

    private void PrintAlphabet()
    {
        foreach(var v in _alphabet)
            Console.WriteLine(v);
    }

    private void PrintTransactionFunctions()
    {
        foreach (var tf in _transitionFunctions)
        {
            Console.WriteLine($"{tf.CurrentState}, {tf.InputSymbol}, {tf.StackSymbol} = {tf.NextState}, {tf.StackOutputSymbols}");
        }
    }

    private void ExecuteDescriptionLine(string inputLine)
    {
        var leftPart = inputLine.Substring(0, inputLine.IndexOf('>'));
        _stackAlphabet.Add(leftPart);

        var rightParts = inputLine.Substring(inputLine.IndexOf('>') + 1, inputLine.Length - leftPart.Length - 1).Split('|');
        foreach(var rightPartToStack in rightParts)
        {
            var rightPartChars = rightPartToStack.ToCharArray();
            Array.Reverse(rightPartChars);
            _transitionFunctions.Add(new TransitionFunction(StartState, "~", leftPart, StartState, new string(rightPartChars)));
        }
    }

    private void AddTerminalTransFunctions()
    {
        foreach(var letter in _alphabet)
            _transitionFunctions.Add(new TransitionFunction(StartState, letter, letter, StartState, "~"));
    }

    private void AddFinalStateTransFunction()
    {
        _transitionFunctions.Add(new TransitionFunction(StartState, "~", StackButton, StartState, "~"));
    }

    public bool IsPAExecutable(string inputLine, List<string> stackSymbols)
    {
        var curState = StartState;
        Queue<Configuration> queue = new();


        foreach (var symbol in stackSymbols)
            _stack.Push(symbol);
       

        queue.Enqueue(new Configuration(curState, inputLine, _stack, null));


        while(queue.Count != 0)
        {
            var curConfig = queue.Dequeue();

            if (_finalStates.Contains(curConfig.State) && curConfig.Stack.Count() == 0 && curConfig.InputLine == "")  return true;
            if (curConfig.Stack.Count() == 0) continue;
            if (curConfig.InputLine == "" && curConfig.Stack.Count() != 1) continue;
           
            var curStackSymbol = curConfig.Stack.Pop();

            if(curConfig.InputLine != "")
            {
                if (!_stackAlphabet.Contains(curConfig.InputLine[0].ToString()))
                {
                    Console.WriteLine($"Incorrect symbol - {curConfig.InputLine[0]}");
                    return false;
                }
            }
            

            var tfWithEmptySyms = _transitionFunctions.Where(tf => tf.CurrentState == curConfig.State &&
                                                                   tf.StackSymbol == curStackSymbol && 
                                                                   tf.InputSymbol == "~");
            if(tfWithEmptySyms.Count() > 0)
            {
                foreach(var tf in tfWithEmptySyms)
                {
                    queue.Enqueue(GetNextCongiguration(tf, curConfig));
                }
            }
            else
            {
                foreach(var tf in _transitionFunctions)
                {
                    var sym = curConfig.InputLine[0].ToString();
                    if (tf.CurrentState == curConfig.State && tf.InputSymbol == sym && tf.StackSymbol == curStackSymbol)
                    {
                        queue.Enqueue(GetNextCongiguration(tf, curConfig));
                    }
                }
            }            
        }


        return false;
    }

    private Configuration GetNextCongiguration(TransitionFunction transitionFunction, Configuration prevConfig)
    {
        var tempStack = new Stack<string>(new Stack<string>(prevConfig.Stack));
        foreach (var sym in transitionFunction.StackOutputSymbols)
        {
            if (sym == '~') continue;
            tempStack.Push(sym.ToString());
        }
        string line = prevConfig.InputLine;
        if(transitionFunction.InputSymbol != "~")
        {
            line = line.Remove(0, 1);
        }

        var newConfig = new Configuration(transitionFunction.NextState, line, tempStack, prevConfig);
        return newConfig;
    }

    private void PrintConfig(Configuration config)
    {
        Console.Write($"{config.InputLine}, ");
        foreach(var v in config.Stack)
        {
            Console.Write($"{v}");
        }
        Console.WriteLine();
    }
}

