using LSS.BE.Core.DataObjects.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class AssignSimilarSizeLockerDto
    {
        public int BookingId { get; set; }
        public List<int> AssignedLockers { get; set; }
        public string HardwareDoorNumber { get; set; }
        public List<List<List<CompartmentDetailDto>>> LockerPreview { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
