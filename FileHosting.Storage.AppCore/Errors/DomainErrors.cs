using FileHosting.Shared.AppCore.Validation;

namespace FileHosting.Storage.AppCore.Errors;

public static class DomainErrors
{
    public static class FolderItem
    {
        public static readonly Error InvalidFolderId = new Error("FolderItem.InvalidFolderId", "Invalid FolderId");
        public static readonly Error EmptyName = new Error("FolderItem.EmptyName", "Empty Name");
        public static readonly Error EmptyPath = new Error("FolderItem.EmptyPath", "Empty Path");
    }
}