
using OperationResult;
using static OperationResult.Helpers;

namespace ExceptionHanlingLibrary
{
    public class Work<T>
    {
        private readonly Func<Task<T>> task;
        private readonly ExceptionHandler exceptionHandler;

        public Work(Func<Task<T>> task)
            : this(task, new NoExceptionHandler())
        { }

        public Work(
            Func<Task<T>> task,
            ExceptionHandler handler)
        {
            this.task = task;
            exceptionHandler = handler;
        }

        public async Task<Result<T>> Run()
        {
            try
            {
                var result = await task();
                return Ok(result);
            }
            catch (Exception ex)
            {
                exceptionHandler.Handle(ex);
                return Error();
            }
        }
    }
}