using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sample4._6Api.Models
{
    public class GetSampleData : IRequestHandler<GetSampleDataQuery, string>
    {
        public async Task<string> Handle(GetSampleDataQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => "I am the best"); 
        }
    }
}