using Application.Commands.Uploads;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Uploads
{
    public class DeleteFileHandler(
        IUploads uploadsService
        ) : IRequestHandler<DeleteFileCommand, object>
    {
        private readonly IUploads _uploadsService = uploadsService;
        public async Task<object> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var result = await _uploadsService.DeleteUploadAsync(request.PublicId);
            return result;
        }
    }
}
