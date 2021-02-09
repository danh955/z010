namespace ConsoleApp
{
    using System;
    using System.Threading.Tasks;
    using App.Application;
    using MediatR;

    internal class MainApp
    {
        private readonly IMediator mediator;

        public MainApp(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Start()
        {
            var nothingResult = await this.mediator.Send(new GetNothing.Query() { Id = 321 });
            Console.WriteLine(nothingResult);
        }
    }
}