using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class LanguagesDto
    {
        public LanguagesDto(string english, string melaya, string _中文, string _தமிழ்)
        {
            English = english;
            Melaya = melaya;
            中文 = _中文;
            தமிழ் = _தமிழ்;
        }
        public string English { get; set; }
        public string Melaya { get; set; }
        public string 中文 { get; set; }
        public string தமிழ் { get; set; }
    }
}
