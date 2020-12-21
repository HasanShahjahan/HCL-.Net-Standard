using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class CategoryDto
    {
        public string Size { get; set; }
        public string Name { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Length { get; set; }
        public int SizeOrder { get; set; }
        public int AvailableLockerTotal { get; set; }
    }
}
