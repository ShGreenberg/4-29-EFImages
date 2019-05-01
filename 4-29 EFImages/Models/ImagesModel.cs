using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4_29_EFImages.Models
{
    public class ImagesModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public bool Disabled { get; set; }
    }
}
