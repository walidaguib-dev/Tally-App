using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Uploads
{
   public record UpdateFileCommand(string userId, IFormFile file, string oldPublicId) : IRequest<Domain.Entities.Uploads?>;
    

}
