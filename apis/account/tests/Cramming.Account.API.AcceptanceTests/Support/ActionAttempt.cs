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

    public class ActionAttempt<TResult>(Func<Task<TResult>> action)
    {
        public TResult? LastResult { get; private set; }

        public async Task<TResult> PerformAsync()
        {
            LastResult = await action();
            return LastResult;
        }
    }

    public static class ActionAttemptFactory
    {
        public static ActionAttempt<TInput, TResult> Create<TInput, TResult>(Func<TInput, Task<TResult>> action)
        {
            return new ActionAttempt<TInput, TResult>(action);
        }

        public static ActionAttempt<TResult> Create<TResult>(Func<Task<TResult>> action)
        {
            return new ActionAttempt<TResult>(action);
        }
    }

}
