using FileHosting.Shared.AppCore.Validation;

namespace FileHosting.Storage.AppCore.Errors;

public static class DomainErrors
{
    public static class Folder
    {
        public static readonly Error InvalidUserId = new("Folder.InvalidUserId", "Invalid UserId");
        public static readonly Error EmptyName = new("Folder.EmptyName", "Empty Name");
        public static readonly Error NotFound = new("Folder.NotFound", "Folder not found");
    }

    public static class FolderItem
    {
        public static readonly Error InvalidFolderId = new("FolderItem.InvalidFolderId", "Invalid FolderId");
        public static readonly Error EmptyName = new("FolderItem.EmptyName", "Empty Name");
        public static readonly Error EmptyPath = new("FolderItem.EmptyPath", "Empty Path");
        public static readonly Error NotFound = new("FolderItem.NotFound", "Folder item not found");
    }

    public static class User
    {
        public static readonly Error InvalidId = new("User.InvalidId", "Invalid Id");
        public static readonly Error EmptyUserName = new("User.EmptyUserName", "Empty UserName");
    }
}