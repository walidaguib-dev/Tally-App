using Application.Commands.Uploads;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Uploads
{
    public class UpdateFileHandler(
        IUploads uploadsService
        ) : IRequestHandler<UpdateFileCommand, Domain.Entities.Uploads?>
    {
        private readonly IUploads _uploadService = uploadsService;
        public async Task<Domain.Entities.Uploads?> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var file = request.file;
            var oldPublicId = request.oldPublicId;
            var result = await _uploadService.UpdateAsync(userId, file, oldPublicId);
            return result;
        }
    }
}
