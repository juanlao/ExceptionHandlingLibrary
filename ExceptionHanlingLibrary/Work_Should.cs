using FluentAssertions;

namespace ExceptionHanlingLibrary
{
    public class Work_Should
    {
        [Fact]
        public async Task RunATask()
        {
            var expectedResult = 2;
            var work = new Work<int>(
                () => { return Task.FromResult(expectedResult); });

            var result = await work.Run();

            result.IsSuccess
                .Should()
                .Be(true);
        }

        [Fact]
        public async Task RaiseTaskException_WhenNoHandler()
        {
            var work = new Work<int>(
                () => { throw new NotImplementedException(); });

            var action = async () => { await work.Run(); };

            await action.Should().ThrowAsync<NotImplementedException>();
        }

        [Fact]
        public async Task ManageExceptionWhenRunningATask()
        {
            var handler = new MyExceptionHandler();
            var work = new Work<int>(
                () => { throw new Exception(); },
                handler);

            var result = await work.Run();

            result.IsError
                .Should()
                .BeTrue();
        }
    }
}