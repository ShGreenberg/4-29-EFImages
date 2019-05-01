using System;
using System.Collections.Generic;
using System.Text;

namespace _4_29_EFImages.data2
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
        public bool Disabled { get; set; }
    }
}
