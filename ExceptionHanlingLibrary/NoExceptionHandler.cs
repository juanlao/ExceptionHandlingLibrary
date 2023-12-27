namespace ExceptionHanlingLibrary
{
    public class NoExceptionHandler : ExceptionHandler
    {
        public void Handle(Exception e)
        {
            throw e;
        }
    }
}