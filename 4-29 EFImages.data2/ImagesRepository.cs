using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _4_29_EFImages.data2
{
    public class ImagesRepository
    {
        private readonly string _connString;

        public ImagesRepository(string connString)
        {
            _connString = connString;
        }

        public void AddImage(Image image)
        {
            using(var context = new ImageContext(_connString))
            {
                context.Images.Add(image);
                context.SaveChanges();
            }
        }

        public IEnumerable<Image> GetImages()
        {
            using(var context = new ImageContext(_connString))
            {
                return context.Images.ToList();
            }
        }

        public Image GetImage(int id)
        {
            using(var context = new ImageContext(_connString))
            {
                return context.Images.FirstOrDefault(i => i.Id == id);
            }
        }

        public void AddLike(int id)
        {
            using (var context = new ImageContext(_connString))
            {
                context.Database.ExecuteSqlCommand("UPDATE Images SET Likes = Likes+1 WHERE Id = @id",
                    new SqlParameter("@id", id));
            }
        }
    }
}
