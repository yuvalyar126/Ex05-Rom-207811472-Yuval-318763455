namespace Ex02
{
    internal struct GuessResult
    {
        internal int Bulls { get; }
        internal int Hits { get; }
        internal string Result { get; }

        internal GuessResult(int i_Bulls, int i_Hits, string i_Result)
        {
            Bulls = i_Bulls;
            Hits = i_Hits;
            Result = i_Result;
        }
    }
}
