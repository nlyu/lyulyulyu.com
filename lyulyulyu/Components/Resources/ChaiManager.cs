using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lyulyulyu.Components.Resources
{
    public interface IChaiManager
    {
        List<Resource> GetImages();
        int GetImageCount();
    }
    public class ChaiManager : IChaiManager
    {
        private readonly IWebHostEnvironment _env;
        private List<Resource> resouce;

        public ChaiManager(IWebHostEnvironment webHostEnvironment)
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

            string[] filePaths = Directory.GetFiles(Path.Combine(_env.WebRootPath, @"Chai"));
            List<Resource> pptlist = new List<Resource>();
            foreach (string filePath in filePaths)
            {
                var ppt = new Resource();
                ppt.DisplayPath = @"Chai/" + Path.GetFileNameWithoutExtension(filePath) + @".png";
                ppt.DownloadPath = @"Chai/download/" + Path.GetFileNameWithoutExtension(filePath) + @".pptx";
                ppt.Name = Path.GetFileNameWithoutExtension(filePath);
                ppt.Type = Path.GetExtension(filePath);
                pptlist.Add(ppt);
            }

            this.resouce = pptlist;
            return pptlist;
        }

        public int GetImageCount()
        {
            return this.resouce.Count;
        }
    }
}
