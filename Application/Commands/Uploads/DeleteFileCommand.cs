using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Uploads
{
    public record DeleteFileCommand(string PublicId) : IRequest<object>;
    
}
