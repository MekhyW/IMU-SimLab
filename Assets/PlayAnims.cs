using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;

public class PlayAnims : MonoBehaviour
{
    private Animator animator;
    private Dictionary<string, string[]> animationSets;
    private string[] folderNames = {"Dance", "Idle", "Run", "Spin", "Walk"};
    public string currentSet = "Dance";
    public int currentAnimIndex = 0;

    void PlayCurrentAnimation()
    {
        string animationName = animationSets[currentSet][currentAnimIndex];
        if (animator.HasState(0, Animator.StringToHash(animationName)))
        {
            animator.Play(animationName, 0, 0f);
            Debug.Log($"Playing animation: {animationName}");
        }
        else Debug.LogError($"Animation state '{animationName}' not found in controller!");
    }

    void NextAnimation()
    {
        currentAnimIndex++;
        if (currentAnimIndex >= animationSets[currentSet].Length)
        {
            currentAnimIndex = 0;
            switch (currentSet)
            {
                case "Dance": currentSet = "Idle"; break;
                case "Idle": currentSet = "Run"; break;
                case "Run": currentSet = "Spin"; break;
                case "Spin": currentSet = "Walk"; break;
                case "Walk": 
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    return;
            }
        }
        PlayCurrentAnimation();
    }

    void LoadAnimationFiles()
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        if (controller == null)
        {
            Debug.LogError("No Animator Controller assigned!");
            return;
        }
        AnimatorController animController = controller as AnimatorController;
        if (animController == null)
        {
            Debug.LogError("Runtime Animator Controller must be an Animator Controller!");
            return;
        }
        AnimatorControllerLayer layer = animController.layers[0];
        foreach (string folder in folderNames)
        {
            string[] files = Directory.GetFiles("Assets/" + folder, "*.fbx");
            string[] animNames = files.Select(Path.GetFileNameWithoutExtension).ToArray();
            foreach (string animName in animNames)
            {
                string path = $"Assets/{folder}/{animName}.fbx";
                AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                if (clip == null)
                {
                    Debug.LogError($"Failed to load animation clip: {path}");
                    continue;
                }
                AnimatorState state = layer.stateMachine.AddState(animName);
                state.motion = clip;
            }
            if (animNames.Length > 0) 
            {
                animationSets.Add(folder, animNames);
                Debug.Log($"Loaded {animNames.Length} animations from {folder} folder");
            }
            else Debug.LogWarning($"No animations found in {folder} folder");
        }
        EditorUtility.SetDirty(animController);
        AssetDatabase.SaveAssets();
        if (animationSets.Count > 0) currentSet = animationSets.Keys.First();
        else Debug.LogError("No animation sets were loaded!");
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on this GameObject!");
            return;
        }
        animationSets = new Dictionary<string, string[]>();
        LoadAnimationFiles();
    }

    void Start()
    {
        PlayCurrentAnimation();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f) NextAnimation();
    }
}