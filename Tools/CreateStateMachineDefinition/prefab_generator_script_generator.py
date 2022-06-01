# coding: utf-8

import os
import sys
import shutil
import json

def Create(fileName):
    # ファイル開く
    src = open(fileName, 'r', encoding="utf-8-sig")
    lineList = src.readlines()

    output = ""
    output += "using UnityEngine;\n"
    output += "using UnityEngine.UI;\n"
    output += "using UnityEditor;\n"
    output += "using UnityEditor.SceneManagement;\n"
    output += "using System.IO;\n"
    output += "using System.Collections;\n"
    output += "using System.Collections.Generic;\n"
    output += "using System.Reflection;\n"
    output += "\n"
    output += "public class UIGenerator : EditorWindow {\n"
    output += '    [UnityEditor.MenuItem("ShortCutCommand/UIGeneratorStart")]\n'
    output += '    public static void UIGeneratorStart() {\n'
    output += '        string[] guids;\n'

    tuple1 = ParseLine(lineList, 0, "", 0, "")
    output += tuple1[0]
    output += "    }\n"
    output += "}\n"

    src.close()

    SaveFile(output, "UIGenerator.cs")

def ParseLine(lineList, lineIndex, parentName, nestCount, output):

    while True:
        if lineIndex >= len(lineList):
            break;

        tuple1 = (output, lineIndex)
        line = lineList[lineIndex]
        nextNextCount = line.count('    ')
        if nextNextCount < nestCount:
            break

        paramList = line.split()
        index = 0
        for param in paramList:
            if index == 0:
                if paramList[0] == "Create":
                    output += '        GameObject %s = new GameObject("%s");\n' % (paramList[1], paramList[1])
                else:
                    output += '        guids = AssetDatabase.FindAssets("t:prefab %s");\n' % (paramList[2])
                    output += '        GameObject prefab%s = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0])m typeof(GameObject)) as GameObject;\n' % (paramList[1])
                    output += '        GameObject %s = PrefabUtility.InstantiatePrefab(prefab%s) as GameObject;\n' % (paramList[1], paramList[1])
                    output += '        %s.name = "%s";\n' % (paramList[1], paramList[1])

            elif index >= 2:
                if paramList[0] == "Create":
                    output += '        %s.AddComponent<%s>();\n' % (paramList[1], paramList[index])

            index = index + 1

        if parentName != "":
            output += '        %s.transform.SetParent(%s.transform);\n' % (paramList[1], parentName)
                    

        lineIndex += 1
        tuple1 = (output, lineIndex)
        if lineIndex < len(lineList):
            line = lineList[lineIndex]
            nextNextCount = line.count('    ')

            if nextNextCount > nestCount:
                tuple1 = ParseLine(lineList, lineIndex, paramList[1], nextNextCount, output)
                output = tuple1[0]
            elif nextNextCount < nestCount:
                break

            lineIndex = tuple1[1]

    return tuple1

def SaveFile(output, filePathAndName):
    isExist = os.path.isfile(filePathAndName)
    of = open(filePathAndName, 'wb')
    of.write(output.encode('utf-8-sig'))
    of.close()

fileName = sys.argv[1]
Create(fileName)
        
