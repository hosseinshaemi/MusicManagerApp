using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Spotify.Utils.Contracts;
namespace Spotify.Utils;

public class Aws3Services : IAws3Services
{
    private const string BUCKET_NAME = "ui-ac-spotify";
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;

    public Aws3Services(IConfiguration configuration)
    {
        _configuration = configuration;

        var credentials = new Amazon.Runtime.BasicAWSCredentials(
            _configuration.GetSection("CDN").GetSection("AccessKey").Value,
            _configuration.GetSection("CDN").GetSection("SecretKey").Value
        );

        var config = new AmazonS3Config { ServiceURL = _configuration.GetSection("CDN").GetSection("ServiceURL").Value };
        _s3Client = new AmazonS3Client(credentials, config);
    }

    public async Task<string?> UploadFile(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            {
                PutObjectRequest request = new()
                {
                    BucketName = BUCKET_NAME,
                    Key = file.FileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                request.Metadata.Add("x-amz-meta-title", "mytitle");
                PutObjectResponse response = await _s3Client.PutObjectAsync(request);
                return $"https://{BUCKET_NAME}.s3.ir-thr-at1.arvanstorage.ir/{file.FileName}";
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Error in uploading...");
            return null;
        }
    }
}