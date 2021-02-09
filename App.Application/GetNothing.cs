// <copyright file="GetNothing.cs" company="None">
// Free and open source code.
// </copyright>

namespace App.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    /// <summary>
    /// Get nothing class.
    /// </summary>
    public static class GetNothing
    {
        /// <summary>
        /// Get nothing query.
        /// </summary>
        public class Query : IRequest<Result>
        {
            /// <summary>
            /// Gets or sets ID of nothing.
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// Get nothing result class.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets message.
            /// </summary>
            public string Message { get; internal set; }
        }

        /// <summary>
        /// Handler class.
        /// </summary>
        public class Handler : IRequestHandler<Query, Result>
        {
            /// <summary>
            /// Get nothing handler.
            /// </summary>
            /// <param name="request">Get nothing query.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>String.</returns>
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(new Result() { Message = $"Nothing {request.Id}" });
            }
        }
    }
}