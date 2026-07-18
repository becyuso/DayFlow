
using System.Text.Json;

namespace DayFlow.Web.Services
{
    public interface IViteManifestService
    {
        string GetAsset(string entry);
        string GetCss(string entry);
    }

    public class ViteManifestService : IViteManifestService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, ViteManifestEntry> _manifest;

        public ViteManifestService(
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;

            var manifestPath = Path.Combine(
                _environment.WebRootPath,
                "dist",
                ".vite",
                "manifest.json"
            );

            if (!File.Exists(manifestPath))
            {
                throw new FileNotFoundException(
                    $"Vite manifest not found: {manifestPath}"
                );
            }

            var json = File.ReadAllText(manifestPath);

            _manifest =
                JsonSerializer.Deserialize
                <Dictionary<string, ViteManifestEntry>>(json)
                ?? new();
        }

        public string GetAsset(string entry)
        {
            if (!_manifest.TryGetValue(entry, out var asset))
            {
                throw new Exception(
                    $"Vite entry '{entry}' not found."
                );
            }
            return "/dist/" + asset.File;
        }

        public string GetCss(string entry)
        {
            if (!_manifest.TryGetValue(entry, out var asset))
            {
                return string.Empty;
            }

            if (asset.Css == null)
                return string.Empty;

            return string.Join(
                "",
                asset.Css.Select(
                    css =>
                    $"<link rel=\"stylesheet\" href=\"/dist/{css}\" />"
                )
            );
        }
    }

    public class ViteManifestEntry
    {
        public string File { get; set; } = "";

        public bool IsEntry { get; set; }

        public string[]? Css { get; set; }


        public string[]? Imports { get; set; }
    }
}