using UnityEngine;
using UnityEditor;
using static System.IO.Path;
using static System.IO.Directory;
using static UnityEditor.AssetDatabase;
public static class Setup
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        Folders.CreateDefault("_Project", "Animations", "Materials", "Arts", "Prefabs", "ScriptableObjects", "Scripts", "Settings");
        Refresh();
    }

    static class Folders
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullPath = Combine(Application.dataPath, root);
            foreach (var folder in folders)
            {
                var path = Combine(fullPath, folder);
                if (!Exists(path))
                {
                    CreateDirectory(path);
                }
            }
        }
    }
}
