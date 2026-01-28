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
    public class GetUploadByUserHandler(
        IUploads uploadsService
        ) : IRequestHandler<Queries.Uploads.GetUploadByUserQuery, Dtos.Uploads.UploadDto?>
    {
        private readonly IUploads _uploadsService = uploadsService;
        public async Task<UploadDto?> Handle(GetUploadByUserQuery request, CancellationToken cancellationToken)
        {
            var upload = await _uploadsService.GetUploadByUserAsync(request.userId);
            return upload!.ToDto();
        }
    }
}
