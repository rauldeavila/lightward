using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class RenameChildren : MonoBehaviour
{
    [Title("Rename Settings")]
    [SuffixLabel("-suffix")]
    public string Suffix;

    [Button("Rename All Children")]
    private void RenameAllChildren()
    {
        RenameChildrenWithSuffix(transform, Suffix);
    }

    [Button("Remove Suffix from All Children")]
    private void RemoveSuffixFromAllChildren()
    {
        RemoveSuffixFromChildren(transform);
    }

    private void RenameChildrenWithSuffix(Transform parent, string suffix)
    {
        foreach (Transform child in parent)
        {
            // Rename the child
            child.name += suffix;

            // Recursively rename the children of the child
            RenameChildrenWithSuffix(child, suffix);
        }
    }

    private void RemoveSuffixFromChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Remove the suffix from the child's name
            int suffixIndex = child.name.LastIndexOf('-');
            if (suffixIndex != -1)
            {
                child.name = child.name.Substring(0, suffixIndex);
            }

            // Recursively remove the suffix from the children of the child
            RemoveSuffixFromChildren(child);
        }
    }
}
