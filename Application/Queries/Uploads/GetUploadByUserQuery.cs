using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Uploads
{
    public record GetUploadByUserQuery(string userId) : IRequest<Dtos.Uploads.UploadDto?>;
    


}
