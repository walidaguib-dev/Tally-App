using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public record DeleteTallySheetMerchandise(int Id) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => throw new NotImplementedException();

        public List<string> CacheTags => throw new NotImplementedException();
    }
}