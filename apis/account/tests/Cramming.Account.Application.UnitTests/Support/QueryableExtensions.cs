//using System.Linq.Expressions;

//namespace Microsoft.EntityFrameworkCore
//{
//    public static class QueryableExtensions
//    {
//        public static IAsyncEnumerable<T> AsAsyncQueryable<T>(this IQueryable<T> source)
//        {
//            return source.AsAsyncEnumerable();
//        }
//    }

//    public class AsyncQueryable<T> : IQueryable<T>
//    {
//        private readonly IEnumerable<T> _source;

//        public AsyncQueryable(IEnumerable<T> source)
//        {
//            _source = source ?? throw new ArgumentNullException(nameof(source));
//            Provider = new AsyncQueryProvider<T>();
//            Expression = Expression.Constant(this);
//        }

//        public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

//        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

//        public Type ElementType => typeof(T);

//        public Expression Expression { get; }

//        public IQueryProvider Provider { get; }

//        public async Task<List<T>> ToListAsync(CancellationToken cancellationToken = default)
//        {
//            // Simula uma operação assíncrona
//            await Task.Delay(100, cancellationToken);
//            return _source.ToList();
//        }
//    }

//    public class AsyncQueryProvider<T> : IQueryProvider
//    {
//        public IQueryable CreateQuery(Expression expression) =>
//            throw new NotImplementedException();

//        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
//            new AsyncQueryable<TElement>(expression);

//        public object Execute(Expression expression) =>
//            throw new NotImplementedException();

//        public TResult Execute<TResult>(Expression expression) =>
//            throw new NotImplementedException();

//        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) =>
//            throw new NotImplementedException();
//    }
//}
