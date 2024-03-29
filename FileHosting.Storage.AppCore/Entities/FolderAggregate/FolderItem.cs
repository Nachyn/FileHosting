﻿// ReSharper disable UnusedAutoPropertyAccessor.Local

using FileHosting.Shared.AppCore.Entities;
using FileHosting.Shared.AppCore.Validation;
using FileHosting.Storage.AppCore.Errors;

namespace FileHosting.Storage.AppCore.Entities.FolderAggregate;

public class FolderItem : BaseEntity
{
    private FolderItem()
    {
    }

    public string Name { get; private set; } = null!;

    public string Path { get; private set; } = null!;

    public DateTime CreatedDate { get; private set; }

    public int FolderId { get; private set; }

    public Result SetPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return Result.Failure(DomainErrors.FolderItem.EmptyPath);
        }

        Path = SanitizePath(path);
        return Result.Success();
    }

    public static Result<FolderItem> Create(
        int folderId,
        string name,
        string path)
    {
        if (folderId < 1)
        {
            return Result.Failure<FolderItem>(DomainErrors.FolderItem.InvalidFolderId);
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<FolderItem>(DomainErrors.FolderItem.EmptyName);
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return Result.Failure<FolderItem>(DomainErrors.FolderItem.EmptyPath);
        }

        return Result.Success(new FolderItem
        {
            Name = name.Trim(),
            Path = SanitizePath(path),
            CreatedDate = DateTime.Now,
            FolderId = folderId,
            Id = 0
        });
    }

    private static string SanitizePath(string path)
    {
        return string.Join('/', path.Split('\\', '/').Where(s => s.Length > 0).Select(s => s.Trim()));
    }
}