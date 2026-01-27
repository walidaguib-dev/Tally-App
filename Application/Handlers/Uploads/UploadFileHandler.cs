using Application.Commands.Uploads;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Uploads
{
    public class UploadFileHandler(
        IUploads uploadsService
        ) : IRequestHandler<UploadFileCommand, Domain.Entities.Uploads>
    {
        private readonly IUploads _uploadsService = uploadsService;
        public async Task<Domain.Entities.Uploads> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            var result = await _uploadsService.UploadsAsync(request.userId,request.File);
            return result;
        }
    }
}
