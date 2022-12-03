using lab5;

var PushdownAutomaton = new CBASInterpreter(@"../../../test2.txt");

var program = File.ReadAllText(@"../../../program.txt");
PushdownAutomaton.Interpret(program);