﻿namespace Lab2;

public interface IAnalyzer
{
    public bool IsAutomateDescriptionCorrect(List<string> inputLines);
    public bool IsDescriptionLineCorrect(string analyzingLine);
    public string ReadState(string analyzingLine, ref int index);
}

