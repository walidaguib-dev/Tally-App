using System;
using System.Collections.Generic;
using System.Text;
using Application.Dtos.Mail;
using Domain.Entities;

namespace Application.Mappers
{
    public static class EmailMapper
    {
        public static EmailDto MapToEmailJson(this EmailToken emailToken)
        {
            return new EmailDto
            {
                Username = emailToken.User.UserName!,
                Id = emailToken.Id,
                ExpiresAt = emailToken.ExpiresAt,
                CreatedAt = emailToken.CreatedAt,
                ConsumedAt = emailToken.ConsumedAt,
                Purpose = emailToken.Purpose,
            };
        }
    }
}
