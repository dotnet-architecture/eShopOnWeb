namespace eShopOnBlazorWasm.EndToEnd.Tests.Infrastructure
{
  using System;
  using System.IO;

  /// <summary>
  /// Helper tools related to git
  /// </summary>
  public class DirectoryService
  {
    /// <summary>
    /// Find the parent directory that contains the Solution file .
    /// </summary>
    /// <returns>DirectoryInfo of the directory that contains the target directory or returns null if not in a git repository</returns>
    public DirectoryInfo FindSolutionRoot()
    {
      const string SolutionFileName = "eShopOnBlazorWasm.sln";
      var directory = new DirectoryInfo(Environment.CurrentDirectory);
      bool found = directory.GetFiles(SolutionFileName).Length > 0;
      while (!found && directory.Parent != null)
      {
        directory = directory.Parent;
        found = directory.GetFiles(SolutionFileName).Length > 0;
      }

      return directory;
    }
  }
}
