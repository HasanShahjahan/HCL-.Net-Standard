using LSS.BE.Core.DataObjects.BaseDtos;
using System.Collections.Generic;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class AvailableSizesDto
    {
        public bool IsRequestSuccess { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
