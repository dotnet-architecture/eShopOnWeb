namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using System;
  using System.IO;
  using System.Linq;

  /// <summary>
  /// Helper tools related to git
  /// </summary>
  public class GitService
  {
    /// <summary>
    /// Finds root folder of the current git repository.
    /// </summary>
    /// <returns>DirectoryInfo of the directory that contains the ".git" directory or returns null if not in a git repository</returns>
    public DirectoryInfo GitRootDirectoryInfo()
    {
      var directory = new DirectoryInfo(Environment.CurrentDirectory);
      bool found = IsGitDirectory(directory);
      while (!found && directory.Parent != null)
      {
        directory = directory.Parent;
        found = IsGitDirectory(directory);
      }

      return directory;
    }

    /// <summary>
    /// Determines whether or not the specified directory is the root of a git repository
    /// </summary>
    /// <param name="aDirectoryInfo">The directory to check</param>
    public bool IsGitDirectory(DirectoryInfo aDirectoryInfo) =>
      aDirectoryInfo.GetDirectories(".git").Any() || aDirectoryInfo.GetDirectories(".template.config").Any();
  }
}
