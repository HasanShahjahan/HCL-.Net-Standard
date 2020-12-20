using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Mappers
{
    public static class CaptureMapper
    {
        public static CaptureDto ToError(this CaptureDto model)
        {
            return new CaptureDto()
            {
                StatusCode = model.StatusCode,
                Error = model.Error
            };
        }
        public static CaptureDto ToObject(this CaptureDto model)
        {
            return new CaptureDto()
            {
                LockerId = model.LockerId,
                TransactionId = model.TransactionId,
                CaptureImage = new Entities.Locker.Image(model.CaptureImage.ImageExtension, model.CaptureImage.ImageData),
                StatusCode = StatusCode.Status200OK,
                Error = null
            };
        }

    }
}
