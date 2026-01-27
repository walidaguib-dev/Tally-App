using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Uploads
{
    public record UploadFileCommand(IFormFile File , string userId) : IRequest<Domain.Entities.Uploads>;
    
}
