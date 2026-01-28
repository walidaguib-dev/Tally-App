using System;
using System.Collections.Generic;
using System.Text;
using Application.Dtos.Users.Profiles;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserProfileMapper
    {
        public static UserProfile MapToEntity(this CreateUserProfileDto model)
        {

            return new UserProfile
            {
                Id = model.Id,
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Bio = model.Bio,
                CreatedAt = model.CreatedAt,
                UpdatedAt = null,
                UploadId = model.UploadId,
            };
        }

        public static ProfileDto MapToDto(this UserProfile entity)
        {
            return new ProfileDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                UploadId = entity.UploadId,
                url = entity.Upload!.Url,
                username = entity.User.UserName!,
            };
        }
    }
}
