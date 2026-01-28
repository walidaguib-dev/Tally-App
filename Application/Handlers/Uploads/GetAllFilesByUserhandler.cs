using Application.Dtos.Uploads;
using Application.Mappers;
using Application.Queries.Uploads;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Uploads
{
    public class GetAllFilesByUserhandler(
        IUploads uploadsService
        ) : IRequestHandler<Queries.Uploads.GetAllFilesByUserQuery, List<Dtos.Uploads.UploadDto>>
    {
        private readonly IUploads _uploadsService = uploadsService;
        public async Task<List<UploadDto>> Handle(GetAllFilesByUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _uploadsService.GetAllFilesByUserAsync(request.UserId);
            var response = result.Select(e => e.ToDto()).ToList();
            return response;
        }
    }
}
