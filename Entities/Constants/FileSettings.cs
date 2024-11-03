namespace Core.Constants
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 2;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
