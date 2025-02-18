using System.Text.Json;

namespace lyulyulyu.Components
{
    public class ViewCounter : BackgroundService
    {
        private readonly IWebHostEnvironment _env;
        public ViewCount viewCount;

        public ViewCounter(IWebHostEnvironment webHostEnvironment)
        {
            _env = webHostEnvironment;
            this.viewCount = this.GetViewCountFromFile();
        }

        public ViewCount GetViewCountFromFile()
        {
            string filePaths = Path.Combine(_env.WebRootPath, @"Home", "ViewCount.txt");
            if (File.Exists(filePaths))
            {
                using (StreamReader sr = File.OpenText(filePaths))
                {
                    try
                    {
                        var viewCount = JsonSerializer.Deserialize<ViewCount>(sr.ReadToEnd());
                        return viewCount;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return null;
        }

        public void WriteViewCountFromFile(ViewCount viewCount)
        {
            string filePaths = Path.Combine(_env.WebRootPath, @"Home", "ViewCount.txt");
            if (File.Exists(filePaths))
            {
                string json = JsonSerializer.Serialize(viewCount);
                File.WriteAllText(filePaths, json);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (this.viewCount != null)
                {
                    this.WriteViewCountFromFile(this.viewCount);
                }

                await Task.Delay(TimeSpan.FromMinutes(60));
            }
        }

        public class ViewCount
        {
            public int AllView { get; set; }
            public int ChaiView { get; set; }
            public int CityDoodleView { get; set; }
        }
    }
}
