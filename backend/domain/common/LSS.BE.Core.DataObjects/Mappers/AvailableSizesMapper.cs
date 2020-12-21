using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Courier;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class AvailableSizesMapper
    {
        public static AvailableSizesDto ToObject(this AvailableSizesResponse model)
        {
            return new AvailableSizesDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                Categories = model.Categories.ToDto(),
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }

        private static List<CategoryDto> ToDto(this List<Category> categories)
        {
            if (categories == null) return null;
            var dto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                var model = new CategoryDto
                {
                    Size = category.Size,
                    Name = category.Name,
                    Width = category.Width,
                    Height = category.Height,
                    Length = category.Length,
                    SizeOrder = category.SizeOrder,
                    AvailableLockerTotal = category.AvailableLockerTotal
                };
                dto.Add(model);
            }
            return dto;
        }
    }
}