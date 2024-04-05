namespace Cramming.Account.API.AcceptanceTests.Support
{
    public class ActionAttempt<TInput, TResult>(Func<TInput, Task<TResult>> action)
    {
        public TInput? LastInput { get; private set; }
        public TResult? LastResult { get; private set; }

        public async Task<TResult> PerformAsync(TInput input)
        {
            LastInput = input;
            LastResult = await action(input);
            return LastResult;
        }
    }

    public class ActionAttemptFactory
    {
        public ActionAttempt<TInput, TResult> Create<TInput, TResult>(Func<TInput, Task<TResult>> action)
        {
            return new ActionAttempt<TInput, TResult>(action);
        }
    }
}
