using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lyulyulyu.Components.Resources
{
    public interface ICityDoodleManager
    {
        List<Resource> GetImages();
        int GetImageCount();
    }
    public class CityDoodleManager : ICityDoodleManager
    {
        private readonly IWebHostEnvironment _env;
        private List<Resource> resouce;

        public CityDoodleManager(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
            this.GetImages();
        }
        public List<Resource> GetImages()
        {
            if (resouce != null)
            {
                return resouce;
            }

            string[] filePaths = Directory.GetFiles(Path.Combine(_env.WebRootPath, @"CityDoodle"));
            List<Resource> imagelist = new List<Resource>();
            foreach (string filePath in filePaths)
            {
                var img = new Resource();
                img.DisplayPath = @"CityDoodle/" + Path.GetFileName(filePath);
                img.DownloadPath = @"CityDoodle/" + Path.GetFileName(filePath);
                img.Name = Path.GetFileNameWithoutExtension(filePath);
                img.Type = Path.GetExtension(filePath);
                imagelist.Add(img);
            }

            this.resouce = imagelist;
            return imagelist;
        }

        public int GetImageCount()
        {
            return this.resouce.Count;
        }
    }
}
