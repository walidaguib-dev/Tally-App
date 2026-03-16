using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class UploadMapper
    {
        public static Dtos.Uploads.UploadDto ToDto(this Domain.Entities.Uploads upload)
        {
            return new Dtos.Uploads.UploadDto
            {
                Id = upload.Id,
                UserId = upload.UserId,
                Url = upload.Url,
                PublicId = upload.PublicId,
                FileType = upload.FileType,
                FileSize = upload.FileSize,
                UploadedAt = upload.UploadedAt,
                username = upload.User.UserName!,
            };
        }
    }
}
