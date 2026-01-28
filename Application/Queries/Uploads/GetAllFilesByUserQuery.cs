using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Uploads
{
    public record GetAllFilesByUserQuery(string UserId) : MediatR.IRequest<List<Dtos.Uploads.UploadDto>>;
}
