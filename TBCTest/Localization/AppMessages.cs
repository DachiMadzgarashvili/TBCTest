namespace TBCTest.LocalizationSupport
{
    public static class AppMessages
    {
        public static readonly Dictionary<string, string> Defaults = new()
        {
            { RequiredField, "This field is required." },
            { ValidationError, "One or more validation errors occurred." },
            { InvalidGender, "Invalid gender." },
            { PersonNotFound, "Person not found." },
            { RelatedPersonNotFound, "Related person not found." },
            { PersonOrRelatedNotFound, "Person or related person not found." },
            { CannotRelateToSelf, "Cannot relate a person to themselves." },
            { RelationAdded, "Relation added successfully." },
            { RelationRemoved, "Relation removed successfully." },
            { NoFileProvided, "No file provided." },
            { ImageUploadFailed, "Image upload failed." },
            { ImageDeleted, "Image deleted." },
            { NoImageToDelete, "No image to delete." },
            { CityNotFound, "City not found." },
            { CityUpdated, "City updated successfully." },
            { CityDeleted, "City deleted successfully." },
            { LocalizationNotFound, "Localization entry not found." },
            { LocalizationUpdated, "Localization updated successfully." },
        };

        public const string RequiredField = "RequiredField";
        public const string ValidationError = "ValidationError";
        public const string InvalidGender = "InvalidGender";
        public const string PersonNotFound = "PersonNotFound";
        public const string RelatedPersonNotFound = "RelatedPersonNotFound";
        public const string PersonOrRelatedNotFound = "PersonOrRelatedNotFound";
        public const string CannotRelateToSelf = "CannotRelateToSelf";
        public const string RelationAdded = "RelationAdded";
        public const string RelationRemoved = "RelationRemoved";
        public const string NoFileProvided = "NoFileProvided";
        public const string ImageUploadFailed = "ImageUploadFailed";
        public const string ImageDeleted = "ImageDeleted";
        public const string NoImageToDelete = "NoImageToDelete";
        public const string CityNotFound = "CityNotFound";
        public const string CityUpdated = "CityUpdated";
        public const string CityDeleted = "CityDeleted";
        public const string LocalizationNotFound = "LocalizationNotFound";
        public const string LocalizationUpdated = "LocalizationUpdated";
    }
}
